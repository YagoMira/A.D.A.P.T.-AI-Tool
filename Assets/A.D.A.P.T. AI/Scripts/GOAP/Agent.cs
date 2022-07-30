using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public string agentName; //Name of the Agent
    public List<Resource> goals_list = new List<Resource>();
    public Dictionary<string, Resource> goals = new Dictionary<string, Resource>(); //Set of multiple agent's goals: <Name, Priority>
    public List<Action> actions = new List<Action>(); //List of agent's actions to achieve a goal
    public Action currentAction; //Current running Action
    public string currentGoal; //Current running Goal
    Planner planner; //Planer for get the actual action sequence

    public void Start()
    {
        SetActions();

        foreach (Resource r in goals_list)
        {
            goals.Add(r.resourceName, r);
        }
    }

    public void LateUpdate()
    {
        DebugPlanner();
    }

    public Action GetCurrentAction()
    {
        return currentAction;
    }

    public string GetCurrentGoal()
    {
        return currentGoal;
    }

    public void SetCurrentAction(Action action)
    {
        currentAction = action;
    }

    public void SetCurrentGoal(string key)
    {
        currentGoal = key;
    }

    public void SetActions()
    {
        Action[] agent_actions = this.GetComponents<Action>();
        foreach (Action action in agent_actions)
        {
            actions.Add(action);
        }
        currentAction = null;
        currentGoal = null;
    }

    public bool goToTarget(Action action) //Allows agent go to the target of a determinate Action (if isInRange/or not in other case)
    {
        float actionLimitRange = 0.0f;
        /*foreach (KeyValuePair<string,IResource>p in action.preconditions)
        {
            //TO-FIX!!!!!!!!!!!!!!!!!!!!!!!!!
            
            if(p.Value.resourceType == ResourceType.Position || p.Value.resourceType == ResourceType.WorldElement)
            {
                actionLimitRange = p.Value.limit;
            }
            
        }*/

        if ((Vector3.Distance(gameObject.transform.position, action.transform.position)) <= actionLimitRange)
        {
            action.setInRange(true);
            return true;
        }
        else
        {
            action.setInRange(false);
            return false;
        }
    }

    public void DebugPlanner()
    {
        //Debug.Log("EY");

        Dictionary<string, Resource> test = new Dictionary<string, Resource>();

        /*foreach (KeyValuePair<string,Resource> r in goals)
        {
            Debug.Log("RESOURCE IN GOAL: " + r.Value.resourceName);
        }*/

        test.Add("noPoisoned", new StatusResource("noPoisoned", false, false, 5));
        //Debug.Log("GOALS: " + goals.ContainsKey("isPoisoned"));
        planner = new Planner();
        planner.plan(actions, test, goals); //Acciones posibles | Estado ACTUAL del agente (es decir los recursos que posee AHORA MISMO el agente) | Metas
    }

}

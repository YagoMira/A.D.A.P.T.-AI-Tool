using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public string agentName; //Name of the Agent
    public Dictionary<string, int> goals = new Dictionary<string, int>(); //Set of multiple agent's goals: <Name, Priority>
    public List<Action> actions = new List<Action>(); //List of agent's actions to achieve a goal
    public Action currentAction; //Current running Action
    public string currentGoal; //Current running Goal

    public void Start()
    {
        Action[] agent_actions = this.GetComponents<Action>();
        foreach (Action action in agent_actions)
        {
            actions.Add(action);
        }
        currentAction = null;
        currentGoal = null;
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

    public bool goToTarget(Action action) //Allows agent go to the target of a determinate Action (if isInRange/or not in other case)
    {
        float actionLimitRange = 0.0f;
        foreach (KeyValuePair<string,Resource>p in action.preconditions)
        {
            if(p.Value.resourceType == ResourceType.Position || p.Value.resourceType == ResourceType.WorldElement)
            {
                actionLimitRange = p.Value.limit;
            }
        }

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

}

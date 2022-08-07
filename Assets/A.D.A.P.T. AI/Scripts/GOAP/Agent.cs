using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public string agentName; //Name of the Agent
    public List<Resource> goals_list = new List<Resource>();
    public Dictionary<string, Resource> goals = new Dictionary<string, Resource>(); //Set of multiple agent's goals: <Name, Priority>
    public List<Action> actions = new List<Action>(); //List of agent's actions to achieve a goal
    public Action currentAction; //Current running Action
    public string currentGoal; //Current running Goal
    Planner planner; //Planer for get the actual action sequence
    NavMeshAgent agent;
    public bool onIdle; //If is TRUE then in case of Agent don't have a plan, stays on position. Otherwise (FALSE), goes around the world with random positions.

    //***OTHER PARAMS***
    float rad = 100f; //Radius to get a random direction for navmesh agent. 


    public void Start()
    {
        SetActions();

        foreach (Resource r in goals_list)
        {
            goals.Add(r.resourceName, r);
        }

        agent = gameObject.AddComponent<NavMeshAgent>();
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

    public void IdleState()
    {
        //Call this function when (Planner == null)
        if(onIdle == true) //Then stays on position.
        {
            //Play Idle animation.
            agent.SetDestination(this.transform.position);
        }
        else //Go around the map.
        {
            agent.SetDestination(RandomNavigation());
        }
    }

    public Vector3 RandomNavigation() //Allows agents to move around the map randomly.
    {
        Vector3 randomDirection, destination;
        NavMeshHit hit;

        destination = Vector3.zero;

        randomDirection = Random.insideUnitSphere * rad;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out hit, rad, 1))
        {
            destination = hit.position;
        }

        return destination;
    }

    public void TargetNavigation() { }//Moves agent to the current action target (WorldResource)

    public void DebugPlanner()
    {
        //Debug.Log("EY");

        Dictionary<string, Resource> test = new Dictionary<string, Resource>();

        /*foreach (KeyValuePair<string,Resource> r in goals)
        {
            Debug.Log("RESOURCE IN GOAL: " + r.Value.resourceName);
        }*/

        //test.Add("noPoisoned", new StatusResource("noPoisoned", false, false, 5));
        test.Add("isNear", new WorldResource("isNear", actions[0].target, actions[0].target, 5, 50.0f));
        Debug.Log(actions[0].target.name);
        //Debug.Log("GOALS: " + goals.ContainsKey("isPoisoned"));
        planner = new Planner();
        if(planner.plan(actions, test, goals) == null) //Acciones posibles | Estado ACTUAL del agente (es decir los recursos que posee AHORA MISMO el agente) | Metas
        {
            IdleState();
        }

    }

}

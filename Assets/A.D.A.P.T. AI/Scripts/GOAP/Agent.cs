using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [Serializable]
    public class Goal
    {
        public Action.ResourceStruct goal_precondition;
        public bool hasAction; //In case of the goal has any action to perform.
        public Action goal_Action; //Action to perfom.

        public Goal(Resource resource, bool hasAction)
        {
            this.goal_precondition = new Action.ResourceStruct(resource.resourceName, resource);
            this.hasAction = false;
        }

        public Goal(Resource resource, bool hasAction, Action action)
        {
            this.goal_precondition = new Action.ResourceStruct(resource.resourceName, resource);
            this.hasAction = hasAction;
            this.goal_Action = action;
        }

    }

    public string agentName; //Name of the Agent
    public List<Goal> goals = new List<Goal>(); //!!!!!!!!!!
    //public List<Resource> goals_list = new List<Resource>();
    public Dictionary<string, Goal> goals_list = new Dictionary<string, Goal>(); //Set of multiple agent's goals: <Name, Priority>
    //public List<Goal> goals_list = new List<Goal>(); //!!!!!!!!!!
    //public Dictionary<string, Goal> goals = new Dictionary<string, Goal>(); //Set of multiple agent's goals: <Name, Priority>

    public List<Action> actions = new List<Action>(); //List of agent's actions to achieve a goal
    public Action currentAction; //Current running Action
    public string currentGoal; //Current running Goal
    public Planner planner; //Planer for get the actual action sequence
    Queue<Action> actionsToRun; //Queue of Action
    public NavMeshAgent agent;
    public bool onIdle; //If is TRUE then in case of Agent don't have a plan, stays on position. Otherwise (FALSE), goes around the world with random positions.

    //***OTHER PARAMS***
    public float rdmDistance = 100f; //Radius to get a random direction for navmesh agent. 
    public AgentStates states;

    public void Start()
    {
        SetActions();

        foreach (Goal g in goals)
        {
            //Add Goal_Actions (as Component) in case of hame some.
            if (g.hasAction == true)
            {
                Type action = g.goal_Action.GetType();
                if((gameObject.GetComponent(action.ToString()) as Action) == null) //In case of Goal-Action is actually attached to Agent
                    gameObject.AddComponent(action);
            }

            //Truncate data to dictionary.
            goals_list.Add(g.goal_precondition.key, g);
        }

        //Add Agent States
        if (states == null)
        {
            states = gameObject.AddComponent<AgentStates>();
        }
        ///

        agent = gameObject.AddComponent<NavMeshAgent>();
    }

    public void LateUpdate()
    {
        PerformPlanner();
    }

    public void PerformPlanner()
    {
        Dictionary<string, object> worldStates = new Dictionary<string, object>();

        foreach (string worldItem in states.worldElements)
        {
            worldStates.Add(worldItem, null);
        }
        foreach (string positionItem in states.positions)
        {
            worldStates.Add(positionItem, null);
        }
        foreach (KeyValuePair<string, float> inventoryItem in states.inventory)
        {
            worldStates.Add(inventoryItem.Key, inventoryItem.Value);
        }
        foreach (KeyValuePair<string, bool> statusItem in states.status)
        {
            worldStates.Add(statusItem.Key, statusItem.Value);
        }

        //LIMITED ACTIONS TO RUN IN THE PLANNER, HERE.!!!...
        //...

        //RUN PLANNER.
        if (planner == null || actionsToRun == null) //Acciones posibles | Estado ACTUAL del agente (es decir los recursos que posee AHORA MISMO el agente) | Metas
        {

            planner = new Planner();
            actionsToRun = planner.Plan(actions, worldStates, goals_list);

            if (agent.pathPending && actionsToRun != null) //Reset all navigation paths for prevent any destination bug.
            {
                agent.isStopped = true;
                agent.ResetPath(); 
            }
            else if (actionsToRun != null)
            {
                IdleState();
            }
        }

        if (actionsToRun != null) //Set the actual Action running.
        {
           if(actionsToRun.Count <= 1)
           {
               currentAction = actionsToRun.Peek();
               
                if(currentAction.finished == true) //In case of last action is finished, perform GOAL ACTION.
                {
                    if(currentGoal != null || !currentGoal.Equals("")) //In case of not empty goal
                    {
                        foreach(KeyValuePair<string, Goal> g in goals_list)
                        {
                            if(g.Key == currentGoal)
                            {
                                if(g.Value.hasAction == true) //In case of have some Action to perform
                                {
                                    Type goal_action = g.Value.goal_Action.GetType(); //Get the actual goal-action from Agent to perform
                                    currentAction = gameObject.GetComponent(goal_action.ToString()) as Action;
                                }
                                    
                            }
                        }
                    }
                }
           }
           else
           {
               //Check if action is finished or not.!!!!
               if(currentAction == null)
               { 
                   currentAction = actionsToRun.Dequeue();
               }
               else
               {
                   if(currentAction == currentAction.finished)
                       currentAction = actionsToRun.Dequeue();

               }

                Debug.Log("NAME / ISFINISHED:" + currentAction.actionName + " - " + currentAction.finished);
           }

            if (currentAction.finished != true) //Run the actual Action
                currentAction.running = true;  

            //Current Goal to achieve
            currentGoal = planner.goal_to_achieve;
        }
        else
        {
            actionsToRun = null;
            currentAction = null;
        }

        if(currentAction.running && currentAction != null && currentAction.finished != true) //In case of some Action is running.
        {
            currentAction.PerformAction();
        }
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
            //Play Idle animation...
            agent.isStopped = true;
            agent.ResetPath();
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

        randomDirection = UnityEngine.Random.insideUnitSphere * rdmDistance;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out hit, rdmDistance, 1))
        {
            destination = hit.position;
        }

        return destination;
    }

    

}

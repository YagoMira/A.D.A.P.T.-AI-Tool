using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner //: MonoBehaviour
{
    //CONSIDER: Action - NODE. Preconditions - EDGE.

    public const int MAX_PRIORITY = 100;
    public string goal_to_achieve;

    //Nodes for the graph
    public class Node
    {
        public Node parent;
        public int priority; //Float cost; - Cheapest plan.
        public Dictionary<string, object> state; //State
        public Action action; //Node's action is reference

        public Node (Node parent, int priority, Dictionary<string, object> state, Action action)
        {
            this.parent = parent;
            this.priority = priority;
            this.state = state;
            this.action = action;
        }
    }

    //Get a plan for a set of {Actions, Actual Resources, Goals} an actual Agent.
    //Sequence of actions can fulfill the goal.
    //public Queue<Action> Plan(List<Action> actions, Dictionary<string, object> states, Dictionary<string, Resource> goal)
    public Queue<Action> Plan(List<Action> actions, Dictionary<string, object> states, Dictionary<string, Agent.Goal> goal)
    {
        /*** PARAMETERS **/
        List<Action> usableActions = new List<Action>(); //For use clean actions without modifications.
        List<Action> result = new List<Action>(); //Find the cheapest - Highest priority nodes above the graph
        List<Node> leaves = new List<Node>(); //List of Leaf that satisfies the goal
        Node start, highest, n; //Starting node (No parent and Highest priority) / Highest node priority found (Equals Cheapest on Cost Queue) / N is used for get all the actions of nodes.
        bool sucess; //In case of find a solution (A PLAN!).
        Queue<Action> queue = new Queue<Action>(); //Actions agent can perform - List of actions to achieve the goal.


        start = new Node(null, MAX_PRIORITY, states, null); //Null because is the first - Highest priority - Resources of world - Not particular action for first node
        highest = null;
      
        /*** FUNCTIONS **/
        //Add actions parameter to new list "usableActions"
        foreach (Action a in actions)
        {
            //Check if can achieve goal by go to...
            if(a.CheckPreconditions() != false) //HERE check if can achieve World or Position Precontidionts!!!
            {
                usableActions.Add(a);
            }
        }

        sucess = BuildGraph(start, leaves, usableActions, goal); //This line should be there becase usableActions foreach!!!.

        //If a plan is not found. ||| Else el camino no existe
        if (!sucess)
        {
            Debug.Log("NO PLAN\n"); 
            return null;
        }

        //Get the highest priority Leaf
        foreach (Node leaf in leaves)
        {
            if (highest == null)
            {
                highest = leaf;
            }
            else
            {
                if (leaf.priority > highest.priority) //If leaf has more priority than actual highest priority node... ||| If vecino.G > (nodo.G + coste(nodo, vecino))
                    highest = leaf; // ||| Vecino.padre = nodo
            }
        }

        n = highest;
        while(n != null)
        {
            if(n.action != null) //Only Null is the starting node 
            {
                result.Insert(0, n.action); //Get the action of actual Highest priority node and store, after we'll get LIST WITH ACTIONS IN ORDER!!.
            }
            n = n.parent; //Set the parent node.
        }

        foreach(Action a in result)
        {
            queue.Enqueue(a); //In the end of queue...
        }

        //Add Goals as Actions in case of have some.
        //...

        Debug.Log("PLAN ::: " + " | Number of actions: " + queue.Count);
        foreach(Action a in queue)
        {
            Debug.Log("(PLANNER) Actions: " + a.actionName);
        }

        //PrintTree(start, actions, queue);

        return queue; //Agent with the plan. // ||| If encontrado retornar el camino
    }

    //Check if any path can assert some solution.
    private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, Agent.Goal> goal)
    {
        bool foundPath = false; //Bool equals true in case of find some solution.
        //Dictionary<string, object> currentState = new Dictionary<string, object>(parent.state);
        Dictionary<string, Resource> received_goals = new Dictionary<string, Resource>();

        foreach(KeyValuePair<string, Agent.Goal> g in goal)
        {
            if (g.Value.goal_precondition.selectedType.ToString() == ResourceType.WorldElement.ToString())
            {
                received_goals.Add(g.Value.goal_precondition.w_resource.resourceName, g.Value.goal_precondition.w_resource);
            }
            else if (g.Value.goal_precondition.selectedType.ToString() == ResourceType.Position.ToString())
            {
                received_goals.Add(g.Value.goal_precondition.p_resource.resourceName, g.Value.goal_precondition.p_resource);
            }
            else if (g.Value.goal_precondition.selectedType.ToString() == ResourceType.InventoryObject.ToString())
            {
                received_goals.Add(g.Value.goal_precondition.i_resource.resourceName, g.Value.goal_precondition.i_resource);
            }
            else if (g.Value.goal_precondition.selectedType.ToString() == ResourceType.Status.ToString())
            {
                received_goals.Add(g.Value.goal_precondition.s_resource.resourceName, g.Value.goal_precondition.s_resource);
            }
        }

        foreach (Action a in usableActions) // ||| While not encontrado NODO_FINAL or lista_abiertos.isEmpty()
        {
            //Compare the preconditions of actions with parent conditions.
            if (InState(a.preconditions, parent.state, false))
            {
                Dictionary<string, object> currentState = new Dictionary<string, object>();
                // Dictionary<string, object> currentState = applyEffects(parent.state, a.effects);
                foreach (KeyValuePair<string, Resource> effect in a.effects)
                    currentState.Add(effect.Key, effect.Value.value);

                //Node node = new Node(parent, parent.priority + a.CalculateTotalPriority(), currentState, a); //CHECK TOTALPRIORITY OF NODES!!!
                Node node = new Node(parent, ((a.totalPriority - a.totalCost) <= 0) ? 1 : a.totalPriority - a.totalCost, currentState, a); //Total Priority less TotalCost of Actions (get individual ones of preconditions and effects)

                if (InState(received_goals, currentState, true))
                {
                    //In case of find a solution...
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    //Iterate over the rest of list element in case of not find a solution.
                    List<Action> subset = IterateOver(usableActions, a); // ||| nodo = lista_abiertos.getNodoMinValue() #elimina el nodo de la lista
                    bool found = BuildGraph(node, leaves, subset, goal); 
                    if (found)
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }

    //Compare if items are in the 'state' dictionary.
    private bool InState(Dictionary<string, Resource> resources, Dictionary<string, object> states, bool receiveGoal) //ReceiveGoal: in case of receive goals as parameter.
    {
        bool match = false, allMatch = true;
        foreach (KeyValuePair<string, Resource> resource in resources)
        {
            foreach (KeyValuePair<string, object> state in states)
            {
                 if ((state.Key).Equals(resource.Key))
                 {
                    if ((resource.Value.resourceEnumType == ResourceType.WorldElement.ToString()) || (resource.Value.resourceEnumType == ResourceType.Position.ToString()))
                    {
                        match = true;

                        if(receiveGoal == true)
                            goal_to_achieve = resource.Key;

                        break;
                    }
                    else
                    {
                        if (state.Value.Equals(resource.Value.value) || ((resource.Value.resourceEnumType == ResourceType.InventoryObject.ToString()) && ((float)state.Value >= (float)resource.Value.value)))
                        {
                            match = true;

                            if (receiveGoal == true)
                                goal_to_achieve = resource.Key;

                            break;
                        }
                    }
                }
            }
        }

        if (!match)
            allMatch = false;

        return allMatch;
    }

    //Create a new list of actions over iterate it.
    private List<Action> IterateOver(List<Action> actions, Action removeMe)
    {
        List<Action> subset = new List<Action>();
        foreach (Action a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

    //Apply effects to the current preconditions for check if can achieve the goal.
    private Dictionary<string, object> applyEffects(Dictionary<string, object> currentState, Dictionary<string, Resource> effects)
    {
        Dictionary<string, object> local_state = new Dictionary<string, object>(); //For prevent any possible resource delete in original list.

        //Store in the local Dictionary for prevent possible deletes.
        foreach (KeyValuePair<string, object> c_state in currentState)
        {
            local_state.Add(c_state.Key, c_state.Value);
        }

        //Apply effects to actual states
        foreach (KeyValuePair<string, Resource> effect in effects)
        {
            //Check if the a current state exists, then update the value.
            bool exists = false;

            foreach (KeyValuePair<string, object> l_state in local_state)
            {
                //if (l_state.Equals(effect)) //!!!!!!!!
                if(l_state.Key.Equals(effect.Key))
                {
                    exists = true;
                    break;
                }
            }

            //In case of exists...Update.
            if (exists)
            {
                local_state[effect.Key] = effect.Value.value;
            }
            else //If the current State doesn't have the effect. Add it!.
            {
                local_state.Add(effect.Key, effect.Value); //!!!!!!!!
            }
        }

        return local_state;
    }


    private void PrintTree(Node start_node, List<Action> all_actions, Queue<Action> solution)
    {
      
        Debug.Log("<color=blue>*****TREE*****</color> ");
        Debug.Log("<color=red>PARENT: </color> " + "PARENT" + " PRIORITY: " + start_node.priority);
        foreach (Action a in all_actions)
        {
            if(solution.Contains(a)) //In case of Action is part of the solution
            {
                Debug.Log("<color=yellow>NODE:  " + a.actionName + "</color> PRIORITY: " + a.totalPriority);
            }
            else
            {
                Debug.Log("<color=green>NODE:  " + a.actionName + "</color> PRIORITY: " + a.totalPriority);
            }
        }
        Debug.Log("<color=blue>*****TREE*****</color> ");
    }
}

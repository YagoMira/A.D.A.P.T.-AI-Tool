using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planner //: MonoBehaviour
{
    //CONSIDER: Action - NODE. Preconditions - EDGE.

    public const int MAX_PRIORITY = 100;
    //Nodes for the graph
    public class Node
    {
        public Node parent;
        public int priority; //Float cost; - Cheapest plan.
        public Dictionary<string, Resource> resource; //State
        public Action action; //Node's action is reference

        public Node (Node parent, int priority, Dictionary<string, Resource> resource, Action action)
        {
            this.parent = parent;
            this.priority = priority;
            this.resource = resource;
            this.action = action;
        }
    }

    //Get a plan for a set of {Actions, Actual Resources, Goals} an actual Agent.
    //Sequence of actions can fulfill the goal.
    public Queue<Action> plan(List<Action> actions, Dictionary<string, Resource> resources, Dictionary<string, Resource> goal)
    {

        /*** PARAMETERS **/
        List<Action> usableActions = new List<Action>(); //For use clean actions without modifications.
        List<Action> result = new List<Action>(); //Find the cheapest - Highest priority nodes above the graph
        List<Node> leaves = new List<Node>(); //List of Leaf that satisfies the goal
        Node start, highest, n; //Starting node (No parent and Highest priority) / Highest node priority found (Equals Cheapest on Cost Queue) / N is used for get all the actions of nodes.
        bool sucess; //In case of find a solution (A PLAN!).

        start = new Node(null, 0, resources, null); //Null because is the first - Highest priority - Resources of world - Not particular action for first node
        highest = null;
      
        /*** FUNCTIONS **/
        //Add actions parameter to new list "usableActions"
        foreach (Action a in actions)
        {
            //If we can run the actions, by default: true.
            //Maybe should check if can achieve goal by go to...
            usableActions.Add(a);
        }

        sucess = BuildGraph(start, leaves, usableActions, goal); //Should be there becase usableActions foreach!!!.

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

        Queue<Action> queue = new Queue<Action>(); //Actions agent can perform - List of actions to achieve the goal.
        foreach(Action a in result)
        {
            queue.Enqueue(a); //In the end of queue... ::: List -> Queue.
        }

        Debug.Log("PLAN ::: " + " | Number of actions: " + queue.Count);
        foreach(Action a in queue)
        {
            Debug.Log("(PLANNER) Action: " + a.actionName);
        }

        return queue; //Agent with the plan. // ||| If encontrado retornar el camino
    }

    //Check if any path can assert some solution.
    private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, Resource> goal)
    {
        bool foundPath = false; //Bool equals true in case of find some solution.
        foreach (Action a in usableActions) // ||| While not encontrado NODO_FINAL or lista_abiertos.isEmpty()
        {
            //Compare the preconditions of actions with parent conditions.
            if (inState(a.preconditions, parent.resource))
            {
                //Debug.Log("TRUE!!!\n");

                Dictionary<string, Resource> currentState = new Dictionary<string, Resource>(parent.resource);
                //Here can apply the effects of the Action -> ...
                //CHECK THIS PART
                foreach (KeyValuePair<string, Resource> eff in a.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                    {
                        currentState.Add(eff.Key, eff.Value);
                    }
                }
                //***************

                Node node = new Node(parent, parent.priority + a.CalculateTotalPriority(), currentState, a);

                if (inState(goal, currentState))
                {
                    //In case of find a solution...
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    //Iterate over the rest of list element in case of not find a solution.
                    List<Action> subset = ActionSubset(usableActions, a); // ||| nodo = lista_abiertos.getNodoMinValue() #elimina el nodo de la lista
                    bool found = BuildGraph(node, leaves, subset, goal); 
                    if (found)
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }

    //Compare if items are in the 'state' ditionary.
    private bool inState(Dictionary<string, Resource> test, Dictionary<string, Resource> state)
    {
        bool allMatch = true;
        foreach (KeyValuePair<string, Resource> t in test)
        {
            bool match = false;
            foreach (KeyValuePair<string, Resource> s in state)
            {
                //Debug.Log("S" + s.Value.resourceName);
                //Debug.Log("T " + t.Value.resourceName);

                //if (s.Equals(t))
                if ((s.Key).Equals(t.Key))
                {
                    match = true;
                    break;
                }
            }
            if (!match)
                allMatch = false;
        }
        return allMatch;
    }

    //Create a new list of actions over iterate it.
    private List<Action> ActionSubset(List<Action> actions, Action removeMe)
    {
        List<Action> subset = new List<Action>();
        foreach (Action a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }
}

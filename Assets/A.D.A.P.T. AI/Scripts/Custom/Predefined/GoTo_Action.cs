using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Allows Agent move to determinate position.
//***Target = Assign target with Unity Inspector.***
public class GoTo_Action : Action
{
    string a_name = "GoTo";
    Agent agent;
    NavMeshAgent actual_agent;

    void Awake()
    {
        //Assign values across Script and not appear in the inspector
        /*
            KeyValuePair<string, Resource> GoTo_preconditions =
                new KeyValuePair<string, Resource>("isNear", new WorldResource("isNear", target, target, 5, 50.0f));
            KeyValuePair<string, Resource> GoTo_effects =
                new KeyValuePair<string, Resource>("onPosition", new StatusResource("onPosition", true, true, 5));
        */

        //Add preconditions and effects and NOT appears in inspector
        /*
            preconditions.Add(GoTo_preconditions.Key, GoTo_preconditions.Value);
            effects.Add(GoTo_effects.Key, GoTo_effects.Value);
        */

        //Assign values across Inspector and not appears in it.
        ResourceStruct GoTo_preconditions = new ResourceStruct("isNear", new WorldResource("isNear", target, target, 5, 50.0f));
        ResourceStruct GoTo_effects = new ResourceStruct("onPosition", new StatusResource("onPosition", true, true, 5));

        actionName = a_name;

        //Add preconditions and effects and appears in inspector
        preconditions_list.Add(GoTo_preconditions);
        effects_list.Add(GoTo_effects);

        //GetComponents
        agent = gameObject.GetComponent<Agent>();
    }

    private void Update()
    {
        /*
            agent.onIdle = false;
            if(finished != true) //While the Action is not finished
            {
                PerformAction();
            }
        */
    }

    public override void PerformAction()
    {
        //CHECK UPDATE-LATEUPDATE WITH AGENT CLASS

        actual_agent = gameObject.GetComponent<NavMeshAgent>();
        //Debug.Log("<color=red>LLAMA!</color>");

        if (target != null && hasTarget) //TARGET EXISTS
        {
            actual_agent.stoppingDistance = stopDistance;
            actual_agent.SetDestination(target.transform.position);
        }

        //Check if Agent NavMesh reach the actual target position
        if (!actual_agent.pathPending && target != null)
        {
            if (actual_agent.remainingDistance <= actual_agent.stoppingDistance)
            {
                if (!actual_agent.hasPath || actual_agent.velocity.sqrMagnitude == 0f)
                {
                    //Debug.Log("COMPLETED\n");
                    //running = false;"
                    //Debug.Log("<color=red>LLAMA!-1 </color> D1: " + actual_agent.remainingDistance + " D2: " + actual_agent.stoppingDistance);
                    finished = true;

                }
            }
            else
            {
                //Debug.Log("NO COMPLETED\n");
                //running = true;
                //Debug.Log("<color=red>LLAMA!-2</color>");
                finished = false;
            }
        }
    }
}

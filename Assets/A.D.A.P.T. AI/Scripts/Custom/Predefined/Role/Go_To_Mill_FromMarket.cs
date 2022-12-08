using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Allows Agent move to determinate position.
//***Target = Assign target with Unity Inspector.***
public class Go_To_Mill_FromMarket : Action
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
        actionName = a_name;

        //Add preconditions and effects and appears in inspector
        //preconditions_list.Add(GoTo_preconditions);
        //effects_list.Add(GoTo_effects);

        //GetComponents
        agent = gameObject.GetComponent<Agent>();

        //WARNING MESSAGE!.
        Debug.Log("<color=blue> Action: </color>" + actionName + "<color=blue> has preconditions/effects added by code,</color> <color=red> DON'T ADD MORE VIA INSPECTOR!.</color>");
    }

    private void Update() { }

    public override void PerformAction()
    {
        agent.agent_states.ModifyStatusItem("inMill", false); //Initialices the state in case of enter in a loop.

        actual_agent = gameObject.GetComponent<NavMeshAgent>();

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
                    agent.agent_states.ModifyStatusItem("inMill", true);
                    agent.agent_states.ModifyStatusItem("onPosition", false);
                    finished = true;
                    
                }
            }
            else
            {
                finished = false;
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Allows Agent move to determinate position.
//***Target = Assign target with Unity Inspector.***
public class GoTo_Action : Action
{
    string a_name = "GoTo";

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
    }

    private void Update()
    {
        Agent agent = gameObject.GetComponent<Agent>();
        agent.onIdle = false;
        Perform();
    }

    void Perform()
    {
        NavMeshAgent actual_agent;
        actual_agent = gameObject.GetComponent<NavMeshAgent>();

        if(gameObject.transform.position != target.transform.position)
        {
            actual_agent.SetDestination(target.transform.position);
            Debug.Log("NO COMPLETED\n");
        }
        else
        {
            Debug.Log("COMPLETED\n");
        }

    }
}

using UnityEngine;
using UnityEngine.AI;
public class GoToWindmill : Action
{
    string a_name = "GoToWindmill";
    Agent agent;
    NavMeshAgent actual_agent;

    void Awake()
    {
        /******DON'T DELETE THIS LINES!!!******/
        actionName = a_name;
        agent = gameObject.GetComponent<Agent>();
        //WARNING MESSAGE!
        //Debug.Log(" <color=blue> Action: </color> " + actionName + " <color=blue> has preconditions / effects added by code,</ color > <color=red> DON'T ADD MORE VIA INSPECTOR!.</color>");
        /************/

        /************/
        //HERE YOU CAN ADD YOUR PRECONDITIONS // EFFECTS
        /************/
        //In case of add preconditions/effects, uncomment the next lines:
        //preconditions_list.Add(ResourceStruct);
        //effects_list.Add(ResourceStruct);
    }

    public override void PerformAction()
    {
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
                    agent.agent_states.ModifyStatusItem("inWMill", true);
                    agent.agent_states.ModifyStatusItem("onPosition", false);
                    global_states.DecreaseInventoryItem("wheat", 100);
                    Debug.Log("WHEAT DECREASED TO: " + global_states.inventory["wheat"]);
                    global_states.IncreaseInventoryItem("depositedWheat", 100);
                    Debug.Log("WHEAT DEPOSITED QUANTITY:" + global_states.inventory["depositedWheat"]);

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

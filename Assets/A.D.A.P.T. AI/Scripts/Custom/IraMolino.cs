using UnityEngine;
using UnityEngine.AI;
public class IraMolino : Action
{
    string a_name = "IraMolino";
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
                    global_states.DecreaseInventoryItem("trigo", 100);
                    global_states.IncreaseInventoryItem("trigoDepositado", 100);
                    Debug.Log("CANTIDAD DE TRIGO RECOGIDA:" + global_states.inventory["trigo"]);
                    Debug.Log("TRIGO DEPOSITADO! - CANTIDAD FINAL:" + global_states.inventory["trigoDepositado"]);
                    
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

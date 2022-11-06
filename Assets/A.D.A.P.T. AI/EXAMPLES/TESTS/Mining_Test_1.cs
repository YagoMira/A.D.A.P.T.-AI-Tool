using UnityEngine;
using UnityEngine.AI;
public class Mining_Test_1 : Action
{
    string a_name = "ActionName";
    Agent agent;
    NavMeshAgent actual_agent;
    float minedQuantity = 10f;

    void Awake()
    {
        /******DON'T DELETE THIS LINES!!!******/
        actionName = a_name;
        agent = gameObject.GetComponent<Agent>();
        //WARNING MESSAGE!
        Debug.Log(" <color=blue> Action: </color> " + actionName + " <color=blue> has preconditions / effects added by code,</color> <color=red> DON'T ADD MORE VIA INSPECTOR!.</color>");
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
        //Uncomment next line if you need some navmesh:
        //actual_agent = gameObject.GetComponent<NavMeshAgent>();
        //Use 'finished = true;' when finish the action.
        agent.agent_states.IncreaseInventoryItem("minedGold", minedQuantity);
        Debug.Log("MINED ORE:" + agent.agent_states.inventory["minedGold"]);
        agent.agent_states.IncreaseInventoryItem("minedGold2", minedQuantity);
        Debug.Log("MINED ORE - 2:" + agent.agent_states.inventory["minedGold2"]);
        finished = true;
    }

}

using UnityEngine;
using UnityEngine.AI;
public class DepositBread : Action
{
    string a_name = "DepositBread";
    Agent agent;
    NavMeshAgent actual_agent;

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

        global_states.ModifyInventoryItem("bread", 0.0f);
        global_states.ModifyInventoryItem("depositedBread", 2.0f);

        finished = true;
    }

}

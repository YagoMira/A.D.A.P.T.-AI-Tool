using UnityEngine;
using UnityEngine.AI;
public class RecogerTrigo : Action 
{
    string a_name = "RecogerTrigo";
    Agent agent;
    NavMeshAgent actual_agent;
    float trigoObtenido = 10f;
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
        global_states.IncreaseInventoryItem("trigo", trigoObtenido);
        Debug.Log("TRIGO:" + global_states.inventory["trigo"]);
        finished = true;
    }

}

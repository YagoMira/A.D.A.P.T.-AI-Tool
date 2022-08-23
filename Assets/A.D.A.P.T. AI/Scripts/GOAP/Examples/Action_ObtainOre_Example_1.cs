using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ObtainOre_Example_1 : Action
{
    float minedQuantity = 10f;

    public override void PerformAction()
    {
        global_states.IncreaseInventoryItem("minedGold", minedQuantity);
        Debug.Log("MINED ORE:" + global_states.inventory["minedGold"]);
        finished = true;
    }
}

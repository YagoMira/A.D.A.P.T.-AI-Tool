using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner_1_Example : Agent
{

    new void Start() //DON'T MODIFY ANY LINE OF THIS FUNCTION!!!
    {
        AddGoals();
        /************/
        base.Start(); //DON'T DELETE THIS LINE!!!
        /************/
        ManageStates();
    }

    public void AddGoals() //Allows to add desired goals via code.
    {
        InventoryResource goal_2_resource = (new InventoryResource("minedGold", 100.0f, 30, 500, false));
        goals.Add(new Goal(goal_2_resource, false));
    }

    public void ManageStates()
    {
        global_states.AddInventoryItem("minedGold", 0f); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
        //agent_states.AddStatusItem("Example_1", false); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
    }

}

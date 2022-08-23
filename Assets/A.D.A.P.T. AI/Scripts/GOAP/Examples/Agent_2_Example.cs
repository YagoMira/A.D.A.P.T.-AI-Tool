using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_2_Example : Agent
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
        //StatusResource goal_1_resource = (new StatusResource("onPosition", true, true, 5));
        //goals.Add(new Goal(goal_1_resource, true, new Action_Goal_1()));
        InventoryResource goal_2_resource = (new InventoryResource("mine", 5.0f, 5.0f, 5, 100, false));
        goals.Add(new Goal(goal_2_resource, false));

    }

    public void ManageStates()
    {
        agent_states.AddWorldItem("isNear");
        //agent_states.AddInventoryItem("I-1", 0f);
        agent_states.AddInventoryItem("I-2", 0f);
        agent_states.AddInventoryItem("mine", 0f);
        //states.ModifyInventoryItem("I-1", 15f); //CHECK !!!
        global_states.AddInventoryItem("ORO", 0f); //Only add initializate in one class.
    }

}

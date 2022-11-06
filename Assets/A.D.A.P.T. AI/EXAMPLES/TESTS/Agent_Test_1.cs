using UnityEngine;
public class Agent_Test_1 : Agent
{
    new void Start() //DON'T MODIFY ANY LINE OF THIS FUNCTION!!!
    {
        AddGoals();
        /************/
        base.Start(); //DON'T DELETE THIS LINE!!!
        /************/
        ManageStates();
    }

    public void AddGoals() {
        InventoryResource goal_1_resource = (new InventoryResource("minedGold", 105.0f, 15, 106, false));
        InventoryResource goal_2_resource = (new InventoryResource("minedGold2", 105.0f, 45, 106, false));
       
        goals.Add(new Goal(goal_1_resource, false));
        goals.Add(new Goal(goal_2_resource, false));
    }

    public void ManageStates() {

        agent_states.AddInventoryItem("minedGold", 0);
        agent_states.AddInventoryItem("minedGold2", 0);
       

        //Debug.Log("GOALS? " + goals.Count);
    }
}

using UnityEngine;
public class Example_Agent_Test_2 : Agent
{
    new void Start() //DON'T MODIFY ANY LINE OF THIS FUNCTION!!!
    {
        AddGoals();
        /************/
        base.Start(); //DON'T DELETE THIS LINE!!!
                      /************/
        ManageStates();
    }

    public void AddGoals()
    {
        //If you want to add some goals can use the next lines (an change the ResourceType):
        //InventoryResource goal_1 = (new InventoryResource("minedGold", 105.0f, 15, 106, false));
        //goals.Add(new Goal(goal_1, false));

    }

    public void ManageStates()
    {
        //If you want to add some states can use the next lines:
        //agent_states.AddInventoryItem("Name", 0); /*For local agent states*/
        //global_states.AddInventoryItem("Name", 0); /*For global agent states*/

    }
}

using UnityEngine;
public class Baker : Agent
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
        InventoryResource Bread = (new InventoryResource("bread", 2.0f, 30, 500, false));
        InventoryResource GoToMarket = (new InventoryResource("depositedBread", 2.0f, 30, 500, false));

        goals.Add(new Goal(Bread, false));
        goals.Add(new Goal(GoToMarket, false));
    }

    public void ManageStates()
    {
        agent_states.AddStatusItem("inMarket", false);
        global_states.AddInventoryItem("bread", 0f);
        global_states.AddInventoryItem("depositedWheat", 0f);
        agent_states.AddStatusItem("onPosition", false);
    }
}

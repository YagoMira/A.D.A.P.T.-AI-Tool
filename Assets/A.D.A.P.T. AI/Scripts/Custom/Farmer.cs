using UnityEngine;
public class Farmer : Agent
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
        InventoryResource obtainWheat = (new InventoryResource("wheat", 100.0f, 30, 500, false));
        StatusResource putWheat = (new StatusResource("inWMill", true, 50));
        goals.Add(new Goal(obtainWheat, false));
        goals.Add(new Goal(putWheat, false));
    }

    public void ManageStates()
    {

        agent_states.AddStatusItem("onPosition", false);
        global_states.AddInventoryItem("wheat", 0f); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
        agent_states.AddStatusItem("inWMill", false);
    }
}

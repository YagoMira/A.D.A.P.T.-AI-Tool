using UnityEngine;
public class Marketer : Agent
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
        StatusResource SellBread = (new StatusResource("breadToSell", true, 30));
        goals.Add(new Goal(SellBread, false));
    }

    public void ManageStates() {
        global_states.AddInventoryItem("depositedBread", 0f);
    }
}

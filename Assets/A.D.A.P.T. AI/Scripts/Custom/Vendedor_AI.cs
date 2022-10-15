using UnityEngine;
public class Vendedor_AI : Agent
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
        StatusResource VenderPan = (new StatusResource("panEnVenta", true, 50));
        //InventoryResource Pan = (new InventoryResource("pan", 2.0f, 30, 500, false));
        goals.Add(new Goal(VenderPan, false));
    }

    public void ManageStates() {
        global_states.AddInventoryItem("panAVender", 0f);
    }
}

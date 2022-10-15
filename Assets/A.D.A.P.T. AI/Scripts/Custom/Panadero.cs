using UnityEngine;
public class Panadero : Agent
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
        StatusResource IrAMercado = (new StatusResource("panDepositado", true, 50));
        //InventoryResource Pan = (new InventoryResource("pan", 2.0f, 30, 500, false));
        goals.Add(new Goal(IrAMercado, false));
        //goals.Add(new Goal(Pan, false));
    }

    public void ManageStates()
    {
        global_states.AddInventoryItem("pan", 0f);
        global_states.AddInventoryItem("trigoDepositado", 0f);
        global_states.AddInventoryItem("minedGold", 0f);
        //global_states.AddInventoryItem("panDepositado", 0f);
    }
}

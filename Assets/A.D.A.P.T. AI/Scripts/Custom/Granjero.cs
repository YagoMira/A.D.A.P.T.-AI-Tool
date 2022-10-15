using UnityEngine;
public class Granjero : Agent
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
        InventoryResource ObtenerTrigo = (new InventoryResource("trigo", 100.0f, 30, 500, false));
        StatusResource LlevarTrigo = (new StatusResource("onMolino", true, 50));
        goals.Add(new Goal(ObtenerTrigo, false));
        goals.Add(new Goal(LlevarTrigo, false));
        //InventoryResource Sobrevivir = (new InventoryResource("trigo", 100.0f, 30, 500, false));
        //goals.Add(new Goal(Sobrevivir, false));
    }

    public void ManageStates() {

        global_states.AddInventoryItem("trigo", 0f); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
        
        //agent_states.AddWorldItem("trigoCerca"); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
        //agent_states.AddStatusItem("enTrigo", false); //DON'T ADD LOCAL AND GLOBAL STATE WITH SAME NAME!!!!!!!!!!!!!!!
    }
}

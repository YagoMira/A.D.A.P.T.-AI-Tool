using UnityEngine;
public class Enemy_AI_Demo : Agent
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

        StatusResource GoToPlayer = (new StatusResource("onPosition", true, 50));
        InventoryResource KillPlayer = (new InventoryResource("health", 0.0f, 30, 101, false));
        goals.Add(new Goal(GoToPlayer, false));
        goals.Add(new Goal(KillPlayer, false));
    }

    public void ManageStates()
    {
        //If you want to add some states can use the next lines:
        //agent_states.AddInventoryItem("Name", 0); /*For local agent states*/
        //global_states.AddInventoryItem("Name", 0); /*For global agent states*/

        global_states.AddInventoryItem("health", 100f); //This is only a example, you can create a variable of the player and add it as a global state to the agents.

        agent_states.AddStatusItem("inPlace", false);
        agent_states.AddStatusItem("onPosition", false);
        
        
    }

}

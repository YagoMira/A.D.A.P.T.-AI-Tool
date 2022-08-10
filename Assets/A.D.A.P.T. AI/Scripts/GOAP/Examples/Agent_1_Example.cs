using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_1_Example : Agent
{
    // Start is called before the first frame update
    void Awake()
    {
        //goals_list.Add(new StatusResource("isPoisoned", true, true, 5));
        //WORLD/POSITION: goals_list.Add(new StatusResource("onPosition", true, true, 5));
        goals_list.Add(new InventoryResource("mine", 5.0f, 5.0f, 5, 100, false));
    }

    new void Start()
    {
        /************/
        base.Start(); //DON'T DELETE THIS LINE!!!
        /************/
        ManageStates();
    }

    public void ManageStates()
    {
        states.AddWorldItem("isNear");
        states.AddInventoryItem("I-1", 0f);
        states.AddInventoryItem("I-2", 0f);
        states.AddInventoryItem("mine", 0f);
        states.ModifyInventoryItem("I-1", 15f);
    }

    public void LateUpdate()
    {
        DebugPlanner();
    }

    public void DebugPlanner()
    {
        //Dictionary<string, Resource> test = new Dictionary<string, Resource>();
        Dictionary<string, object> worldStates = new Dictionary<string, object>();

        foreach (string worldItem in states.worldElements)
        {
            worldStates.Add(worldItem, null);

        }

        foreach (string positionItem in states.positions)
        {
            worldStates.Add(positionItem, null);

        }
        foreach (KeyValuePair<string, float> inventoryItem in states.inventory)
        {
            worldStates.Add(inventoryItem.Key, inventoryItem.Value);
            
        }
        foreach(KeyValuePair<string, bool> statusItem in states.status)
        {
            worldStates.Add(statusItem.Key, statusItem.Value);
        }
        //WORLD/POSITION: test.Add("isNear", new WorldResource("isNear", actions[0].target, actions[0].target, 5, 50.0f));
        //STATES::::
        //test.Add("I-1", new InventoryResource("I-1", 5.0f, 5.0f, 5, 100, false));

        planner = new Planner();
        if (planner.Plan(actions, worldStates, goals) == null) //Acciones posibles | Estado ACTUAL del agente (es decir los recursos que posee AHORA MISMO el agente) | Metas
        {
            IdleState();
        }

    }

    //////
    /*
     * -COMPROBAR QUE LAS ACTION/RESOURCES FUNCIONAN PARA: World/Position Y Status.
     * -AÑADIR DEBUGPLANNER A ACTION.
     * -PODER AÑADIR GOALS POR INTERFAZ.
     * -CONTROLAR .PLAN NULL O NO Y CURRENT ACTION (ACTION FINIHED REMOVED, ...).
     * 
    */
}

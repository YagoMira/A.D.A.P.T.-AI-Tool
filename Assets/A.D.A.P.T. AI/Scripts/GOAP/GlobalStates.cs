using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStates //Unique instance for all game world, when the agents can store, modify or add states.
{
    private static readonly GlobalStates global = new GlobalStates();
    private static AgentStates states;

    static GlobalStates()
    {
        //Create global states instance
        states = new AgentStates();
    }

    private GlobalStates() { }

    public static GlobalStates GetGlobalStatesInstance
    {

        get { return global; }
    }

    public AgentStates GetGlobalStates()
    {

        return states;
    }
}

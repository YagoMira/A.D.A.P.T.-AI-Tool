﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetOre_Example1 : Action
{

    Agent agent;
    AgentStates states;
    int mine = 1;

    void Awake()
    {
        agent = gameObject.GetComponent<Agent>();
        
    }

    void LateUpdate()
    {
        states = agent.states;
        //Mining(); 
    }

    void Mining()
    {
        if(states.inventory["mine"] < ((float)effects["mine"].value))
        {
            states.ModifyInventoryItem("mine", mine++);
            Debug.Log("Mine: " + states.inventory["mine"]);
        }
        else
        {
            Debug.Log("A: " + states.inventory["mine"] + " B: " + (float)effects["mine"].value);
        }
            
    }

    public override void PerformAction() { }
}

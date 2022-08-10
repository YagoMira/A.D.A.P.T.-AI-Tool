using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GetOre_Example1 : Action
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        effects.Add("isMining", new Resource("isMining", ResourceType.Status, false, 1, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
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
}

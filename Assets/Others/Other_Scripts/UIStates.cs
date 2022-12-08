using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStates : MonoBehaviour
{
    public Text health_Text;
    public GameObject dead_Text_Object;
    AgentStates globalStates =  GlobalStates.GetGlobalStatesInstance.GetGlobalStates(); //Get the global states shared between agents.

    //Example of how to get the global states to show it into the UI.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health_Text.text = globalStates.inventory["health"].ToString();

        if (globalStates.inventory["health"] <= 0)
            dead_Text_Object.SetActive(true);
    }
}

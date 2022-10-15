using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleText : MonoBehaviour
{
    protected AgentStates global_states = GlobalStates.GetGlobalStatesInstance.GetGlobalStates();
    public Text texto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = global_states.inventory["trigo"].ToString();
    }
}

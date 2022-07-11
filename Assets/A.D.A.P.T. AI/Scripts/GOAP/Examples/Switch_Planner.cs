using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Switch_Planner : MonoBehaviour
{

    public Text text;
    public Text modifyCurrentAction;
    public Text actionEffectText;

    public GameObject switchObject;

    public Agent_Example_1 agent;
    // Start is called before the first frame update
    void Start() {
        //agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        agent = other.GetComponent<Agent_Example_1>();
        agent.SetCurrentAction(agent.actions[1]);
        text.text = agent.goToTarget(agent.actions[0]).ToString();
        modifyCurrentAction.text = agent.GetCurrentAction().actionName;
        actionEffectText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}

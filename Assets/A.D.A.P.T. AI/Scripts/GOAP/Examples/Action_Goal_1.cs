using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Goal_1 : Action
{
    string a_name = "GOALACTION";
    Agent agent;


    void Awake()
    {
        actionName = a_name;

        //GetComponents
        agent = gameObject.GetComponent<Agent>();
    }

    private void Update()
    {
        /*if (finished != true) //While the Action is not finished
        {
            PerformAction();
        }*/
    }

    public override void PerformAction()
    {
        Debug.Log("LET'S, GOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //gameObject.SetActive(false);
        finished = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Agent_Example_1 : Agent
{
    public Text currentGoalText;
    public Text currentActionText;
    public Text goalsText;
    public Text actionsText;

    new void Start() //Create new Start and call Action class start
    {
        base.Start(); //Call main class
        goals.Add("AchieveOre", 5);
        goals.Add("ExampleGoal_2", 1);

        SimulatePlanner();
    }

    void SimulatePlanner() //Function to simulate the Goap_Planner to select a goal
    {
        goalsText.text = "";
        actionsText.text = "";

        if (currentGoal == null)
        {
            Debug.Log("Entra");
            foreach (KeyValuePair<string, int> pGoal in goals)
            {
                Debug.Log("All posible goals:" + pGoal.Key);
                goalsText.text += ("\n" + pGoal.Key);
            }

            foreach (Action a in actions)
            {
                Debug.Log("All posible Actions:" + a.actionName);
                actionsText.text += ("\n" + a.actionName);
            }

            //Simulate the planner get the most achievable
            SetCurrentGoal(goals.ElementAt(0).Key);
            currentAction = actions[0];
            //Debug.Log("Current GOAL" + currentGoal);
            currentActionText.text = currentAction.actionName;
            currentGoalText.text = currentGoal;
        }

    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_1_Example : Agent
{
    // Start is called before the first frame update
    void Awake()
    {
        //goals_list.Add(new StatusResource("isPoisoned", true, true, 5));
        goals_list.Add(new StatusResource("onPosition", true, true, 5));
    }


}

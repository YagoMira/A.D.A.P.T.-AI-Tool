using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GoToOre_Example1 : Action
{
    // Start is called before the first frame update
    void Start()
    {
        //Added preconditions and effects by code, by should added by editor.
        preconditions.Add("canMine", new Resource("canMine", ResourceType.WorldElement, false, 1, 1.0f, 1000.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

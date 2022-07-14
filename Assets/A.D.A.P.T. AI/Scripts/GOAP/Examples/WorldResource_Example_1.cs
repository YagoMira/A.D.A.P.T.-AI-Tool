using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldResource_Example_1 : MonoBehaviour
{
    void Start()
    {
        string name = "Example1";
        ResourceType resourceType = ResourceType.WorldElement;
        GameObject type = null;
        GameObject value = null;
        int priority = 1;
        WorldResource worldResource_Example_1 = new WorldResource(name, resourceType, type, value, priority);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class WorldResource: Resource
{

    [SerializeField]
    public GameObject resource_value;

    public WorldResource() { }

    public WorldResource(string name, GameObject type, GameObject value, int priority, float limit)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.WorldElement.ToString();
        this.type = type;
        this.value = value;
        this.resource_value = value;
        this.priority = priority;
        this.limit = limit;
    }

    public void ModifyValue(GameObject newValue) //Allows to change the actual value of the Resource as any Basic Data Type
    {
        this.value = newValue;
    }

}

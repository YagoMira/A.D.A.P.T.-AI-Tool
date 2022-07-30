using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldResource: Resource
{
    
    public WorldResource() { }

    public WorldResource(string name, GameObject type, GameObject value, int priority)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.WorldElement.ToString();
        this.type = type;
        this.value = value;
        this.priority = priority;
    }

}

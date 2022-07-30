using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StatusResource : Resource
{
    public StatusResource() { }

    public StatusResource(string name, bool type, bool value, int priority)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.Status.ToString();
        this.type = type;
        this.value = value;
        this.priority = priority;
    }

}

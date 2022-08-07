using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StatusResource : Resource
{
    [SerializeField]
    public bool resource_value;

    public StatusResource() { }

    public StatusResource(string name, bool type, bool value, int priority)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.Status.ToString();
        this.type = type;
        this.value = value;
        this.priority = priority;
        this.limit = 0.0f;
    }

    public void ModifyValue(bool newValue) //Allows to change the actual value of the Resource as any Basic Data Type
    {
        this.value = newValue;
    }

}

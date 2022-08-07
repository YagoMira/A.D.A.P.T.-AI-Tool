using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionResource : Resource
{

    [SerializeField]
    public Transform resource_value;

    public PositionResource() { }

    public PositionResource(string name, Transform type, Transform value, int priority, float limit)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.Position.ToString();
        this.type = type;
        this.value = value;
        this.priority = priority;
        this.limit = limit;
    }

    public void ModifyValue(Transform newValue) //Allows to change the actual value of the Resource as any Basic Data Type
    {
        this.value = newValue;
    }

}

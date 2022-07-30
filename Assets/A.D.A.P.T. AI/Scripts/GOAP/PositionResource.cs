using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionResource : Resource
{

    [SerializeField]
    public float limit;

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

}

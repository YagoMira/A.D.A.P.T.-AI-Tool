using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InventoryResource : Resource
{
    [SerializeField]
    public float limit;
    [SerializeField]
    public bool isConsumable;

    public InventoryResource() { }

    public InventoryResource(string name, float type, float value, int priority, float limit, bool isConsumable)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.InventoryObject.ToString();
        this.type = type;
        this.value = value;
        this.priority = priority;
        this.limit = limit;
        this.isConsumable = isConsumable;
    }


}

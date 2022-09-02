using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InventoryResource : Resource
{
    [SerializeField]
    public float resource_value;

    [SerializeField]
    public bool isConsumable;

    public InventoryResource() { }

    public InventoryResource(string name, float value, int priority, float limit, bool isConsumable)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.InventoryObject.ToString();
        this.type = value.GetType();
        this.value = value;
        this.resource_value = value;
        this.priority = priority;
        this.limit = limit;
        this.isConsumable = isConsumable;
    }

    public void ModifyValue(float newValue) //Allows to change the actual value of the Resource as any Basic Data Type
    {
        this.value = newValue;
    }



}

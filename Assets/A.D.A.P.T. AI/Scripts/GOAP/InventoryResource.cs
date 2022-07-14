using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryResource : IResource
{
    public string resourceName
    {
        get;
        private set;
    }
    public ResourceType resourceType
    {
        get;
        private set;
    }
    public object type
    {
        get;
        private set;
    }
    public object value
    {
        get;
        private set;
    }
    public int priority
    {
        get;
        private set;
    }
    public float limit
    {
        get;
        private set;
    }
    public bool isConsumable
    {
        get;
        private set;
    }


    public InventoryResource(string name, ResourceType resourceType, float type, float value, int priority, float limit, bool isConsumable)
    {
        this.resourceName = name;
        this.resourceType = resourceType;
        this.type = type;
        this.value = value;
        this.priority = priority;
        this.limit = limit;
        this.isConsumable = isConsumable;
    }

    /*--FUNCTIONS--*/
    public new object GetType()
    {
        return type;
    }

    public ResourceType GetResourceType()
    {
        return resourceType;
    }

    public void ModifyValue(object newValue)
    {
        value = newValue;
    }



}

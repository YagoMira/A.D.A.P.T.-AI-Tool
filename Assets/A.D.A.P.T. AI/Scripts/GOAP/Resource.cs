using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : IResource
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
    public int priority
    {
        get;
        private set;
    }
    public bool isConsumable
    {
        get;
        private set;
    }
    public float value
    {
        get;
        private set;
    }
    public float limit
    {
        get;
        private set;
    }

    public ResourceType GetResourceType()
    {
        return resourceType;
    }

    public Resource(string name, ResourceType resourceType, bool isConsumable, int priority, float value, float limit)
    {
        this.resourceName = name;
        this.resourceType = resourceType;
        this.isConsumable = isConsumable;
        this.priority = priority;
        this.value = value;
        this.limit = limit;
    }

    /*--FUNCTIONS FOR CONSUMABLE RESOURCES--*/
    public void ModifyValue(float newValue)
    {
        value = newValue;
    }


}

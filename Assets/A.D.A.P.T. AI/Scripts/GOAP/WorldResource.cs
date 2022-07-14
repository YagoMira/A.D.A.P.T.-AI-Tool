using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldResource : IResource
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


    public WorldResource(string name, ResourceType resourceType, GameObject type, GameObject value, int priority)
    {
        this.resourceName = name;
        this.resourceType = resourceType;
        this.type = type;
        this.value = value;
        this.priority = priority;
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

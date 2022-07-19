using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusResource : IResource
{
    [SerializeField]
    private string resourceName;
    public string ResourceName
    {
        get
        {
            return this.resourceName;
        }
        set
        {
            this.resourceName = value;
        }
    }

    [SerializeField]
    private ResourceType resourceType;
    public ResourceType ResourceType
    {
        get
        {
            return this.resourceType;
        }
        set
        {
            this.resourceType = value;
        }
    }

    [SerializeField]
    private object type;
    public object Type
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
        }
    }

    [SerializeField]
    private object value;
    public object Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }

    [SerializeField]
    private int priority;
    public int Priority
    {
        get
        {
            return this.priority;
        }
        set
        {
            this.priority = value;
        }
    }



    public StatusResource(string name, ResourceType resourceType, bool type, bool value, int priority)
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

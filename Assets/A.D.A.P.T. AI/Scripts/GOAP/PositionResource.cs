﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResource : IResource
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

    [SerializeField]
    private float limit;
    public float Limit
    {
        get
        {
            return this.limit;
        }
        set
        {
            this.limit = value;
        }

    }

    public PositionResource(string name, ResourceType resourceType, Transform type, Transform value, int priority, float limit)
    {
        this.resourceName = name;
        this.resourceType = resourceType;
        this.type = type;
        this.value = value;
        this.priority = priority;
        this.limit = limit;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InventoryResource : IResource
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

    [ReadOnly]
    [SerializeField]
    private string resourceEnumType;
    public string ResourceEnumType
    {
        get
        {
            return this.resourceEnumType;
        }
        set
        {
            this.resourceEnumType = value;
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

    [SerializeField]
    private bool isConsumable;
    public bool IsConsumable
    {
        get
        {
            return this.isConsumable;
        }
        set
        {
            this.isConsumable = value;
        }
    }


    public InventoryResource(string name, ResourceType resourceType, float type, float value, int priority, float limit, bool isConsumable)
    {
        this.resourceName = name;
        this.resourceEnumType = ResourceType.InventoryObject.ToString();
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

    public string GetResourceType()
    {
        return resourceEnumType;
    }

    public void ModifyValue(object newValue)
    {
        value = newValue;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType
{
    WorldElement, //Element in the world is needed. Ex.: Fire.
    Position, //Some transform position where the agent/actions goes to.
    InventoryObject, //Some inventory object consumable or not.
    Status //Does reference to some status result from execute and action. Ex. : GoSwim -> GetWet == True
}

public abstract class Resource
{

    //VARIABLES:
    //[ReadOnly]
    public string resourceName; //Name of the resource
    [ReadOnly]
    public string resourceEnumType; //Type of resource (ENUM)
    public object type; //Type of resource as Basic Data Type
    public object value; //Value of resource as Basic Data Type
    public int priority; //Priority of the precondition/after effect
    [ReadOnly]
    public int cost; //Cost of the actual Resource, implies increase or decrease the actual priority in relation of distances, number of inventory elements, ...
    public float limit; //Maximum distance to target or limit capacity into inventory

    //METHODS:
    public object GetResourceType() //Returns type of the current resource as Basic Data Type
    {
        return value.GetType();
    }
    public void SetResourceType(ResourceType resourceType) //Returns type of the current resource
    {
        resourceEnumType = resourceType.ToString();
    }
    public string GetEnumResourceType() //Returns type of the current resource
    {
        return resourceEnumType;
    }
    public void ModifyValue(object newValue) //Allows to change the actual value of the Resource as any Basic Data Type
    {
        this.value = newValue;
    }
}

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

public interface IResource
{

    //VARIABLES:
    string ResourceName { get; } //Name of the resource
    string ResourceEnumType { get; } //Type of resource (ENUM)
    object Type { get; } //Type of resource as Basic Data Type
    object Value { get; } //Value of resource as Basic Data Type
    int Priority { get; } //Priority of the precondition/after effect


    //METHODS:
    object GetType(); //Returns type of the current resource as Basic Data Type
    string GetResourceType(); //Returns type of the current resource
    void ModifyValue(object newValue); //Allows to change the actual value of the Resource as any Basic Data Type
}

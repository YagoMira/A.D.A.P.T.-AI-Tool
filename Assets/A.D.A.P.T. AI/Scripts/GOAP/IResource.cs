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
    string resourceName { get; } //Name of the resource
    ResourceType resourceType { get; } //Type of resource
    bool isConsumable { get; } //Check if the current resource is consumable or not
    int priority { get; } //Priority of the precondition/after effect
    float value { get; } //We'll use value variable for some situations: [0.0 or 1.0] if WorldElement/Position exists and [0.0 - Inventory_Limit] 
    float limit { get; } //Limit for inventory objects or for reach some transform position

    //METHODS:
    ResourceType GetResourceType(); //Returns type of the current source

}

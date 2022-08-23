using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Inventory for actual states of the agent.
public class AgentStates : MonoBehaviour
{
    public List<string> worldElements = new List<string>();
    public List<string> positions = new List<string>();
    public Dictionary<string, float> inventory = new Dictionary<string, float>();
    public Dictionary<string, bool> status = new Dictionary<string, bool>();

    public void AddWorldItem(string name) //Add new WorldElement Item to the actual inventory collection.
    {
        worldElements.Add(name);
    }

    public void AddPositionItem(string name) //Add new Position Item to the actual inventory collection.
    {
        positions.Add(name);
    }

    public void AddInventoryItem(string name, float initialValue) //Add new Inventory Item to the actual inventory collection.
    {
        inventory.Add(name, initialValue);
    }

    public void AddStatusItem(string name, bool initialValue) //Add new Status Item to the actual status collection.
    {
        status.Add(name, initialValue);
    }

    public void RemoveWorldItem(string name) //Remove WorldElement Item from the actual inventory collection.
    {
        worldElements.Remove(name);
    }

    public void RemovePositionItem(string name) //Remove Position Item from the actual inventory collection.
    {
        positions.Remove(name);
    }

    public void RemoveInventoryItem(string name) //Remove Inventory Item from the actual inventory collection.
    {
        inventory.Remove(name);
    }

    public void RemoveStatusItem(string name) //Remove  Status Item from the actual status collection.
    {
        status.Remove(name);
    }

    public void ModifyInventoryItem(string name, float newValue) //Modify an actual inventory item of the collection.
    {
        if(inventory.ContainsKey(name))
        {
            inventory[name] = newValue;
        }
       
    }

    public void IncreaseInventoryItem(string name, float newValue) //Modify an actual inventory item of the collection (Increase Value).
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name] += newValue;
        }

    }

    public void DecreaseInventoryItem(string name, float newValue) //Modify an actual inventory item of the collection (Decrease Value).
    {
        if (inventory.ContainsKey(name))
        {
            inventory[name] -= newValue;
        }

    }

    public void ModifyStatusItem(string name, bool newValue) //Modify an actual status item of the collection.
    {
        if (status.ContainsKey(name))
        {
            status[name] = newValue;
        }
    }

    /*
    public void GetAllInventoryItems()
    {

    }

    public void GetAllStatusItem()
    {

    }

    public X GetActualValueOfIndexItemInList()
    {

    }
    */
}

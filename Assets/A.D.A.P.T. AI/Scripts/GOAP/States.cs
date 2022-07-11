using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    public Dictionary<string, int> worldStates; //All worldStates

    public States()
    {
        worldStates = new Dictionary<string, int>();
    }

    public void AddState(string key, int resource) //Add new state to the world state
    {
        worldStates.Add(key, resource);
    }

    public void RemoveState(string key, int resource) //Remove state from the world state
    {
        worldStates.Remove(key);
    }

    public void ModifyStateByKey(string key, int resource) //Modify exist state into world state
    {
        worldStates[key] = resource;
    }

    public Dictionary<string, int> GetAllStates() //Get all world states
    {
        return worldStates;
    }

    public int GetStateByKey(string key) //Get only one state from the world state
    {
        if (worldStates.ContainsKey(key))
            return worldStates[key];
        else
            return -1;
    }

    public bool StateExists(string key) //Check if a secific state exists
    {
        if (worldStates.ContainsKey(key))
            return true;
        else
            return false;
    }




}

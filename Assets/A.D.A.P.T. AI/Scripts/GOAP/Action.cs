using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public string actionName; //Name of the action
    public Animation actionAnimation; //Animation to play when run action
    public GameObject target; //Target to achieve/ go to
    public bool inRange; //Check if the target is in range <-!!!!!!
    public float duration; //Duration of the action
    public Dictionary<string, Resource> preconditions; //List of preconditions/resources to achieve
    public Dictionary<string, Resource> effects; //List of effects/resources to achieve
    public int totalPriority; //Sumatory of the different precondition's priorities 
    public bool running; //Action running or not

    // Start is called before the first frame update
    public Action()
    {
        preconditions = new Dictionary<string, Resource>();
        effects = new Dictionary<string, Resource>();
    }

    public void AddPrecondition(string key, Resource resource)
    {
        preconditions.Add(key, resource);
    }

    public void RemovePrecondition(string key)
    {
        preconditions.Remove(key);
    }

    public void AddEffects(string key, Resource resource)
    {
        effects.Add(key, resource);
    }

    public void RemoveEffects(string key)
    {
        effects.Remove(key);
    }

    public bool isRunning()
    {
        if (running)
            return true;
        else
            return false;
    }

    public int CalculateTotalPriority()
    {
        int totalPriority = 0;

        foreach (KeyValuePair<string,Resource> r in preconditions)
        {
            totalPriority += r.Value.priority;
        }

        return totalPriority;
    }

    public Dictionary<string, Resource> GetAllPreconditions()
    {
        return preconditions;
    }

    public Dictionary<string, Resource> GetAllEffects()
    {
        return effects;
    }

    public bool isInRange()
    {
        return inRange;
    }

    public void setInRange(bool inRange)
    {
        this.inRange = inRange;
    }
}

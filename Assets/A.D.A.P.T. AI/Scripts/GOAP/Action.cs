using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class Action : MonoBehaviour
{
    [Serializable]
    public class ResourceStruct
    {
        public string key;
        public ResourceType selectedType;
        public int priority;
    }
    public string actionName; //Name of the action
    public Animation actionAnimation; //Animation to play when run action
    public GameObject target; //Target to achieve/ go to
    public bool inRange; //Check if the target is in range
    public float duration; //Duration of the action
    [SerializeField]
    public List<ResourceStruct> preconditions_list; //List of preconditions/resources to achieve for manipulate in the Editor
    [SerializeField]
    public List<ResourceStruct> effects_list; //List of effects/resources to achieve for manipulate in the Editor
    public Dictionary<string, IResource> preconditions; //List of preconditions/resources to achieve
    public Dictionary<string, IResource> effects; //List of effects/resources to achieve
    public int totalPriority; //Sumatory of the different precondition's priorities 
    public bool running; //Action running or not

    // Start is called before the first frame update
    void Awake()
    {
        preconditions = new Dictionary<string, IResource>();
        effects = new Dictionary<string, IResource>();
        CheckResources();
    }


    public void CheckResources()
    {
        //Store all Editor preconditions into the Dictionary
        foreach (var p in preconditions_list)
        {
            if (p.selectedType == ResourceType.WorldElement)
            {
                preconditions[p.key] = new WorldResource(p.key, p.selectedType, null, null, p.priority);
            }
            else if (p.selectedType == ResourceType.Position)
            {
                preconditions[p.key] = new PositionResource(p.key, p.selectedType, null, null, p.priority, 0.0f);
            }
            else if (p.selectedType == ResourceType.InventoryObject)
            {
                preconditions[p.key] = new InventoryResource(p.key, p.selectedType, 0.0f, 0.0f, p.priority, 0.0f, false);
            }
            else if (p.selectedType == ResourceType.Status)
            {
                preconditions[p.key] = new StatusResource(p.key, p.selectedType, false, false, p.priority);
            }
        }
        //Store all Editor effects into the Dictionary
        foreach (var e in effects_list)
        {
            if (e.selectedType == ResourceType.WorldElement)
            {
                preconditions[e.key] = new WorldResource(e.key, e.selectedType, null, null, e.priority);
            }
            else if (e.selectedType == ResourceType.Position)
            {
                preconditions[e.key] = new PositionResource(e.key, e.selectedType, null, null, e.priority, 0.0f);
            }
            else if (e.selectedType == ResourceType.InventoryObject)
            {
                preconditions[e.key] = new InventoryResource(e.key, e.selectedType, 0.0f, 0.0f, e.priority, 0.0f, false);
            }
            else if (e.selectedType == ResourceType.Status)
            {
                preconditions[e.key] = new StatusResource(e.key, e.selectedType, false, false, e.priority);
            }
        }
    }

    public void AddPrecondition(string key, IResource resource)
    {
        preconditions.Add(key, resource);
    }

    public void RemovePrecondition(string key)
    {
        preconditions.Remove(key);
    }

    public void AddEffects(string key, IResource resource)
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

        foreach (KeyValuePair<string, IResource> r in preconditions)
        {
            totalPriority += r.Value.priority;
        }

        return totalPriority;
    }

    public Dictionary<string, IResource> GetAllPreconditions()
    {
        return preconditions;
    }

    public Dictionary<string, IResource> GetAllEffects()
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

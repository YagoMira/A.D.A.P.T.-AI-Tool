using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public abstract class Action : MonoBehaviour
{
    #region Resource Struct
    [Serializable]
    public class ResourceStruct
    {
        public string key;
        public ResourceType selectedType;

        [SerializeField]
        public WorldResource w_resource;

        [SerializeField]
        public PositionResource p_resource;

        [SerializeField]
        public InventoryResource i_resource;

        [SerializeField]
        public StatusResource s_resource;

        public ResourceStruct(string key, WorldResource resource)
        {
            this.key = key;
            this.w_resource = resource;
            this.p_resource = null;
            this.i_resource = null;
            this.s_resource = null;
        }

        public ResourceStruct(string key, PositionResource resource)
        {
            this.key = key;
            this.w_resource = null;
            this.p_resource = resource;
            this.i_resource = null;
            this.s_resource = null;
        }

        public ResourceStruct(string key, InventoryResource resource)
        {
            this.key = key;
            this.w_resource = null;
            this.p_resource = null;
            this.i_resource = resource;
            this.s_resource = null;
        }

        public ResourceStruct(string key, StatusResource resource)
        {
            this.key = key;
            this.w_resource = null;
            this.p_resource = null;
            this.i_resource = null;
            this.s_resource = resource;
        }
    }
    #endregion

    public string actionName; //Name of the action
    public Animation actionAnimation; //Animation to play when run action
    public GameObject target; //Target to achieve/ go to
    public bool inRange; //Check if the target is in range
    public float duration; //Duration of the actions
    public List<ResourceStruct> preconditions_list; //List of preconditions/resources to achieve for manipulate in the Editor
    public List<ResourceStruct> effects_list; //List of effects/resources to achieve for manipulate in the Editor
    public Dictionary<string, IResource> preconditions; //List of preconditions/resources to achieve
    public Dictionary<string, IResource> effects; //List of effects/resources to achieve
    public int totalPriority; //Sumatory of the different precondition's priorities 
    public bool running; //Action running or not

    // Start is called before the first frame update
    void Awake()
    {
        //preconditions_list = new List<ResourceStruct>();
        //effects_list = new List<ResourceStruct>();
        preconditions = new Dictionary<string, IResource>();
        effects = new Dictionary<string, IResource>();
    }

    void Start()
    {
        Debug_Vars(); //Allows developer to do some tests.
    }


    #region GOAP Functions
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
            totalPriority += r.Value.Priority;
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
    #endregion

    public void Debug_Vars()
    {
        foreach (var i in preconditions_list)
        {
            Debug.Log("Index - Preconditions: " + i + " - " + i.w_resource.ResourceName + " - " + i.p_resource.ResourceName + " - " + i.i_resource.ResourceName + " - " + i.s_resource.ResourceName);
        }
        foreach (var j in effects_list)
        {
            Debug.Log("Index - Effects: " + j + " - " + j.w_resource.ResourceName + " - " + j.p_resource.ResourceName + " - " + j.i_resource.ResourceName + " - " + j.s_resource.ResourceName);
        }
    }
}

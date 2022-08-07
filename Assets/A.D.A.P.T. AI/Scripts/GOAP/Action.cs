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

        public ResourceStruct(string key, Resource resource)
        {
            this.key = key;
            if(resource.resourceEnumType == ResourceType.WorldElement.ToString())
            {
                this.w_resource = (WorldResource)resource;
                this.selectedType = ResourceType.WorldElement;
            }
            else if (resource.resourceEnumType == ResourceType.Position.ToString())
            {
                this.p_resource = (PositionResource)resource;
                this.selectedType = ResourceType.Position;
            }
            else if (resource.resourceEnumType == ResourceType.InventoryObject.ToString())
            {
                this.i_resource = (InventoryResource)resource;
                this.selectedType = ResourceType.InventoryObject;
            }
            else if (resource.resourceEnumType == ResourceType.Status.ToString())
            {
                this.s_resource = (StatusResource)resource;
                this.selectedType = ResourceType.Status;
            }
        }

        private void ResetResources() //Function for reset all resources
        {
            this.w_resource = null;
            this.p_resource = null;
            this.i_resource = null;
            this.s_resource = null;
        }

    }
    #endregion

    public string actionName; //Name of the action
    public Animation actionAnimation; //Animation to play when run action
    public GameObject target; //Target to achieve/ go to
    [ReadOnly]
    public bool inRange = true; //Check if the target is in range
    public float duration; //Duration of the actions
    public List<ResourceStruct> preconditions_list; //List of preconditions/resources to achieve for manipulate in the Editor
    public List<ResourceStruct> effects_list; //List of effects/resources to achieve for manipulate in the Editor
    public Dictionary<string, Resource> preconditions; //List of preconditions/resources to achieve
    public Dictionary<string, Resource> effects; //List of effects/resources to achieve
    public int totalPriority; //Sumatory of the different precondition's priorities 
    public bool running; //Action running or not

    // Start is called before the first frame update
    void Start()
    {
        //preconditions_list = new List<ResourceStruct>();
        //effects_list = new List<ResourceStruct>();
        preconditions = new Dictionary<string, Resource>();
        effects = new Dictionary<string, Resource>();
        PerformData();
        totalPriority = CalculateTotalPriority();
       
    }

    void Update()
    {
        //Debug_Vars(); //Allows developer to do some tests.
        //CheckDistance();
    }

    void PerformData() //Store public preconditions and effects list into preconditions and effects dictionaries
    {
        if (preconditions_list != null)
        {
            foreach (ResourceStruct r_p in preconditions_list)
            {
                preconditions.Add(r_p.key, CheckResource(r_p));
            }
        }

        if (effects_list != null)
        {
            foreach (ResourceStruct r_e in effects_list)
            {
                effects.Add(r_e.key, CheckResource(r_e));
            }
        }
    }

    Resource CheckResource (ResourceStruct resource) //Allows to check if the actual resource has only the specific type
    {
        int index;

        foreach (ResourceType r_t in Enum.GetValues(typeof(ResourceType)))
        {
            if (resource.selectedType == r_t)
            {
                index = Array.IndexOf(Enum.GetValues(typeof(ResourceType)), r_t);

                if (index == 0)
                {
                    resource.w_resource.ModifyValue(resource.w_resource.resource_value);
                    resource.w_resource.SetResourceType(resource.selectedType);
                    return resource.w_resource;
                }
                else if(index == 1)
                {
                    resource.p_resource.ModifyValue(resource.p_resource.resource_value);
                    resource.p_resource.SetResourceType(resource.selectedType);
                    return resource.p_resource;
                }
                else if(index == 2)
                {
                    resource.i_resource.ModifyValue(resource.i_resource.resource_value);
                    resource.i_resource.SetResourceType(resource.selectedType);
                    return resource.i_resource;
                }
                else if(index == 3)
                {
                    resource.s_resource.ModifyValue(resource.s_resource.resource_value);
                    resource.s_resource.SetResourceType(resource.selectedType);
                    return resource.s_resource;
                }
                else
                {
                    return null;
                }
            }
        }

        return null;
    }

    #region GOAP Functions
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

        foreach (KeyValuePair<string, Resource> r in preconditions)
        {
            totalPriority += r.Value.priority;
        }

        foreach (KeyValuePair<string, Resource> e in effects)
        {
            totalPriority += e.Value.priority;
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
    #endregion

    public void Debug_Vars()
    {
        /*
        foreach (var i in preconditions_list)
        {
            //Debug.Log("Index - Preconditions: " + i + " - " + i.w_resource.resourceName + " - " + i.p_resource.resourceName + " - " + i.i_resource.resourceName + " - " + i.s_resource.resourceName);
            //Debug.Log("Index: " + i + " - Resource:" + i.w_resource.value.name);
        }
        
        foreach (var j in effects_list)
        {
            Debug.Log("Index - Effects: " + j + " - " + j.w_resource.resourceName + " - " + j.p_resource.resourceName + " - " + j.i_resource.resourceName + " - " + j.s_resource.resourceName);
        } 
        foreach (KeyValuePair<string,Resource> r in preconditions)
        {
            Debug.Log("RESOURCE-VALUE-P: " + r.Value.GetResourceType());
        }
        foreach (KeyValuePair<string, Resource> r in effects)
        {
            Debug.Log("RESOURCE-VALUE-E: " + r.Value.GetResourceType());
        }
        */
    }

    public bool CheckDistance() //Check if the actual distance of World and Position resources can be achieved by the agent
    {
        float actionLimitRange = 0.0f;
        GameObject resource_position_gameobject;
        Transform resource_position_transform;
        float distance;

        foreach (KeyValuePair<string, Resource> r in preconditions)
        {
            if((r.Value.resourceEnumType == ResourceType.WorldElement.ToString()) || (r.Value.resourceEnumType == ResourceType.Position.ToString()))
            {
                actionLimitRange = r.Value.limit;
                if (r.Value.value != null) 
                {
                    if(r.Value.resourceEnumType == ResourceType.WorldElement.ToString())
                    {
                        resource_position_gameobject = (GameObject)r.Value.value;
                        distance = (Vector3.Distance(gameObject.transform.position, resource_position_gameobject.transform.position));
                    }
                    else
                    {
                        resource_position_transform = (Transform)r.Value.value;
                        distance = (Vector3.Distance(gameObject.transform.position, resource_position_transform.transform.position));

                    }

                    if (distance <= actionLimitRange)
                    {
                        setInRange(true);
                    }
                    else //Can't run action
                    {
                        setInRange(false);
                    }
                }
                else //In case of value of world/position resource don't have value assigned.
                {
                    Debug.Log("NULL VALUE\n");
                    r.Value.value = target; //Assign target in case of don't find some resource.
                }
            }
            else //For the other resources (Status or Inventory).
            {
                setInRange(true);
            }
        }

        Debug.Log("IN RANGE: " + inRange);
        return inRange;
    }
}

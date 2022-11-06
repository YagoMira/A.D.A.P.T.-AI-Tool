using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            setResourceType(resource);
        }

        public void setResourceType(Resource resource)
        {
            if (resource.resourceEnumType == ResourceType.WorldElement.ToString())
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
    public bool running; //Action running or not
    public bool finished; //Action FINISHED or not
    public AnimationClip actionAnimation; //Animation to play when run action
    public float duration; //Duration of the actions
    public bool hasTarget; //In case of actual Action has target to achieve (some world position), diplay target variables in the inspector.
    public GameObject target; //Target to achieve/ go to
    public float stopDistance; //Distance for stop Navmesh from the actual target.
    [ReadOnly]
    public bool inRange; //Check if the target is in range
    public List<ResourceStruct> preconditions_list; //List of preconditions/resources to achieve for manipulate in the Editor
    public List<ResourceStruct> effects_list; //List of effects/resources to achieve for manipulate in the Editor
    public Dictionary<string, Resource> preconditions; //List of preconditions/resources to achieve
    public Dictionary<string, Resource> effects; //List of effects/resources to achieve
    [ReadOnly]
    public int totalPriority; //Sumatory of the different resources's priorities 
    [ReadOnly]
    public int totalCost; //Sumatory of the different resources's cost 
    int modNumber = 10; //Number used to calculate cost (ex. In case of have 60 items, cost of 6 - Division by 10 is the default).

    protected AgentStates global_states = GlobalStates.GetGlobalStatesInstance.GetGlobalStates(); //Call it in this class for do easy to modify globlal states with custom actions.

    // Start is called before the first frame update
    void OnEnable() //Start.
    {
        //preconditions_list = new List<ResourceStruct>();
        //effects_list = new List<ResourceStruct>();
        preconditions = new Dictionary<string, Resource>();
        effects = new Dictionary<string, Resource>();
        PerformData();
        totalPriority = CalculateTotalPriority();
        CalculateCost();
        totalCost = CalculateTotalCost();

       if(actionName == null || actionName == "") //In case of null name add one random name.
       { 
            Debug.Log("<color=red>AGENT: </color> " + this.gameObject.GetComponent<MonoBehaviour>() + " <color=red>HAS AN ACTION WITHOUT AN ACTION NAME, MUST BE ADDED!.</color>");
            actionName = "Action" + GenerateRandomName(5);
       }

       if(actionAnimation != null) //In case of have some animation.
       {
            if (duration != 0) //If duration has been modified by user.
            {
                if (actionAnimation.averageDuration < duration) //If duration of animation is less than inspector value, then loop animation.
                    actionAnimation.wrapMode = WrapMode.Loop;
            }
            else
            {
                duration = actionAnimation.averageDuration;
                actionAnimation.wrapMode = WrapMode.Default;
            }
       }
    }

    void PerformData() //Store public preconditions and effects list into preconditions and effects dictionaries.
    {
        int counter_pred = 0, counter_eff = 0; //Used for count the number of foreach iterations.

        if (preconditions_list != null)
        {
            foreach (ResourceStruct r_p in preconditions_list)
            {
                if(r_p.key == null || r_p.key == "") //In case of actual resource doesn't have a name.
                {
                    Debug.Log("<color=red>RESOURCE(PRECONDITION): </color> " + counter_pred + " <color=red> DOESN'T HAVE A NAME, RANDOM NAME WILL BE ADDED!.</color>");

                    r_p.key = GenerateRandomName(7);

                    if(r_p.w_resource.resourceName == "" || (r_p.w_resource.resourceName == null))
                    {
                        r_p.w_resource.resourceName = r_p.key;
                    }
                    else if (r_p.p_resource.resourceName == "" || (r_p.p_resource.resourceName == null))
                    {
                        r_p.p_resource.resourceName = r_p.key;
                    }
                    else if (r_p.i_resource.resourceName == "" || (r_p.i_resource.resourceName == null))
                    {
                        r_p.i_resource.resourceName = r_p.key;
                    }
                    else if (r_p.s_resource.resourceName == "" || (r_p.s_resource.resourceName == null))
                    {
                        r_p.s_resource.resourceName = r_p.key;
                    }
                }

                preconditions.Add(r_p.key, CheckResource(r_p));
                counter_pred++;
            }

            preconditions = preconditions.OrderByDescending(x => x.Value.priority).ToDictionary(x => x.Key, x => x.Value); //Order Preconditions by priority
            
        }

        if (effects_list != null)
        {
            foreach (ResourceStruct r_e in effects_list)
            {
                if (r_e.key == null || r_e.key == "") //In case of actual resource doesn't have a name.
                {
                    Debug.Log("<color=red>RESOURCE(EFFECT): </color> " + counter_eff + " <color=red> DOESN'T HAVE A NAME, RANDOM NAME WILL BE ADDED!.</color>");

                    r_e.key = GenerateRandomName(7);

                    if (r_e.w_resource.resourceName == "" || (r_e.w_resource.resourceName == null))
                    {
                        r_e.w_resource.resourceName = r_e.key;
                    }
                    else if (r_e.p_resource.resourceName == "" || (r_e.p_resource.resourceName == null))
                    {
                        r_e.p_resource.resourceName = r_e.key;
                    }
                    else if (r_e.i_resource.resourceName == "" || (r_e.i_resource.resourceName == null))
                    {
                        r_e.i_resource.resourceName = r_e.key;
                    }
                    else if (r_e.s_resource.resourceName == "" || (r_e.s_resource.resourceName == null))
                    {
                        r_e.s_resource.resourceName = r_e.key;
                    }
                }

                effects.Add(r_e.key, CheckResource(r_e));
                counter_eff++;
            }

            effects = effects.OrderByDescending(x => x.Value.priority).ToDictionary(x => x.Key, x => x.Value); //Order Effects by priority
        }
    }

    public string GenerateRandomName(int size) //Generates random string name for actions, resources, ...
    {
        string result = "";
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyz"; //Used for create random names.

        for (int i = 0; i < 7; i++)
        {
            result += chars[UnityEngine.Random.Range(0, chars.Length)];
        }

        return result;
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

    public int CalculateTotalCost()
    {
        int totalCost = 0;

        foreach (KeyValuePair<string, Resource> r in preconditions)
        {
            totalCost += r.Value.cost;
        }

        foreach (KeyValuePair<string, Resource> e in effects)
        {
            totalCost += e.Value.cost;
        }

        return totalCost;
    }

    public void CalculateCost() //Allows to calculate cost in fuction of distance, inventory items, ...
    {
        foreach (KeyValuePair<string, Resource> r in preconditions)
        {
            r.Value.cost = SubCalculateCost(r.Value);
        }

        foreach (KeyValuePair<string, Resource> e in effects)
        {
            e.Value.cost = SubCalculateCost(e.Value);
        }
    }

    public int SubCalculateCost(Resource resource) //Auxiliar function for retrieve the cost in relation of received Resource
    {
        int totalCost = 0;

        if (resource.resourceEnumType == ResourceType.WorldElement.ToString() || resource.resourceEnumType == ResourceType.Position.ToString()) //In case of World/Position Resource
        {
            int distance = 0;
            int increaseMod = modNumber; //In case of high number to compare with (Big distances or high inventory object amount).


            if (resource.resourceEnumType == ResourceType.WorldElement.ToString())
            {
                if ((GameObject) resource.value != null)
                {
                    distance = (int)Vector3.Distance(gameObject.transform.position, ((GameObject)resource.value).transform.position);
                }
                else
                {
                    Debug.Log("<color=red>RESOURCE: </color>" + resource.resourceName + " <color=red> DOESN'T HAVE AN ASSIGNED VALUE, DEFAULT TARGET WILL BE ADDED AS ONE!.</color>");
                    resource.value = target;
                    ((WorldResource)resource).resource_value = target;
                    distance = (int)Vector3.Distance(gameObject.transform.position, ((GameObject)resource.value).transform.position);
                }
                
            } 
            else if(resource.resourceEnumType == ResourceType.Position.ToString())
            {
                if ((Transform)resource.value != null)
                {
                    distance = (int)Vector3.Distance(gameObject.transform.position, ((Transform)resource.value).transform.position);
                }
                else
                {
                    Debug.Log("<color=red>RESOURCE: </color>" + resource.resourceName + " <color=red> DOESN'T HAVE AN ASSIGNED VALUE, DEFAULT TARGET WILL BE ADDED AS ONE!.</color>");
                    resource.value = target.transform;
                    ((PositionResource)resource).resource_value = target.transform;
                    distance = (int)Vector3.Distance(gameObject.transform.position, ((Transform)resource.value).transform.position);
                }
                
            }
                

            while ((distance / modNumber) > modNumber) //In case of high number to compare with (Big distances or high inventory object amount).
            {
                increaseMod = increaseMod * modNumber;
            }

            distance = distance / increaseMod; //Mod 10 by default
            totalCost = distance;
        }
        else if (resource.resourceEnumType == ResourceType.InventoryObject.ToString()) //In case of Inventory Resource
        {
            int items = 0;
            int increaseMod = modNumber; //In case of high number to compare with (Big distances or high inventory object amount).

            items = (int)((float)resource.value / modNumber); //Mod 10 by default

            while (items > modNumber) //In case of high number to compare with (Big distances or high inventory object amount).
            {
                increaseMod = increaseMod * modNumber;
                items = (int)((float)resource.value / increaseMod);
            }

            totalCost = items;
        }
        else if (resource.resourceEnumType == ResourceType.Status.ToString()) //In case of Status Resource
        {
            if((bool)resource.value == true)
                totalCost = 1; //Add a extra cost of 1 in case of need some variable as true.
        }

        return totalCost;
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

    public bool CheckPreconditions() //Function for check if actual precondition asserts all actual world resources, in case of not...Dont add it to the plan.
    {
        float actionLimitRange = 0.0f;
        GameObject resource_position_gameobject = null;
        Transform resource_position_transform = null;
        float distance;
        int element = 0; //Var used for check if all elements in the preconditions array are checked.
        bool target_exists = false; //Target exists as precondition.
        bool assertPreconditions = false; //Helps to know if preconditions are completed or not.

        foreach (KeyValuePair<string, Resource> r in preconditions)
        {
            if ((r.Value.resourceEnumType == ResourceType.WorldElement.ToString()) || (r.Value.resourceEnumType == ResourceType.Position.ToString()))
            {
                actionLimitRange = r.Value.limit;
                element++;

                if (r.Value.value.ToString() != "null") 
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

                    if (target.Equals(resource_position_gameobject) || target.transform.position.Equals(resource_position_transform) && target != null) //IF NOT FIND ANY PRECONDITION WITH VALUE AS TARGET, THEN CANNOT RUN ACTION!.
                    {
                        target_exists = true;
                    }

                    if (element == preconditions.Count()) //Last element, then check if some precondicion with actual target exists.
                    {
                        if(target_exists != true)
                        {
                            Debug.Log("<color=red>ADD SOME PRECONDITION AS World/Position Resource from TARGET!</color>");

                            /***IN CASE OF NEED CHANGE SOME TARGET IN EXECUTION TIME***/
                            //ReRun();
                            /***IN CASE OF NEED CHANGE SOME TARGET IN EXECUTION TIME***/
                        }
                    }

                    if (distance <= actionLimitRange)
                    {
                        setInRange(true);
                        assertPreconditions = true;
                    }
                    else //Can't run action because distance range
                    {
                        setInRange(false);
                        assertPreconditions = false;
                    }
                }
                else //In case of value of world/position resource don't have value assigned.
                {
                    Debug.Log(r.Key + " :<color=red> TARGET VALUE IS NULL!. Action Target will be assigned.</color>\n");
                    if (target != null)
                        r.Value.value = target; //Assign target in case of don't find some resource.
                }

            }
            else if (r.Value.resourceEnumType == ResourceType.InventoryObject.ToString()) //In case of inventory item
            {
                element++;
                if(r.Value.limit <= (float)r.Value.value) //Check if value is less than limit of inventory items.
                {
                    Debug.Log("<color=red>Inventory </color> " + r.Key + " <color=red> in </color> " + actionName + " <color=red> is full!.(Reduce value or increase limit.).</color>");
                    assertPreconditions = false;
                }

                CheckEffectsResources(); //Call this function for check if inventory maximum is surpassed.

                assertPreconditions = true;
            }
            else //For the other resources (Status or Inventory).
            {
                element++;
                assertPreconditions = true; //This values depends on if other resources asserts conditios or not.
            }
        }

        if(target == null && hasTarget == true)
        {
            Debug.Log(actionName + " :<color=red> TARGET VALUE IS NULL!. For use 'hasTarget' bool you should assign one gameobject as target.</color>\n");
        }

        return assertPreconditions;
    }

    public void CheckEffectsResources() //Check if inventory values is less than limit into the effects dictionary.
    {
        foreach (KeyValuePair<string, Resource> e in effects)
        {
            if (e.Value.resourceEnumType == ResourceType.InventoryObject.ToString()) //In case of inventory item
            {
                if (e.Value.limit <= (float)e.Value.value) //Check if value is less than limit of inventory items.
                {
                    Debug.Log("<color=red>Inventory </color> " + e.Key + " <color=red> in </color> " + actionName + "(Effects) <color=red> is full!.(Reduce value or increase limit.).</color>");
                }
            }
        }
    }

    public void ReRun() //CALL THIS CLASS ONLY IF YOU NEED DO CHANGES IN EXECUTION TIME
    {
        finished = false;
        preconditions = new Dictionary<string, Resource>();
        PerformData();
    }

    public abstract void PerformAction();
}

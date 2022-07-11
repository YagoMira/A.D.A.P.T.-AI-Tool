using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_DB_Example : MonoBehaviour
{
    public List<Resource> resources = new List<Resource>();

    // Start is called before the first frame update
    void Start()
    {
        resources.Add(new Resource("Fire", ResourceType.WorldElement, false, 1, 0.0f, 1.0f)); //Resource - WorldObject
        resources.Add(new Resource("Ore", ResourceType.InventoryObject, true, 2, 10.0f, 25.0f)); //Resource - Consumable
    }

    public List<Resource> GetList()
    {
        return resources;
    }
}

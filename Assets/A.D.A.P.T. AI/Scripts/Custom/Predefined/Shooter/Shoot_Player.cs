using UnityEngine;
using UnityEngine.AI;
public class Shoot_Player : Action
{
    Agent agent;
    NavMeshAgent actual_agent;
    private float timer;
    GameObject bullet;
    GameObject bullet_spawn;

    void Awake()
    {
        /******DON'T DELETE THIS LINES!!!******/
        agent = gameObject.GetComponent<Agent>();
        //WARNING MESSAGE!
        Debug.Log(" <color=blue> Action: </color> " + actionName + " <color=blue> has preconditions / effects added by code,</color> <color=red> DON'T ADD MORE VIA INSPECTOR!.</color>");
        /************/

        /************/
        //HERE YOU CAN ADD YOUR PRECONDITIONS // EFFECTS
        /************/
        //In case of add preconditions/effects, uncomment the next lines:
        //preconditions_list.Add(ResourceStruct);
        //effects_list.Add(ResourceStruct);
    }

    public override void PerformAction()
    {
        //Uncomment next line if you need some navmesh:
        //actual_agent = gameObject.GetComponent<NavMeshAgent>();
        //Use 'finished = true;' when finish the action.

        bullet = GameObject.FindGameObjectWithTag("Bullet");

        /*This code is a simple example of a ranged-enemy, doesn't pretend to have a good functionality: */
        if (Time.time > timer)
        {
            timer = Time.time + 1500f / 1000;

            this.gameObject.transform.LookAt(target.transform);
            bullet_spawn = Instantiate(bullet, new Vector3(this.gameObject.transform.position.x + 4, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z), this.gameObject.transform.rotation); //create our bullet
            bullet_spawn.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, 80f)); //shoot the bullet

            global_states.DecreaseInventoryItem("health", 10f);
        }

        if (global_states.inventory["health"] <= 0)
        {
            finished = true;
        }
    }

}

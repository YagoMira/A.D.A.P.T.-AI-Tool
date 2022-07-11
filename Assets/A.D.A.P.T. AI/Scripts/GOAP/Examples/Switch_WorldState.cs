using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_WorldState : MonoBehaviour
{

    public Text text;

    public GameObject switchObject;
    
    // Start is called before the first frame update
    void Start() {
        switchObject = this.gameObject;
        World.Instance.GetWorldState().AddState(switchObject.name, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(switchObject.name);
        text.text = switchObject.name + "-" +  World.Instance.GetWorldState().GetStateByKey(switchObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        World.Instance.GetWorldState().ModifyStateByKey(switchObject.name, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        World.Instance.GetWorldState().ModifyStateByKey(switchObject.name, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch_Resource_1 : MonoBehaviour
{

    public Text text;

    public GameObject switchObject;
    public GameObject database;

    Resource_DB_Example db_Resources;
    List<Resource> localList;

    // Start is called before the first frame update
    void Start() {
        switchObject = this.gameObject;
        db_Resources = database.GetComponent<Resource_DB_Example>();
        localList = db_Resources.GetList();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Example resource" + db_Resources.GetList()[1].resourceName);
        text.text = localList[1].resourceName + ":" + localList[1].resourceType + ":" + localList[1].value;
    }

    private void OnTriggerEnter(Collider other)
    {
        localList[1].ModifyValue(100);
    }

    private void OnTriggerExit(Collider other)
    {
        localList[1].ModifyValue(110);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GoTo_Example_1 : Action
{

    public override void PerformAction()
    {
        Debug.Log("<color=blue> START--ACTION!!!! </color>");
        //StartCoroutine(waiter());
        finished = true;
    }

    IEnumerator waiter()
    {
        //Wait for 'X' seconds
        yield return new WaitForSeconds(5);
        finished = true;
        Debug.Log("<color=red> EXAMPLE--ACTION---DONE!!!! </color>");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{

    public float force = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(0, 0, -1) * force);

        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(0, 0, 1) * force);

        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.left * force);

        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.right * force);

    }
}

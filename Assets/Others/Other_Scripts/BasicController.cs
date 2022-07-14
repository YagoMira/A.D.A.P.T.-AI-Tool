﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{

    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;

    }
}

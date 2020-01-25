using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code from https://learn.unity.com/project/roll-a-ball-tutorial
public class PlayerController : MonoBehaviour
{
    // public makes this accessible from Unity Inspector
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Check for player input before rendering every frame
    //void Update()

    // Called before performing physics calculations 
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // public makes this accessible from Unity Inspector
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Drop ball at start point
        rb.transform.position = new Vector3(Globals.startX, 1, Globals.startZ);
    }

    // Check for player input before rendering every frame
    //void Update()

    // Called before performing physics calculations 
    // Code from https://learn.unity.com/project/roll-a-ball-tutorial
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }
}

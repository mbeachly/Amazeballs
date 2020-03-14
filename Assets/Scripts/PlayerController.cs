using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets input from phone accelerometer and applies force to the player ball
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }


    // Called before performing physics calculations 
    // Code from https://learn.unity.com/project/roll-a-ball-tutorial
    void FixedUpdate()
    {   
        /*
        // Keyboard controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * Globals.ballSpeed);
        */
        
        
        // Phone accelerometer controls
        // https://www.youtube.com/watch?v=fsEkZLBeTJ8
        Vector3 tilt = Input.acceleration;
        // Rotate so that up is perpendicular to phone surface
        tilt = Quaternion.Euler(90, 0, 0) * tilt;
        rb.AddForce(tilt * Globals.ballSpeed);
        
        
    }
}

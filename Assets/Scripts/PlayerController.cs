using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // public makes this accessible from Unity Inspector
    public float speed;
    bool isFlat = true;
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
        /* Keyboard controls
        float moveHorizontal = Input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        */

        // Phone accelerometer controls
        // https://www.youtube.com/watch?v=fsEkZLBeTJ8
        Vector3 tilt = Input.acceleration;
        if (isFlat) {
            // Rotate so that up is perpendicular to phone surface
            tilt = Quaternion.Euler(90, 0, 0) * tilt * Globals.ballSpeed;
        }

        rb.AddForce(tilt);
    }
}

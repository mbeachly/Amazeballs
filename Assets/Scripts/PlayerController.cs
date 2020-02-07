using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Ball Size
    public static float ballSize = 1f; // Default = 1

    // Ball Speed
    public static float ballSpeed = 6f; // Default = 5

    // Start Point
    public static int startX = 0;
    public static int startZ = 0;

    private Rigidbody rb;

    // Change Ball Size
    public void UpdateBallSize(float newSize)
    {
        ballSize = newSize / 3;
    }

    // Change Ball Speed
    public void UpdateBallSpeed(float newSpeed)
    {
        ballSpeed = newSpeed * 2;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Drop ball at start point
        rb.transform.position = new Vector3(startX, ballSize, startZ);
        rb.gameObject.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
    }


    // Called before performing physics calculations 
    // Code from https://learn.unity.com/project/roll-a-ball-tutorial
    void FixedUpdate()
    {   /*
        // Keyboard controls
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * ballSpeed);
        */

        // Phone accelerometer controls
        // https://www.youtube.com/watch?v=fsEkZLBeTJ8
        Vector3 tilt = Input.acceleration;
        // Rotate so that up is perpendicular to phone surface
        tilt = Quaternion.Euler(90, 0, 0) * tilt;
        rb.AddForce(tilt * ballSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Ball Size
    private static float ballSize = 1f; // Default = 1

    // Ball Speed
    private static float ballSpeed = 6f; // Default = 5

    // Start Point Coordinates
    public static int startX = 0;
    public static int startZ = 0;

    private Rigidbody rb;

    /// <summary>
    /// Sets Ball Size (radius of ball)
    /// </summary>
    /// <param name="newSize">floating point number (input from slider)</param>
    public void SetBallSize(float newSize)
    {
        ballSize = newSize / 3;
    }
    /// <summary>
    /// Gets Ball Size (radius of ball)
    /// </summary>
    /// <returns>integer (slider should be set to only whole numbers)</returns>
    public int GetBallSize()
    {   // Had issues with rounding without Ceil
        return (int)Mathf.Ceil(3 * ballSize);
    }
    /// <summary>
    /// Sets Ball Speed
    /// </summary>
    /// <param name="newSpeed">floating point number (input from slider)</param>
    public void SetBallSpeed(float newSpeed)
    {
        ballSpeed = newSpeed * 2;
    }
    /// <summary>
    /// Gets Ball Speed
    /// </summary>
    /// <returns>integer (slider should be set to only whole numbers)</returns>
    public int GetBallSpeed()
    {
        return (int)ballSpeed / 2;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Drop ball at start point
        rb.transform.position = new Vector3(startX, ballSize, startZ);
        // Set ball to specified size
        rb.gameObject.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
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

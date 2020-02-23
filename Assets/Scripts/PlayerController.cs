using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
            // Drop ball at start point
            rb.transform.position = new Vector3(Globals.startX, Globals.ballSize, Globals.startZ);
            // Set ball to specified size
            rb.gameObject.transform.localScale = new Vector3(Globals.ballSize, Globals.ballSize, Globals.ballSize);

            // Textures must be in Resources folder for Resources Load to work
            Texture2D ballTexture = Resources.Load(Globals.ballTexName) as Texture2D;
            Renderer ballRenderer = GetComponent<Renderer>();
            ballRenderer.material.SetTexture("_MainTex", ballTexture);
        }
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

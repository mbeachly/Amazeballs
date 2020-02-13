using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is a class to store global variables 
// such as background file path, start point, and endpoint
// Found on https://forum.unity.com/threads/c-global-variables-available-to-all-scenes.544901/
// Although this is NOT considered good Object-Oriented programming
public static class Globals
{
    // Set image for background (prototype phase)
    public static string backgroundFile = "/Materials/PunchbowlFallsU.png";

    // Ball Size
    public static float ballSize = 1f; // Default = 1

    // Ball Speed
    public static float ballSpeed = 6f; // Default = 5

    // Ball Texture
    public static string ballTexName = "hubble"; // Default = hubble

    // Start Point Coordinates
    public static int startX = 0;
    public static int startZ = 0;
    // End Point Coordinates
    public static int endX = -3;
    public static int endZ = 0;

    // Declare timeText variable
    public static string timeText;

    public static Texture2D tex = new Texture2D(1, 1);

    /// <summary>
    /// Gets Ball Size (radius of ball)
    /// </summary>
    /// <returns>integer (slider should be set to only whole numbers)</returns>
    public static int GetBallSize()
    {   // Had issues with rounding without Ceil
        return (int)Mathf.Ceil(3 * ballSize);
    }
    /// <summary>
    /// Gets Ball Speed
    /// </summary>
    /// <returns>integer (slider should be set to only whole numbers)</returns>
    public static int GetBallSpeed()
    {
        return (int)ballSpeed / 2;
    }
}

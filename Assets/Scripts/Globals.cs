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
    // Set image for background
    public static string backgroundFile = "/Resources/MazeTall.jpg";
    // Ball Size
    public static float ballSize = 1f; // Default = 1

    public static float ballSpeed = 6f;

    public static string ballTexName = "hubble";

    public static bool gameSaved = false;

    public static bool inCaptureImage = false;

    public static bool inSelectMaze = false;
	
	// Increment through menus in game
	public static int pickStep = 0;

    // Grayscale threshold value to distinguish wall from floor
    // White to light-gray = floor, dark-gray to black = wall
    public static float edgeThresh = 0.4f; // 0 = black, 1 = white

    // Where ball starts
    //public static float startX = 0;
    //public static float startZ = 0;
    public static Vector3 startPosition;

    // End of the maze
    //public static float endX = -1;
    //public static float endZ = -1;
    public static Vector3 endPosition;

    // Declare timeText variable
    public static string timeText;

    public static Texture2D tex = new Texture2D(1, 1);
    //public static Texture2D tex = Resources.Load("Maze512") as Texture2D; // Test image
    //public static Texture2D tex = Resources.Load("MazeTall") as Texture2D; // Test image

    public static int GetBallSize()
    {
        return (int)Mathf.Ceil(3 * ballSize);
    }

    public static int GetBallSpeed()
    {
        return (int)ballSpeed / 2;
    }
}

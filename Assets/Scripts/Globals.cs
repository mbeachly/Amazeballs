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
    // Set image for background (Can be removed when MazeGame scene is removed)
    public static string backgroundFile = "/Resources/MazeTall.jpg";
    // Ball Size
    public static float ballSize = 1f; // Default = 1

    public static float ballSpeed = 6f;

    public static string ballTexName = "hubble";

    public static bool gameSaved = false;

    public static bool playSavedGame = false;

    public static bool inCaptureImage = false;

    public static bool inSelectMaze = false;

    public static bool autoDetect = true; // Automatically detect start/end points
	
	// Increment through menus in game
	public static int pickStep = 0;

    // Grayscale threshold value to distinguish wall from floor
    // White to light-gray = floor, dark-gray to black = wall
    public static float edgeThresh = 0.4f; // 0 = black, 1 = white

    // Where ball starts
    public static Vector3 startPosition;

    // End of the maze
    public static Vector3 endPosition;

    // Declare timeText variable
    public static string timeText;

    public static Texture2D tex = Resources.Load("MazeTall") as Texture2D; 

    public static int GetBallSize()
    {
        return (int)Mathf.Ceil(3 * ballSize);
    }

    public static int GetBallSpeed()
    {
        return (int)ballSpeed / 2;
    }

    public static int GetEdgeThreshold()
    {
        return (int)(10 * edgeThresh);
    }
}

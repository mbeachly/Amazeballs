using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Update camera orthographic size based on device aspect ratio and object plane bounds
//Source:  Understanding Orthographic Size in Unity, https://www.youtube.com/watch?v=3xXlnSetHPM
//Source:  https://docs.unity3d.com/ScriptReference/Camera-orthographic.html

public class CameraController : MonoBehaviour
{
    //Get access to the maze plane
    public MeshRenderer ground;

    void Start()
    {
        //Get device aspect ratio
        float width = (float)Screen.width;
        float height = (float)Screen.height;
        float aspectRatio = width / height;

        //Get the maze plane's bounds ratio
        float groundRatio = ground.bounds.size.x / ground.bounds.size.y;

        //Fit camera to the edges of the maze plane
        if (aspectRatio >= groundRatio)
        {
            Camera.main.orthographicSize = ground.bounds.size.y / 2;
        }
        else
        {
            float diff = groundRatio / aspectRatio;
            Camera.main.orthographicSize = ground.bounds.size.y / 2 * diff;
        }
    }
}

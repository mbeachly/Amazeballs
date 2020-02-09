using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImage : MonoBehaviour
{
    WebCamTexture deviceCam;
    public RawImage camDisplay;
    SceneLoader scene;

    public void StartCam()
    {
        if (deviceCam != null)
        {
            deviceCam.Stop();
            deviceCam = null;
            camDisplay.texture = null;
            GameObject gameObject = new GameObject("SceneLoader");
            scene = gameObject.AddComponent < SceneLoader>();
            scene.LoadGame();
        }
        else { 
            if (WebCamTexture.devices.Length > 0)
            {
                for (int i = 0; i < WebCamTexture.devices.Length; i++)
                {
                    if (!WebCamTexture.devices[i].isFrontFacing)
                    {
                        deviceCam = new WebCamTexture(WebCamTexture.devices[i].name, Screen.width, Screen.height);
                    }
                }
                if (deviceCam != null)
                {
                    deviceCam.Play();
                    camDisplay.texture = deviceCam;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

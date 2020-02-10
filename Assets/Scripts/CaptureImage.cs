using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImage : MonoBehaviour
{
    WebCamTexture deviceCam;
    public RawImage camDisplay;
    public Text startCaptureCamera;
    SceneLoader scene;

    public void StartCam()
    {
        if (deviceCam != null)
        {
            deviceCam.Stop();
            deviceCam = null;
            camDisplay.texture = null;
            GameObject gameObject = new GameObject("SceneLoader");
            scene = gameObject.AddComponent <SceneLoader>();
            scene.LoadGame();
        }
        else { 
            if (WebCamTexture.devices.Length > 0)
            {
                for (int i = 0; i < WebCamTexture.devices.Length; i++)
                {
                    if (WebCamTexture.devices[i].isFrontFacing)
                    {
                        deviceCam = new WebCamTexture(WebCamTexture.devices[i].name);
                    }
                }
                if (deviceCam != null)
                {
                    deviceCam.Play();
                    camDisplay.texture = deviceCam;
                    startCaptureCamera.text = "Capture Image";
                }
            }
        }
        
    }

    // Update is called once per frame
    public void ExitToMainMenu()
    {
        if (deviceCam != null)
        {
            deviceCam.Stop();
            deviceCam = null;
            camDisplay.texture = null;
            GameObject gameObject = new GameObject("SceneLoader");
            scene = gameObject.AddComponent<SceneLoader>();
            scene.LoadMainMenu();
        }
        else
        {
            GameObject gameObject = new GameObject("SceneLoader");
            scene = gameObject.AddComponent<SceneLoader>();
            scene.LoadMainMenu();
        }

    }
}

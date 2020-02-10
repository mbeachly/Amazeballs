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
            Texture2D image = new Texture2D(deviceCam.width, deviceCam.height);
            image.SetPixels(deviceCam.GetPixels());
            image.Apply();
            byte[] bytes = image.EncodeToPNG();
            var tex = new Texture2D(1, 1);
            Globals.tex = tex;
            Globals.tex.LoadImage(bytes);
            //GetComponent<Renderer>().material.mainTexture = tex;
        
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
                    if (!WebCamTexture.devices[i].isFrontFacing)
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

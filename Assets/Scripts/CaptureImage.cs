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

    //Capture an image from device back facing camera and attach to maze plane
    //Source:  https://docs.unity3d.com/ScriptReference/WebCamTexture.html
    //Source:  https://www.youtube.com/watch?v=C_Q1QMomEyM
    //Source:  https://stackoverflow.com/questions/24496438/can-i-take-a-photo-in-unity-using-the-devices-camera
    
    public void StartCam()
    {
        //If camera is already on when button clicked, get current pixels, convert to PNG and load image to Globals.tex
        if (deviceCam != null)
        {
            Texture2D image = new Texture2D(deviceCam.width, deviceCam.height);
            image.SetPixels(deviceCam.GetPixels());
            image.Apply();
            byte[] bytes = image.EncodeToPNG();
            var tex = new Texture2D(1, 1);
            Globals.tex = tex;
            Globals.tex.LoadImage(bytes);
        
            //Stop live camera, set camera and raw image to null and load maze game
            deviceCam.Stop();
            deviceCam = null;
            camDisplay.texture = null;
            GameObject gameObject = new GameObject("SceneLoader");
            scene = gameObject.AddComponent <SceneLoader>();
            scene.LoadGame();
        }
        else { //If a camera exists and is currently off, play back facing camera when start camera button clicked
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
                    startCaptureCamera.text = "Capture Image";  //Change button text to capture image, on second click it will save image and start game
                }
            }
        }
        
    }

    // If Main Menu button is clicked, turn off camera if currently on and load main menu
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    Slider sizeSlider; 
    Slider speedSlider;
    Slider edgeSlider;
    Toggle detectToggle;

    public void SetBallSpace()
    {
        Globals.ballTexName = "hubble";
        SizeBalls("SpaceButton");
    }

    public void SetBallSmiley()
    {
        Globals.ballTexName = "smiley";
        SizeBalls("SmileButton");
    }

    public void SetBallMarble()
    {
        Globals.ballTexName = "marble";
        SizeBalls("MarbleButton");
    }

    // Shrink all the ball themes before enlarging the selected theme
    public void SizeBalls(string buttonName)
    {
        Vector3 scale;
        // Get all the theme buttons
        Button[] buttons = Object.FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == buttonName)
            {   // Enlarge theme
                scale = new Vector3(6f, 6f, 1f);
            }
            else
            {   // Shrink theme
                scale = new Vector3(4f, 4f, 1f);
            }

            // Get all image children
            Image[] images = button.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {   // Change the size of the panel image
                if (image.name == "Panel")
                {
                    image.transform.localScale = scale;
                }
            }
        }

        GameObject marbleButton = GameObject.Find("MarbleButton");
        marbleButton.GetComponent<Image>().transform.localScale = new Vector3(6f, 6f, 1f);
    }
    
    public void SetBallSize(float newSize)
    {
        Globals.ballSize = newSize / 3;
    }

    public void SetBallSpeed(float newSpeed)
    {
        Globals.ballSpeed = newSpeed * 2;
    }

    public void SetEdgeThreshold(float newThresh)
    {
        Globals.edgeThresh = newThresh / 10;
    }

    public void SetAutoDetect(bool newSetting)
    {
        Globals.autoDetect = newSetting;
    }


    // Start is called when Options scene is loaded
    void Start()
    {
        // Load current settings
        sizeSlider = GameObject.Find("SizeSlider").GetComponent<Slider>();
        sizeSlider.value = Globals.GetBallSize(); 

        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        speedSlider.value = Globals.GetBallSpeed();

        edgeSlider = GameObject.Find("EdgeSlider").GetComponent<Slider>();
        edgeSlider.value = Globals.GetEdgeThreshold();

        detectToggle = GameObject.Find("DetectToggle").GetComponent<Toggle>();
        detectToggle.isOn = Globals.autoDetect;
    }
}

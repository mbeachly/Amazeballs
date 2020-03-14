using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Takes input from options sliders to set threshold values
/// for detecting start and end points and edges.
/// Takes input from checkbox to enable or disable
/// automatic start and end point detection.
/// </summary>
public class AdvOptionsController : MonoBehaviour
{
    Slider edgeSlider;
    Slider blueSlider;
    Slider redSlider;
    Toggle detectToggle;

    public void SetEdgeThreshold(float newThresh)
    {
        Globals.edgeThresh = newThresh / 10;
    }

    public void SetBlueThreshold(float newThresh)
    {
        Globals.blueThresh = newThresh / 10;
    }

    public void SetRedThreshold(float newThresh)
    {
        Globals.redThresh = newThresh / 10;
    }

    public void SetAutoDetect(bool newSetting)
    {
        Globals.autoDetect = newSetting;
    }


    // Start is called when Options scene is loaded
    void Start()
    {
        edgeSlider = GameObject.Find("EdgeSlider").GetComponent<Slider>();
        edgeSlider.value = Globals.GetEdgeThreshold();

        blueSlider = GameObject.Find("StartSlider").GetComponent<Slider>();
        blueSlider.value = Globals.GetBlueThreshold();

        redSlider = GameObject.Find("EndSlider").GetComponent<Slider>();
        redSlider.value = Globals.GetRedThreshold();

        detectToggle = GameObject.Find("DetectToggle").GetComponent<Toggle>();
        detectToggle.isOn = Globals.autoDetect;
    }
}

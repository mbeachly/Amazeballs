using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    Slider sizeSlider; 
    Slider speedSlider;

    PlayerController player;

    // Start is called when Options scene is loaded
    void Start()
    {
        player = GameObject.Find("Player Controller").GetComponent<PlayerController>();
        // Load current settings
        sizeSlider = GameObject.Find("SizeSlider").GetComponent<Slider>();
        sizeSlider.value = player.GetBallSize(); 

        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        speedSlider.value = player.GetBallSpeed();
    }
}

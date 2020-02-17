using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    Slider sizeSlider; 
    Slider speedSlider;

    public void SetBallSpace()
    {
        Globals.ballTexName = "hubble";
    }

    public void SetBallSmiley()
    {
        Globals.ballTexName = "smiley";
    }

    public void SetBallMarble()
    {
        Globals.ballTexName = "marble";
    }
    
    public void SetBallSize(float newSize)
    {
        Globals.ballSize = newSize / 3;
    }

    public void SetBallSpeed(float newSpeed)
    {
        Globals.ballSpeed = newSpeed * 2;
    }


    // Start is called when Options scene is loaded
    void Start()
    {
        // Load current settings
        sizeSlider = GameObject.Find("SizeSlider").GetComponent<Slider>();
        sizeSlider.value = Globals.GetBallSize(); 

        speedSlider = GameObject.Find("SpeedSlider").GetComponent<Slider>();
        speedSlider.value = Globals.GetBallSpeed();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    Slider sizeSlider; 
    Slider speedSlider;

    /// <summary>
    /// Set ball texture to space theme
    /// </summary>
    public void SetBallSpace()
    {
        Globals.ballTexName = "hubble";
    }

    /// <summary>
    /// Set ball texture to smiley face theme
    /// </summary>
    public void SetBallSmiley()
    {
        Globals.ballTexName = "smiley";
    }
    /// <summary>
    /// Set ball texture to marble theme
    /// </summary>
    public void SetBallMarble()
    {
        Globals.ballTexName = "marble";
    }
    /// <summary>
    /// Sets Ball Size (radius of ball)
    /// </summary>
    /// <param name="newSize">floating point number (input from slider)</param>
    public void SetBallSize(float newSize)
    {
        Globals.ballSize = newSize / 3;
    }
    /// <summary>
    /// Sets Ball Speed
    /// </summary>
    /// <param name="newSpeed">floating point number (input from slider)</param>
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

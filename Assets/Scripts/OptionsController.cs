using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Allows options to be set such as player ball texture/theme, ball size, and ball speed
/// Loads current settings from Globals and sets them in the scene when it is loaded
/// </summary>
public class OptionsController : MonoBehaviour
{
    Slider sizeSlider; 
    Slider speedSlider;

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

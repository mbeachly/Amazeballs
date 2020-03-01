using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFinder : MonoBehaviour
{
    public GameObject PlayerObj;

    // Start is called before the first frame update
    void Start()
    {
        //Texture2D hMap = Resources.Load("MazeTall") as Texture2D; // Test image
        Texture2D hMap = Globals.tex;

        // Sample pixels on intervals to avoid sampling 
        // an excessive number (>100) of pixels in a direction
        int pixelSkip = (int)Mathf.Ceil((float)hMap.width / 100f);
        int sampleSizeX = hMap.width / pixelSkip;
        int sampleSizeY = hMap.height / pixelSkip;

        // Scale pixel coordinates to screen
        float camSizeY = Camera.main.orthographicSize; // orthographicSize is half the screen height, 100 pixels per unit
        float camSizeX = camSizeY * Screen.width / Screen.height;
        float meshScaleX = ((float)camSizeX / (float)sampleSizeX) / 50; // (100 pixels per unit)/2 = 50
        float meshScaleY = ((float)camSizeY / (float)sampleSizeY) / 50;

        // Track blue areas as they are being scanned
        int blueCount = 0;
        int blueStartX = -1;
        int blueEndX = -1;
        int blueY = -1;
        
        // Record the widest blue area found so far
        int maxCount = 0;
        int maxStartX = -1;
        int maxEndX = -1;
        int maxY = -1;

        // Loop through all the pixels in the image
        // Sample pixels on intervals
        // Assign heights to vertices based on greyscale values of pixels
        for (int i = 0; i < sampleSizeX; i++) // X dimension
        {   
            for (int j = 0; j < sampleSizeY; j++) // Y dimension
            {   // Check if pixel is sufficiently blue
                if (hMap.GetPixel(i * pixelSkip, j * pixelSkip).b > 0.3f)
                {   // Is this the start of a new area?
                    if (blueCount == 0)
                    {
                        blueY = j;
                        blueStartX = i;
                    }
                    // Increment count of blue area width
                    blueCount = blueCount + pixelSkip;
                    blueEndX = i;

                    // Is this the widest blue area found so far?
                    if (blueCount > maxCount)
                    {   // Record this as the widest blue area
                        maxCount = blueCount;
                        maxStartX = blueStartX;
                        maxEndX = blueEndX;
                        maxY = blueY;
                    }
                }
                else
                {   // Reset count
                    blueCount = 0;
                    blueStartX = -1;
                    blueEndX = -1;
                    blueY = -1;
                }
            }
        }

        // Was a blue area found?
        if (maxCount > 0)
        {
            float centerY = pixelSkip * meshScaleY * (float)maxY;
            float centerX = pixelSkip * meshScaleX * ((float)maxStartX + (float)maxEndX) / 2;

            centerY = centerY + camSizeY;
            centerX = centerX - camSizeX;
            // Place start point
            Vector3 startPosition = new Vector3(centerX, 0f, centerY);
            GameObject player = Instantiate(PlayerObj, startPosition, Quaternion.identity) as GameObject;
        }
    }
}


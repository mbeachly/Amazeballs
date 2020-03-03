using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFinder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   // Check if autoDetect is enabled
        if (Globals.autoDetect)
        {   // Procede to automatically detect start and end points
            FindPoints();
        }
    }

    // Automatically detect start and end points by scanning
    // texture pixels for blue and red areas. If found,
    // store start and end coordinates in Globals and set 
    // flag to skip asking the user to place points.
    void FindPoints()
    {
        //Texture2D hMap = Resources.Load("MazeTall") as Texture2D; // Test image
        Texture2D hMap = Globals.tex;

        // Sample pixels on intervals to avoid sampling 
        // an excessive number (>100) of pixels in a direction
        int pixelSkip = (int)Mathf.Ceil((float)hMap.width / 100f);
        int sampleSizeX = hMap.width / pixelSkip;
        int sampleSizeZ = hMap.height / pixelSkip;

        // Scale pixel coordinates to screen
        float camSizeZ = 2 * Camera.main.orthographicSize; // orthographicSize is half the screen height
        float camSizeX = camSizeZ * Screen.width / Screen.height;

        // Track blue areas as they are being scanned
        int blueCount = 0;
        int blueStartZ = -1;
        int blueEndZ = -1;
        int blueX = -1;

        // Record the widest blue area found so far
        int maxBlueCount = 0;
        int maxBlueStartZ = -1;
        int maxBlueEndZ = -1;
        int maxBlueX = -1;

        // Track red areas as they are being scanned
        int redCount = 0;
        int redStartZ = -1;
        int redEndZ = -1;
        int redX = -1;

        // Record the widest red area found so far
        int maxRedCount = 0;
        int maxRedStartZ = -1;
        int maxRedEndZ = -1;
        int maxRedX = -1;

        // Coordinates of start and end points
        float startX;
        float startZ;
        float endX;
        float endZ;

        // Store color of a pixel
        Color pixel;

        // Loop through all the pixels in the image
        // Sample pixels on intervals
        // Assign heights to vertices based on greyscale values of pixels
        for (int i = 0; i < sampleSizeX; i++) // X dimension
        {   
            for (int j = 0; j < sampleSizeZ; j++) // Z dimension
            {   
                pixel = hMap.GetPixel(i * pixelSkip, j * pixelSkip);

                // Check if pixel is sufficiently blue
                if (pixel.r < 0.7 * pixel.b && pixel.g < pixel.b)
                {   // Is this the start of a new area?
                    if (blueCount == 0)
                    {
                        blueX = i;// j;
                        blueStartZ = j;// i;
                    }
                    // Increment count of blue area width
                    blueCount = blueCount + 1;
                    blueEndZ = j;// i;

                    // Is this the widest blue area found so far?
                    if (blueCount > maxBlueCount)
                    {   // Record this as the widest blue area
                        maxBlueCount = blueCount;
                        maxBlueStartZ = blueStartZ;
                        maxBlueEndZ = blueEndZ;
                        maxBlueX = blueX;
                    }
                }
                else
                {   // Reset count
                    blueCount = 0;
                }

                // Check if pixel is sufficiently red
                if (pixel.b < 0.7 * pixel.r && pixel.g < 0.7 * pixel.r)
                {   // Is this the start of a new area?
                    if (redCount == 0)
                    {
                        redX = i;// j;
                        redStartZ = j;// i;
                    }
                    // Increment count of blue area width
                    redCount = redCount + 1;
                    redEndZ = j;// i;

                    // Is this the widest blue area found so far?
                    if (redCount > maxRedCount)
                    {   // Record this as the widest blue area
                        maxRedCount = redCount;
                        maxRedStartZ = redStartZ;
                        maxRedEndZ = redEndZ;
                        maxRedX = redX;
                    }
                }
                else
                {   // Reset count
                    redCount = 0;
                }
            }
        }

        // Was a blue area found?
        if (maxBlueCount > 0)
        {   // Convert pixel coordinates to screen coordinates
            startX = (float)maxBlueX * camSizeX / sampleSizeX;
            startZ = ((float)maxBlueStartZ + (float)maxBlueEndZ) * camSizeZ / (sampleSizeZ * 2);

            // Adjust coordinates to center of screen
            // Assign to global static varible
            startX = startX - (camSizeX / 2);
            startZ = startZ - (camSizeZ / 2);

            Globals.startPosition = new Vector3(startX, 1f, startZ);

            Globals.pickStep = 1; // Don't ask user to pick start point

            // Was a red area found?
            if (maxRedCount > 0)
            {   // Convert pixel coordinates to screen coordinates
                endX = (float)maxRedX * camSizeX / sampleSizeX;
                endZ = ((float)maxRedStartZ + (float)maxRedEndZ) * camSizeZ / (sampleSizeZ * 2);

                // Adjust coordinates to center of screen
                // Assign to global static varible
                endX = endX - (camSizeX / 2);
                endZ = endZ - (camSizeZ / 2);
                Globals.endPosition = new Vector3(endX, 1f, endZ);

                Globals.pickStep = 2; // Don't ask user to pick start point
            }
        }
    }
}

// Place start point Icon
// GameObject startPoint = Instantiate(StartPoint, Globals.startPosition, Quaternion.Euler(90, 0, 0)) as GameObject;
// startPoint.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
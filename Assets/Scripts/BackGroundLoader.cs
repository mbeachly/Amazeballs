using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BackGroundLoader : MonoBehaviour
{
    public MeshRenderer ground;

    private void Start()
    {
        // Resize plane to fit the screen
        float camSizeZ = 2 * Camera.main.orthographicSize; // orthographicSize is half the screen height
        float camSizeX = camSizeZ * Screen.width / Screen.height;
        float planeSizeX = ground.bounds.size.x;
        float planeSizeZ = ground.bounds.size.z;
        ground.transform.localScale = new Vector3(camSizeX/planeSizeX, 1, camSizeZ/planeSizeZ);

        // Load the background plane texture from a specified jpeg or png file
        // Based on https://answers.unity.com/questions/541258/load-material-texture-from-file.html
        string filePath = Application.dataPath + Globals.backgroundFile;

        if (System.IO.File.Exists(filePath))
        {
            var bytes = System.IO.File.ReadAllBytes(filePath);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
        else
        {
    
            ground.material.mainTexture = Globals.tex;
        }
    }
}

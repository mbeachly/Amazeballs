using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoader : MonoBehaviour
{
    private void Start()
    {
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
    }
}

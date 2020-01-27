using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Go to next scene in build order
    // Based on Udemy course:
    // Complete C# Unity Developer 2D: Learn to Code Making Games
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Go to previous scene in build order
    public void LoadPrevScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex - 1);
    }
}

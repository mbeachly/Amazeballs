using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Based on Udemy 2D Unity course
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Go to next scene in build order
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

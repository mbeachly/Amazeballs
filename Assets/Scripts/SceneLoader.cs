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


    // Go to Main Menu
    public void LoadMainMenu()
    {   // Is there a better way to do this than hardcoding?
        SceneManager.LoadScene("MainMenu");
    }

    // Start the Game
    public void LoadGame()
    {   // Will the maze to load be controlled by global variable?
        SceneManager.LoadScene("MazeGame");
    }

    // Go to Instructions
    public void LoadInstructions()
    {   
        SceneManager.LoadScene("Instructions");
    }

    // Exit the application
    public void Quit()
    {
        Application.Quit();
    }
}

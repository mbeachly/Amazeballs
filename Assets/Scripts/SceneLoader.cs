using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    // Go to Main Menu
    public void LoadMainMenu()
    {   // Is there a better way to do this than hardcoding?
        SceneManager.LoadScene("MainMenu");
        GameController.control.Load();
    }

    public void LoadMainMenuFromMaze()
    {
        GameController.control.Save();
        SceneManager.LoadScene("MainMenu");
    }

    // Go to Capture Image
    public void LoadCaptureImageMainMenu()
    {
        GameController.control.Load();

        if (Globals.gameSaved == false)
            SceneManager.LoadScene("CaptureImage");
        else
        {
            SceneManager.LoadScene("LoadSavedMaze");
            Globals.inCaptureImage = true;
            Globals.inSelectMaze = false;
        }
    }

    public void LoadCaptureImage()
    {
        SceneManager.LoadScene("CaptureImage");
    }

    // Go to Select Maze
    public void LoadSelectMazeMainMenu()
    {
        GameController.control.Load();

        if (Globals.gameSaved == false)
            SceneManager.LoadScene("SelectMaze");
        else
        {
            SceneManager.LoadScene("LoadSavedMaze");
            Globals.inSelectMaze = true;
            Globals.inCaptureImage = false;
        }
    }

    public void CheckImageLoadScene()
    {
        if (Globals.inCaptureImage == true)
            SceneManager.LoadScene("CaptureImage");
        else if (Globals.inSelectMaze == true)
            SceneManager.LoadScene("SelectMaze");
    }

    public void SelectMaze()
    {
        SceneManager.LoadScene("SelectMaze");
    }

    // Start the Game
    public void LoadGame()
    {   // Will the maze to load be controlled by global variable?
        //SceneManager.LoadScene("MazeGame");
        SceneManager.LoadScene("MazeGame2");
    }

    public void LoadSavedGame()
    {
        // Skip start and endpoint selection
        Globals.pickStep = 2;
        Globals.playSavedGame = true;
        SceneManager.LoadScene("MazeGame2");
    }
    // Go to Instructions
    public void LoadInstructions()
    {   
        SceneManager.LoadScene("Instructions");
    }

    // Go to Options
    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    // Go to Win Scene
    public void LoadWin()
    {
        SceneManager.LoadScene("WinGame");
        GameController.control.Save();
    }

    // Exit the application
    public void Quit()
    {
        GameController.control.Save();
        Application.Quit();
    }
}

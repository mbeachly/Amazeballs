using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    SceneLoader scene;
    // Load win screne when ball triggers red plane
    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = new GameObject("SceneLoader");
        scene = gameObject.AddComponent<SceneLoader>();
        scene.LoadNextScene();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays music track on all scenes of the game unless disabled
/// Follows singleton design pattern so music is not interupted
/// by destroying and reinstantiating on every scene.
/// </summary>
public class MusicController : MonoBehaviour
{
    public static MusicController control;
    public AudioSource music;

    //Singleton design pattern, only have one instance of this game object
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayPauseMusic()
    {
        if (music.volume == 0)
        {
            music.volume = 1;
            music.Play();
        }
        else if (music.volume == 1)
        {
            music.volume = 0;
            music.Pause();
        }
    }
}

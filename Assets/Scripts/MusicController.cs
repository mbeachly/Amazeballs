using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

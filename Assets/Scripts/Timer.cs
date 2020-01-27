using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //This is a class to keep track of time since start of game,
    //updates time and displays minutes and seconds in Timer Text
    public Text timer;
    private float start;
    // Start is called before the first frame update
    void Start()
    {
        start = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time - start;
        float timeMinutes = (int)currentTime / 60;
        string displayMinutes = timeMinutes.ToString( );
        float timeSeconds = currentTime % 60;
        string displaySeconds = timeSeconds.ToString("F2");

        timer.text = displayMinutes + ":" + displaySeconds;
        //Set Globals timeText variable to current time it's taken
        Globals.timeText = timer.text;
    }
}

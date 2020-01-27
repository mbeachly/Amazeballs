using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStateTime : MonoBehaviour
{
    // Get and display Global Time text variable for final time
    public Text timer;
    private void Update() {
        timer.text = Globals.timeText;
    }

}

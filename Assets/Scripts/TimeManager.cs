using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Text timeText;
    private static float secondsLeft = 120F;

    public void Update() {
        secondsLeft -= Time.deltaTime;
        int secondsLeftRounded = (int) secondsLeft;
        timeText.text = "Time Left: " + secondsLeftRounded / 60 + ":" + secondsLeftRounded % 60;
    }
}

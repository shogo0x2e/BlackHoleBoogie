using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Text timeText;
    private static float secondsLeft = 120F;

    public void Update() {
        secondsLeft -= Time.deltaTime;

        if (secondsLeft <= 0) {
            ScoreManager.scoreCount = 0;
            secondsLeft = 120F;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        int secondsLeftRounded = (int)secondsLeft;
        timeText.text = "Time Left: " + secondsLeftRounded / 60 + ":" + (secondsLeftRounded % 60).ToString("D2");
    }
}

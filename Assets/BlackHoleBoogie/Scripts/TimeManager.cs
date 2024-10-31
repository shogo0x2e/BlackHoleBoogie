using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Text timeText;

    private const float gameDuration = 91F;
    public static float secondsLeft = gameDuration;

    public void Update() {
        if (BlackHole.paused) return; // TODO: Remove when menu is implemented
        
        secondsLeft -= Time.deltaTime;

        if (secondsLeft <= 0) {
            // ScoreManager.scoreCount = 0;
            // TutorialManager.tutorialStep = 0;
            // TutorialManager.tutorialStringText = "Quest: Punch an Asteroid!";
            // secondsLeft = gameDuration;
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 0;
        }

        int secondsLeftRounded = (int)secondsLeft;
        timeText.text = "Time Left: " + secondsLeftRounded / 60 + ":" + (secondsLeftRounded % 60).ToString("D2");
    }
}

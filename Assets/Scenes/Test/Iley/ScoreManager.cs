using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public Text scoreText;
    public static int scoreCount;

    public void Update() {
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}

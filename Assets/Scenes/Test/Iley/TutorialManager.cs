using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public Text tutorialText;
    public static string tutorialStringText = "Quest: Punch an Asteroid!";

    public static int tutorialStep = 0;

    public void Update() {
        tutorialText.text = tutorialStringText;
    }
}

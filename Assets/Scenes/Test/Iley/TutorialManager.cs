using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText;
    public static string tutorialStringText = "Quest: Punch an Asteroid!";

    public static int tutorialStep = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tutorialText.text = tutorialStringText;
    }
}

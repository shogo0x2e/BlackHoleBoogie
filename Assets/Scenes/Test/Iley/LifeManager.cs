using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {
    public Text lifeText;
    public static int lifeCount;

    public void Update() {
        lifeText.text = "Lives: " + Mathf.Round(lifeCount);
    }
}

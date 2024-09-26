using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoToSStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

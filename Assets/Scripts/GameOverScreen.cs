using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LiveMan;
    public void Setup(){
        gameObject.SetActive(true);
    }
    public void Restart(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

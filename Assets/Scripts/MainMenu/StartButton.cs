using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public int gameStartScene;
    // Start is called before the first frame update
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}

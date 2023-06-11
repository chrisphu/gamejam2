using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;
    public GameObject PauseMenuUI;
    private PlayerDeathAndTransitionController _playerDeathAndTransitionController;
    private void Start()
    {
        isPaused = false;
        _playerDeathAndTransitionController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeathAndTransitionController>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {
        Resume();
        _playerDeathAndTransitionController.StartFadeOutAndMove(false, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string SceneToLoad = string.Empty;
    public float TimeDelayBeforeTransition = 1.0f;

    private float _currentTime = 0.0f;
    private int _loadingState = 0;  // 0 = not loading, 1+ = loading

    private void Update()
    {
        if (_loadingState == 0)
        {
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime < TimeDelayBeforeTransition)
        {
            return;
        }

        _currentTime = 0.0f;

        if (_loadingState > 1)
        {
            return;
        }

        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
        _loadingState++;
    }

    public void LoadLevelMethod()
    {
        // SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
        _loadingState++;
        GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Animator>().SetTrigger("FadeOut");
    }
}

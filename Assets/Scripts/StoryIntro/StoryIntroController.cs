using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryIntroController : MonoBehaviour
{
    public Image TopLeftPanel;
    public Image BottomLeftPanel;
    public Image RightPanel;

    public float DelayBetweenPanels = 2.0f;
    public float TimeBetweenPanels = 5.0f;

    private float _currentTime = 0.0f;
    private int _currentState = 0;

    private void Start()
    {
        TopLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 0.0f);
        BottomLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 0.0f);
        RightPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 0.0f);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        float timeThreshold = 1.0f;

        if (_currentState % 2 == 0)
        {
            timeThreshold = DelayBetweenPanels;
        }
        else
        {
            timeThreshold = TimeBetweenPanels;
        }

        float i = Mathf.Clamp01(_currentTime / timeThreshold).EaseInOutQuad();

        if (_currentState == 1)
        {
            TopLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, i);
        }
        else if (_currentState == 3)
        {
            BottomLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, i);
        }
        else if (_currentState == 5)
        {
            RightPanel.color = SameColorChangeAlpha(TopLeftPanel.color, i);
        }
        else if (_currentState == 7)
        {
            TopLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 1.0f - i);
            BottomLeftPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 1.0f - i);
            RightPanel.color = SameColorChangeAlpha(TopLeftPanel.color, 1.0f - i);
        }

        if (_currentTime < timeThreshold)
        {
            return;
        }

        _currentTime = 0.0f;
        _currentState++;

        if (_currentState == 9)
        {
            SceneManager.LoadScene("Level01", LoadSceneMode.Single);
        }
    }

    private Color SameColorChangeAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.g, alpha);
    }
}

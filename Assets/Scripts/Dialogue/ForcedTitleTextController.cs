using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ForcedTitleTextController : MonoBehaviour
{
    public string TitleText = string.Empty;
    public float TimeBetweenLetters = 0.05f;
    public float TimeOnScreenAfterCompleted = 1.0f;

    private TextMeshProUGUI _titleTextTextMeshPro;
    private float _currentTimeBetweenLetters = 0.0f;
    private int _currentLetter = 0;
    private int _readingState = 0; // 0 = not reading; 1 = reading; 2 = text on screen; 3 = text erasing; 4 = done

    private void Start()
    {
        _titleTextTextMeshPro = transform.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_readingState == 0)
        {
            return;
        }

        if (_readingState == 1)
        {
            _currentTimeBetweenLetters += Time.deltaTime;

            if (_currentTimeBetweenLetters < TimeBetweenLetters)
            {
                return;
            }

            _titleTextTextMeshPro.text += TitleText[_currentLetter];
            _currentTimeBetweenLetters = 0.0f;
            _currentLetter++;

            if (_currentLetter >= TitleText.Length)
            {
                _readingState++;
            }
        }
        else if (_readingState == 2)
        {
            _currentTimeBetweenLetters += Time.deltaTime;

            if (_currentTimeBetweenLetters < TimeOnScreenAfterCompleted)
            {
                return;
            }

            _readingState++;
        }
        else if (_readingState == 3)
        {
            _currentTimeBetweenLetters += Time.deltaTime;

            if (_currentTimeBetweenLetters < TimeBetweenLetters)
            {
                return;
            }

            _titleTextTextMeshPro.text = _titleTextTextMeshPro.text.Substring(0, _titleTextTextMeshPro.text.Length - 1);
            _currentTimeBetweenLetters = 0.0f;

            if (_titleTextTextMeshPro.text == string.Empty)
            {
                _readingState++;
            }
        }
    }

    public void RunDialogue(string titleText = "")
    {
        ClearDialogue();

        if (titleText != string.Empty)
        {
            TitleText = titleText;
        }

        _currentLetter = 0;
        _currentTimeBetweenLetters = 0.0f;
        _readingState = 1;
    }

    public void ClearDialogue()
    {
        _currentLetter = 0;
        _readingState = 0;
        _titleTextTextMeshPro.text = string.Empty;
    }
}

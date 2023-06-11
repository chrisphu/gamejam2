using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class ChatBoxController : MonoBehaviour
{
    public string SpeakerName = string.Empty;
    public Color SpeakerNameColor = Color.white;
    [TextAreaAttribute(6, 6)] public string DialogueText = string.Empty;
    public float TimeBetweenLetters = 0.05f;

    public bool Speaking { get; private set; } = false;

    private TextMeshProUGUI _speakerNameTextMeshPro;
    private TextMeshProUGUI _dialogueTextTextMeshPro;
    private float _currentTimeBetweenLetters = 0.0f;
    private int _currentLetter = 0;

    private void Start()
    {
        _speakerNameTextMeshPro = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _dialogueTextTextMeshPro = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!Speaking)
        {
            return;
        }

        _speakerNameTextMeshPro.text = SpeakerName;
        _speakerNameTextMeshPro.color = SpeakerNameColor;
        _currentTimeBetweenLetters += Time.deltaTime;

        if (_currentTimeBetweenLetters < TimeBetweenLetters)
        {
            return;
        }

        _dialogueTextTextMeshPro.text += DialogueText[_currentLetter];
        _currentTimeBetweenLetters = 0.0f;
        _currentLetter++;

        if (_currentLetter >= DialogueText.Length)
        {
            Speaking = false;
        }
    }

    public void RunDialogue()
    {
        ClearDialogue();
        _currentTimeBetweenLetters = 0.0f;
        _currentLetter = 0;
        Speaking = true;
    }

    public void SkipDialogue()
    {
        Speaking = false;
        _speakerNameTextMeshPro.text = SpeakerName;
        _speakerNameTextMeshPro.color = SpeakerNameColor;
        _dialogueTextTextMeshPro.text = DialogueText;
    }

    public void ClearDialogue()
    {
        Speaking = false;
        _speakerNameTextMeshPro.text = string.Empty;
        _dialogueTextTextMeshPro.text = string.Empty;
    }
}

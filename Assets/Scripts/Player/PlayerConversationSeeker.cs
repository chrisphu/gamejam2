using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConversationSeeker : MonoBehaviour
{
    public float MaxSpeakingDistance = 1.0f;

    private ChatBoxController _chatBoxController;
    private SpeechBalloonController _speechBalloonController;
    private bool _interactKeyDebounce = false;
    private SpeakerInfo _currentSpeaker = null;
    private int _currentSpeakerDialogueTextLine = 0;

    private void Start()
    {
        _chatBoxController = GameObject.FindGameObjectWithTag("ChatBox").GetComponent<ChatBoxController>();
        _speechBalloonController = GameObject.FindGameObjectWithTag("SpeechBalloon").GetComponent<SpeechBalloonController>();
    }

    private void Update()
    {
        QuitConversationIfTooFar();

        SpeakerInfo nearestSpeaker = FindNearestSpeaker();

        if (nearestSpeaker != null)
        {
            _speechBalloonController.AttachTo = nearestSpeaker.transform;
        }
        else
        {
            _speechBalloonController.AttachTo = null;
        }

        if ((Input.GetAxis("Interact") > 0.0f) && !_interactKeyDebounce)
        {
            _interactKeyDebounce = true;

            if (_currentSpeaker == null)
            {
                if (nearestSpeaker == null)
                {
                    return;
                }

                _currentSpeaker = nearestSpeaker;
                _chatBoxController.SpeakerName = _currentSpeaker.SpeakerName;
                _chatBoxController.SpeakerNameColor = _currentSpeaker.SpeakerNameColor;
                _chatBoxController.DialogueText = _currentSpeaker.DialogueTextLines[0];
                _currentSpeakerDialogueTextLine = 0;
                _chatBoxController.RunDialogue();
            }
            else
            {
                if (_chatBoxController.Speaking)
                {
                    _chatBoxController.SkipDialogue();
                }
                else if (_currentSpeakerDialogueTextLine < (_currentSpeaker.DialogueTextLines.Length - 1))
                {
                    _currentSpeakerDialogueTextLine++;
                    _chatBoxController.DialogueText = _currentSpeaker.DialogueTextLines[_currentSpeakerDialogueTextLine];
                    _chatBoxController.RunDialogue();
                }
                else
                {
                    _currentSpeaker = null;
                    _chatBoxController.ClearDialogue();
                }
            }
        }
        else if (Input.GetAxis("Interact") == 0.0f)
        {
            _interactKeyDebounce = false;
        }
    }

    private void QuitConversationIfTooFar()
    {
        if (_currentSpeaker != null)
        {
            float distance = (transform.position - _currentSpeaker.transform.position).magnitude;

            if (distance > MaxSpeakingDistance)
            {
                _currentSpeaker = null;
                _chatBoxController.ClearDialogue();
            }
        }
    }

    private SpeakerInfo FindNearestSpeaker()
    {
        GameObject _speakers = GameObject.FindGameObjectWithTag("Speakers");

        if (_speakers == null)
        {
            return null;
        }

        if (_speakers.transform.childCount == 0)
        {
            return null;
        }

        Transform[] speakers = _speakers.transform.GetChildren();
        SpeakerInfo closestSpeaker = null;
        float closestDistance = MaxSpeakingDistance;

        foreach (Transform speaker in speakers)
        {
            float distance = (transform.position - speaker.transform.position).magnitude;

            if (distance < closestDistance)
            {
                closestSpeaker = speaker.GetComponent<SpeakerInfo>();
                closestDistance = distance;
            }
        }

        return closestSpeaker;
    }
}

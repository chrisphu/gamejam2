using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBalloonController : MonoBehaviour
{
    public Transform AttachTo;
    public Sprite[] SpeechBalloonSprites = new Sprite[1];
    public float TimeBetweenFrames = 1.0f;

    private SpriteRenderer _spriteRenderer;
    private float _currentTimeBetweenFrames = 0.0f;
    private int _currentFrame = 0;
    private float _speechBalloonBobbleValue = 0.0f;

    void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        BobbleUpAndDown();
        AnimateFrames();
    }


    private void BobbleUpAndDown()
    {
        _speechBalloonBobbleValue += Time.deltaTime;

        if (AttachTo != null)
        {
            transform.position = AttachTo.position + new Vector3(1.5f, 3.0f, 0.0f) +
                Vector3.up * 0.1f * Mathf.Sin(_speechBalloonBobbleValue * 2.0f * Mathf.PI);
        }
        else
        {
            transform.position = Vector3.one * 100000.0f;
        }
    }

    private void AnimateFrames()
    {
        _currentTimeBetweenFrames += Time.deltaTime;

        if (_currentTimeBetweenFrames < TimeBetweenFrames )
        {
            return;
        }

        _currentTimeBetweenFrames = 0.0f;
        _currentFrame = (_currentFrame + 1) % SpeechBalloonSprites.Length;
        _spriteRenderer.sprite = SpeechBalloonSprites[_currentFrame];
    }
}

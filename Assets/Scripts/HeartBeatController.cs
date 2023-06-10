using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartBeatController : MonoBehaviour
{
    public bool Beating = true;
    public float BeatsPerMinute = 60.0f;
    public int BeatsPerMeasure = 4;

    public UnityEvent OnHeartDownBeat;
    public UnityEvent OnHeartNormalBeat;

    private AudioSource _audioSource;
    private float _currentBeatTime = 0.0f;
    private int _currentBeat = 0;

    void Start()
    {
        _audioSource = transform.GetComponent<AudioSource>();
        _currentBeat = BeatsPerMeasure - 1;
    }

    void Update()
    {
        if (Beating)
        {
            _currentBeatTime += Time.deltaTime;

            if (_currentBeatTime >= 1.0f / (BeatsPerMinute / 60.0f))
            {
                _currentBeatTime = 0.0f;
                _currentBeat = (_currentBeat + 1) % BeatsPerMeasure;

                if (_currentBeat == 0)
                {
                    OnHeartDownBeat.Invoke();
                }
                else
                {
                    OnHeartNormalBeat.Invoke();
                }

                _audioSource.Play();
            }
        }
    }

    public void ResetBeat()
    {
        _currentBeatTime = 0.0f;
        _currentBeat = BeatsPerMeasure - 1;
    }
}

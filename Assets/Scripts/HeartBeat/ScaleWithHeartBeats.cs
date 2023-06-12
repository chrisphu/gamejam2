using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithHeartBeats : MonoBehaviour
{
    [Header("Appearance")]
    public float DefaultScale = 1.0f;
    public float ScaleFactorOnDownBeat = 1.25f;
    public float ScaleFactorOnNormalBeat = 1.125f;
    public float TimeToScaleBackDown = 0.25f;

    private float _currentTimeScalingBackDown = 0.0f;
    private float _scaleFactor = 1.0f;

    void Start()
    {
        _currentTimeScalingBackDown = TimeToScaleBackDown;

        HeartBeatController heartBeatController = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
        heartBeatController.OnHeartDownBeat.AddListener(StartScalingDownBeat);
        heartBeatController.OnHeartNormalBeat.AddListener(StartScalingNormalBeat);
    }

    void Update()
    {
        _currentTimeScalingBackDown += Time.deltaTime;

        float i = Mathf.Clamp01(_currentTimeScalingBackDown / TimeToScaleBackDown).EaseInOutQuad();
        transform.localScale = Vector3.Lerp(Vector3.one * DefaultScale * _scaleFactor, Vector3.one * DefaultScale, i);
    }

    private void StartScalingDownBeat()
    {
        StartScaling(ScaleFactorOnDownBeat);
    }

    private void StartScalingNormalBeat()
    {
        StartScaling(ScaleFactorOnNormalBeat);
    }

    private void StartScaling(float scaleFactor)
    {
        _currentTimeScalingBackDown = 0.0f;
        _scaleFactor = scaleFactor;
    }
}

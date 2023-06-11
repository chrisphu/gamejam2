using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutsceneTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public int DefaultCameraPriority = 9;
    public int CutsceneCameraPriority = 11;
    public float CutsceneTime = 1.0f;

    private float _currentCutsceneTime = 0.0f;
    private int _triggerState = 0;

    private void Update()
    {
        if (_triggerState != 1)
        {
            return;
        }

        _currentCutsceneTime += Time.deltaTime;

        if (_currentCutsceneTime < CutsceneTime)
        {
            return;
        }

        _triggerState++;
        VirtualCamera.Priority = DefaultCameraPriority;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_triggerState == 0)
        {
            _triggerState = 1;
            VirtualCamera.Priority = CutsceneCameraPriority;
        }
    }
}

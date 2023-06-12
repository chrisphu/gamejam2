using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CutsceneTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public int DefaultCameraPriority = 9;
    public int CutsceneCameraPriority = 11;
    public float CutsceneTime = 1.0f;

    public UnityEvent OnCutSceneStart;

    private PlayerController _playerController;
    private float _currentCutsceneTime = 0.0f;
    private int _triggerState = 0;

    private void Start()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

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
        _playerController.PlayerInputLocked = false;
        VirtualCamera.Priority = DefaultCameraPriority;
        DestroyImmediate(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerController = collider.GetComponent<PlayerController>();

        if (_playerController == null)
        {
            return;
        }

        if (_triggerState == 0)
        {
            _triggerState = 1;
            _playerController.PlayerInputLocked = true;
            VirtualCamera.Priority = CutsceneCameraPriority;
            OnCutSceneStart.Invoke();
        }
    }
}

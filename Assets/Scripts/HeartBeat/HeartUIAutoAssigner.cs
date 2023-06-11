using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUIAutoAssigner : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = transform.GetComponent<Animator>();

        HeartBeatController heartBeatController = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
        heartBeatController.OnHeartDownBeat.AddListener(SendTriggerDownBeat);
        heartBeatController.OnHeartNormalBeat.AddListener(SendTriggerNormalBeat);
    }

    private void SendTriggerDownBeat()
    {
        SendTrigger("DownBeat");
    }

    private void SendTriggerNormalBeat()
    {
        SendTrigger("NormalBeat");
    }

    private void SendTrigger(string trigger)
    {
        _animator.SetTrigger(trigger);
    }
}

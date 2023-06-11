using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatModifier : MonoBehaviour
{
    public float RampTime = 0.0f;
    public float SpeedChangePercent = 0.0f;
    public float VignetteChange = 0.0f;
    public float ZoomChangePercent = 0.0f;

    private HeartBeatController _heartbeat;

    
    private Collider2D _collider2d;

    private void Start()
    {
        
        _collider2d = transform.GetComponent<Collider2D>();
        //_heartbeat = GameObject.FindWithTag("HeartBeat").GetComponent<HeartBeatController>();
        _heartbeat = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
    }

    private void ModifyHeartbeat()
    {
        _heartbeat.BeatsPerMinute = 200.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision2d)
    {
        PlayerController playerController = collision2d.collider.GetComponent<PlayerController>();

        if (playerController == null)
        {
            return;
        }

        ModifyHeartbeat();
    }
}

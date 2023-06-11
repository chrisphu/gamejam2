using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeartbeatModifier : MonoBehaviour
{
    public int BeatDuration = 4; //How many beats to stay at new modified time?
    public float SpeedChangePercent = 0.0f; //What percentage to change the new BPM to?
    public float VignetteChange = 0.0f; 
    public float ZoomChangePercent = 0.0f;

    private HeartBeatController _heartbeat;
    private CinemachineVirtualCamera _camera;

    private float _bpm;
    private float _durationDelay;
    private float _rampdownDelay;
    private float _newBPM;

    private float _camera_zoom;

    private IEnumerator _resetterCoroutine;
    private IEnumerator _setterCoroutine;

    private Collider2D _collider2d;

    private void Start()
    {
        
        _collider2d = transform.GetComponent<Collider2D>();
        //_heartbeat = GameObject.FindWithTag("HeartBeat").GetComponent<HeartBeatController>();
        _heartbeat = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
        _camera = GameObject.FindGameObjectWithTag("VirtualMainCamera").GetComponent<CinemachineVirtualCamera>();

        _bpm = _heartbeat.BeatsPerMinute;
        SpeedChangePercent /= 100;
        ZoomChangePercent /= 100;
        _camera_zoom = _camera.m_Lens.OrthographicSize;
    }
    private IEnumerator SetHeartBeat(float bpm,float zoom,float vignette,float delay)
    {
        yield return new WaitForSeconds(delay);
        _camera.m_Lens.OrthographicSize = _camera_zoom / zoom;
        _heartbeat.BeatsPerMinute = bpm;
    }
    private IEnumerator DeleteOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    private void ModifyHeartbeat()
    {
        _newBPM = _bpm * SpeedChangePercent;
        _setterCoroutine = SetHeartBeat(_newBPM, 1.15f,VignetteChange, 0);
        StartCoroutine(_setterCoroutine);
        float delay = 60 * BeatDuration / (_bpm * SpeedChangePercent);

        Vector3 dimensions = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = new Vector3(1.15f*dimensions.x, 1.15f*dimensions.y, 1);

        StartCoroutine(SetHeartBeat(_bpm,1f,1f,delay));
        StartCoroutine(DeleteOnDelay(delay+.05f));
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        if (playerController == null)
        {
            return;
        }

        ModifyHeartbeat();
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeartbeatModifier : MonoBehaviour
{
    public int BeatDuration = 4; //How many beats to stay at new modified time?
    public float SpeedChangePercent = 0.0f; //What percentage to change the new BPM to?
    public float ZoomChangePercent = 100.0f; //camera zoom change

    private HeartBeatController _heartbeat;
    private CinemachineVirtualCamera _camera;
    
    private PlayerController _player;

    private float _playerMaxMoveSpeed;
    private float _playerMoveSpeed;
    private float _playerDragSpeed;

    private float _bpm;
    private float _newBPM;

    private float _camera_zoom;

    private IEnumerator _setterCoroutine;


    private void Start()
    {
        
        //_heartbeat = GameObject.FindWithTag("HeartBeat").GetComponent<HeartBeatController>();
        _heartbeat = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
        _camera = GameObject.FindGameObjectWithTag("VirtualMainCamera").GetComponent<CinemachineVirtualCamera>();
        
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerMaxMoveSpeed = _player.MaxMoveSpeed;
        _playerMoveSpeed = _player.MoveSpeed;
        _playerDragSpeed = _player.DragSpeed;

        _bpm = _heartbeat.BeatsPerMinute;
        SpeedChangePercent /= 100;
        ZoomChangePercent /= 100;
        _camera_zoom = _camera.m_Lens.OrthographicSize;
    }
    private IEnumerator SetHeartBeat(float bpm,float zoom,float speedChangePercent, float delay)
    {
        yield return new WaitForSeconds(delay);
        _camera.m_Lens.OrthographicSize = _camera_zoom / zoom;
        _heartbeat.BeatsPerMinute = bpm;

        _player.MaxMoveSpeed = _playerMaxMoveSpeed*speedChangePercent;
        _playerMoveSpeed = _player.MoveSpeed * speedChangePercent;
        _playerDragSpeed = _player.DragSpeed * speedChangePercent;
    }
    private IEnumerator DeleteOnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
    private void ModifyHeartbeat()
    {
        _newBPM = _bpm * SpeedChangePercent;
        _setterCoroutine = SetHeartBeat(_newBPM, ZoomChangePercent, SpeedChangePercent, 0);
        StartCoroutine(_setterCoroutine);
        float delay = 60 * BeatDuration / (_bpm * SpeedChangePercent);

        Vector3 dimensions = this.gameObject.transform.localScale;
        this.gameObject.transform.localScale = new Vector3(1.15f*dimensions.x, 1.15f*dimensions.y, 1);


        StartCoroutine(SetHeartBeat(_bpm,1f, 1, delay));
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

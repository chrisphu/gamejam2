using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    public float TimeDelayBeforeFadeOut = 2.0f;
    public float TimeDelayBeforeMove = 2.0f;
    public float TimeDelayBeforeFadeIn = 0.1f;
    public float TimeDelayBeforePlayerCanMove = 2.0f;

    private PlayerController _playerController;
    private Rigidbody2D _rigidbody2d;
    private bool _playerDeathDebounce = false;
    private int _previousState = 0;
    private int _currentState = 0;
    private float _currentStateTime = 0.0f;
    private Vector3 _positionForPlayerAfterRespawn = Vector3.zero;

    private void Start()
    {
        _playerController = transform.GetComponent<PlayerController>();
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_previousState != _currentState)
        {
            Debug.Log(_currentState);
            _previousState = _currentState;
            _currentStateTime = 0.0f;

            if (_currentState == 2)
            {
                GameObject screenFade = GameObject.FindGameObjectWithTag("ScreenFade");

                if (screenFade != null)
                {
                    screenFade.GetComponent<Animator>().SetTrigger("FadeOut");
                }
            }
            else if (_currentState == 3)
            {
                transform.position = _positionForPlayerAfterRespawn;
            }
            else if (_currentState == 4)
            {
                GameObject screenFade = GameObject.FindGameObjectWithTag("ScreenFade");

                if (screenFade != null)
                {
                    screenFade.GetComponent<Animator>().SetTrigger("FadeIn");
                }
            }
            else if (_currentState == 5)
            {
                _playerController.PlayerInputLocked = false;
                _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                _playerDeathDebounce = false;
            }
        }
        else
        {
            _currentStateTime += Time.deltaTime;

            if (
                (_currentState == 1 && _currentStateTime > TimeDelayBeforeFadeOut) ||
                (_currentState == 2 && _currentStateTime > TimeDelayBeforeMove) ||
                (_currentState == 3 && _currentStateTime > TimeDelayBeforeFadeIn) ||
                (_currentState == 4 && _currentStateTime > TimeDelayBeforePlayerCanMove))
            {
                _currentState++;
            }
        }
    }

    public void StartFadeOutAndMove()
    {
        if (!_playerDeathDebounce)
        {
            _playerDeathDebounce = true;
            _playerController.PlayerInputLocked = true;
            _rigidbody2d.velocity = Vector3.zero;
            _rigidbody2d.bodyType = RigidbodyType2D.Kinematic;

            GameObject playerSpawn = GameObject.Find("PlayerSpawn");

            if (playerSpawn != null)
            {
                _positionForPlayerAfterRespawn = playerSpawn.transform.position;
            }
            else
            {
                _positionForPlayerAfterRespawn = Vector3.zero;
            }

            _previousState = 0;
            _currentState = 1;

            GameObject hurtSplash = GameObject.FindGameObjectWithTag("HurtSplash");

            if (hurtSplash != null)
            {
                hurtSplash.GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }
}

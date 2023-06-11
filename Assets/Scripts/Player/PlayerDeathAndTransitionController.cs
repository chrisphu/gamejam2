using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathAndTransitionController : MonoBehaviour
{
    [Header("Player death")]
    public float TimeDelayBeforeFadeOut = 2.0f;
    public float TimeDelayBeforeMove = 2.0f;
    public float TimeDelayBeforeFadeIn = 0.1f;
    public float TimeDelayBeforePlayerCanMove = 2.0f;

    [Header("Scene transition")]
    public float TimeDelayBeforeSceneChange = 2.0f;

    private Animator _screenFadeAnimator;
    private PlayerController _playerController;
    private Rigidbody2D _rigidbody2d;
    private bool _playerDeathAndTransitionDebounce = false;
    private int _previousState = 0;
    private int _currentState = 0;
    private float _currentStateTime = 0.0f;
    private Vector3 _positionForPlayerAfterRespawn = Vector3.zero;
    
    private string _sceneToTransitionTo = string.Empty;

    private void Start()
    {
        _screenFadeAnimator = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Animator>();
        _playerController = transform.GetComponent<PlayerController>();
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_previousState != _currentState)
        {
            _previousState = _currentState;
            _currentStateTime = 0.0f;

            // States 0 - 5 are for respawning
            if (_currentState == 2)
            {
                _screenFadeAnimator.SetTrigger("FadeOut");
            }
            else if (_currentState == 3)
            {
                transform.position = _positionForPlayerAfterRespawn;
            }
            else if (_currentState == 4)
            {
                _screenFadeAnimator.SetTrigger("FadeIn");
            }
            else if (_currentState == 5)
            {
                _playerController.PlayerInputLocked = false;
                _rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                _playerDeathAndTransitionDebounce = false;
            }

            // States 10 - 12 is for scene transitioning
            else if (_currentState == 11)
            {
                _screenFadeAnimator.SetTrigger("FadeOut");
            }
            else if (_currentState == 12)
            {
                SceneManager.LoadScene(_sceneToTransitionTo, LoadSceneMode.Single);
            }
        }
        else
        {
            _currentStateTime += Time.deltaTime;

            if (
                (_currentState == 1 && _currentStateTime > TimeDelayBeforeFadeOut) ||
                (_currentState == 2 && _currentStateTime > TimeDelayBeforeMove) ||
                (_currentState == 3 && _currentStateTime > TimeDelayBeforeFadeIn) ||
                (_currentState == 4 && _currentStateTime > TimeDelayBeforePlayerCanMove) ||
                (_currentState == 11 && _currentStateTime > TimeDelayBeforeSceneChange))
            {
                _currentState++;
            }
        }
    }

    public void StartFadeOutAndMove(bool playerDied = true, string sceneToTransitionTo = "")
    {
        if (!_playerDeathAndTransitionDebounce)
        {
            _playerDeathAndTransitionDebounce = true;
            _playerController.PlayerInputLocked = true;
            _rigidbody2d.velocity = Vector3.zero;
            _rigidbody2d.bodyType = RigidbodyType2D.Kinematic;

            if (playerDied)
            {
                _previousState = 0;
                _currentState = 1;

                GameObject playerSpawn = GameObject.Find("PlayerSpawn");

                if (playerSpawn != null)
                {
                    _positionForPlayerAfterRespawn = playerSpawn.transform.position;
                }
                else
                {
                    _positionForPlayerAfterRespawn = Vector3.zero;
                }

                GameObject hurtSplash = GameObject.FindGameObjectWithTag("HurtSplash");

                if (hurtSplash != null)
                {
                    hurtSplash.GetComponent<Animator>().SetTrigger("Hit");
                }
            }
            else
            {
                _previousState = 10;
                _currentState = 11;
                _sceneToTransitionTo = sceneToTransitionTo;
            }
        }
    }
}

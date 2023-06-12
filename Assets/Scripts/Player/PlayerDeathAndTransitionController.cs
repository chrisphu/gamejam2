using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathAndTransitionController : MonoBehaviour
{
    [Header("Player death")]
    public float TimeDelayBeforeFadeOut = 2.0f;
    public float TimeDelayBeforeSceneReload = 2.0f;

    [Header("Scene transition")]
    public float TimeDelayBeforeSceneChange = 2.0f;

    [Header("Audio")]
    public AudioClip DeathSound;

    public bool PlayerDeathAndTransitionDebounce { get; private set; } = false;

    private Animator _screenFadeAnimator;
    private PlayerController _playerController;
    private Rigidbody2D _rigidbody2d;
    private AudioSource _audioSource;
    private int _previousState = 0;
    private int _currentState = 0;
    private float _currentStateTime = 0.0f;
    
    private string _sceneToTransitionTo = string.Empty;

    private void Start()
    {
        _screenFadeAnimator = GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Animator>();
        _playerController = transform.GetComponent<PlayerController>();
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _audioSource = transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_previousState != _currentState)
        {
            _previousState = _currentState;
            _currentStateTime = 0.0f;

            // States 0 - 3 are for respawning
            if (_currentState == 2)
            {
                _screenFadeAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                _screenFadeAnimator.SetTrigger("FadeOut");
            }
            else if (_currentState == 3)
            {
                // transform.position = _positionForPlayerAfterRespawn;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }

            // States 10 - 12 is for scene transitioning
            else if (_currentState == 11)
            {
                _screenFadeAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                _screenFadeAnimator.SetTrigger("FadeOut");
            }
            else if (_currentState == 12)
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(_sceneToTransitionTo, LoadSceneMode.Single);
            }
        }
        else
        {
            _currentStateTime += Time.unscaledDeltaTime;

            if (
                (_currentState == 1 && _currentStateTime > TimeDelayBeforeFadeOut) ||
                (_currentState == 2 && _currentStateTime > TimeDelayBeforeSceneReload) ||
                (_currentState == 11 && _currentStateTime > TimeDelayBeforeSceneChange))
            {
                _currentState++;
            }
        }
    }

    public void StartFadeOutAndMove(bool playerDied = true, bool sceneReload = true, string sceneToTransitionTo = "")
    {
        if (!PlayerDeathAndTransitionDebounce)
        {
            PlayerDeathAndTransitionDebounce = true;
            _playerController.PlayerInputLocked = true;
            _rigidbody2d.velocity = Vector3.zero;
            _rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
            Time.timeScale = 0.0f;

            if (playerDied)
            {
                GameObject hurtSplash = GameObject.FindGameObjectWithTag("HurtSplash");

                if (hurtSplash != null)
                {
                    hurtSplash.GetComponent<Animator>().SetTrigger("Hit");
                }

                if (DeathSound != null)
                {
                    _audioSource.PlayOneShot(DeathSound);
                }
            }

            if (sceneReload)
            {
                _previousState = 0;
                _currentState = 1;
            }
            else
            {
                _previousState = 10;
                _currentState = 11;
                _sceneToTransitionTo = sceneToTransitionTo;
                Debug.Log("Starting transition to scene " + _sceneToTransitionTo);
            }
        }
    }
}

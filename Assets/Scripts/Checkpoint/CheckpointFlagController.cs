using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointFlagController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;

    private void Start()
    {
        gameObject.name += SceneManager.GetActiveScene().name;

        _particleSystem = transform.GetComponent<ParticleSystem>();
        _audioSource = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        if (playerController == null)
        {
            return;
        }

        GameObject checkpointInfoGameObject = GameObject.FindGameObjectWithTag("CheckpointInfo");

        if (checkpointInfoGameObject == null)
        {
            return;
        }

        CheckpointInfo checkpointInfo = checkpointInfoGameObject.GetComponent<CheckpointInfo>();

        if (checkpointInfo.CheckpointName == gameObject.name)
        {
            return;
        }

        Debug.Log("Checkpoint reached!");
        checkpointInfo.CheckpointName = gameObject.name;
        _particleSystem.Emit(6);
        _audioSource.Play();
    }
}

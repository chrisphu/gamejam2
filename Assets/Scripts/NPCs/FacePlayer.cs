using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float xDirectionTowardsPlayer = Mathf.Sign(_player.position.x - transform.position.x);
        transform.localEulerAngles = new Vector3(0.0f, ((xDirectionTowardsPlayer < 0.0f) ? 180.0f : 0.0f), 0.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoppingEnemyController : MonoBehaviour
{
    public float HopForwardForce = 1.0f;
    public float HopUpwardForce = 1.0f;
    public float MaxDistanceThresholdForChasing = 1.0f;

    Rigidbody2D _rigidbody2d;
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public void HopAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return;
        }

        float distance = (player.transform.position - transform.position).magnitude;

        if (distance > MaxDistanceThresholdForChasing)
        {
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController.PlayerInputLocked)
        {
            return;
        }

        float xDirectionTowardsPlayer = Mathf.Sign(player.transform.position.x - transform.position.x);
        _rigidbody2d.AddForce(new Vector2(xDirectionTowardsPlayer * HopForwardForce, HopUpwardForce), ForceMode2D.Impulse);
        _spriteRenderer.flipX = (xDirectionTowardsPlayer < 0.0f);
    }
}

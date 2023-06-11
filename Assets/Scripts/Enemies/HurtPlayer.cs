using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private Collider2D _collider2d;
    private Collider2D _playerCollider2d;
    private PlayerDeathAndTransitionController _playerDeathController;

    private void Start()
    {
        _collider2d = transform.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision2d)
    {
        PlayerDeathAndTransitionController playerDeathController = collision2d.collider.GetComponent<PlayerDeathAndTransitionController>();

        if (playerDeathController != null)
        {
            playerDeathController.StartFadeOutAndMove();
        }
    }
}

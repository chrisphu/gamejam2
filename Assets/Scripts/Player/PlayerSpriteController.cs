using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Animator _animator;

    void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0.0f)
        {
            _animator.SetBool("Walking", false);
        }
        else
        {
            _animator.SetBool("Walking", true);
            _spriteRenderer.flipX = (Input.GetAxis("Horizontal") < 0.0f);
        }
    }
}

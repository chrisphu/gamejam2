using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteFlipping : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _spriteRenderer.flipX = (Input.GetAxis("Horizontal") < 0.0f);
    }
}

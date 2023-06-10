using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1.0f;

    private Rigidbody2D _rigidbody2d;

    void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody2d.AddForce(Vector2.right * MoveSpeed, ForceMode2D.Force);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float MaxMoveSpeed = 1.0f;

    private Rigidbody2D _rigidbody2d;

    void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 playerInput = Vector2.right * Input.GetAxis("Horizontal") * MoveSpeed;
        Vector2 newRigidBody2dVelocity = _rigidbody2d.velocity + playerInput * Time.fixedDeltaTime;

        if (Mathf.Abs(newRigidBody2dVelocity.x) < MaxMoveSpeed)
        {
            _rigidbody2d.velocity = newRigidBody2dVelocity;
        }
        else if (Mathf.Abs(_rigidbody2d.velocity.x) < MaxMoveSpeed)
        {
            _rigidbody2d.velocity = new Vector2(Mathf.Sign(_rigidbody2d.velocity.x) * MaxMoveSpeed, _rigidbody2d.velocity.y);
        }
    }
}

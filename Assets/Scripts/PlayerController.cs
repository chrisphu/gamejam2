using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public float MaxMoveSpeed = 1.0f;
    public float JumpForce = 1.0f;
    public float MaxCoyoteTime = 1.0f;
    public float DistanceToGround = 1.0f;

    private Rigidbody2D _rigidbody2d;
    private CapsuleCollider2D _capsuleCollider2d;

    private bool _grounded = true;
    private bool _doubleJumpAvailable = true;
    private float _coyoteTime = 0.0f;

    private int _ignorePlayerMask = ~(1 << 3);

    private void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _capsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        UpdatePlayerGroundedState();
        CheckJumpInput();
    }

    private void FixedUpdate()
    {
        CheckHorizontalInput();
    }

    private void UpdatePlayerGroundedState()
    {
        RaycastHit2D groundRaycastHit = Physics2D.CapsuleCast(transform.position, _capsuleCollider2d.size * 0.99f, _capsuleCollider2d.direction, 0.0f, -Vector2.up, DistanceToGround, _ignorePlayerMask);

        if (groundRaycastHit.collider != null)
        {
            _grounded = true;
            _doubleJumpAvailable = true;
            _coyoteTime = 0.0f;
        }
        else
        {
            if (_coyoteTime < MaxCoyoteTime)
            {
                _coyoteTime += Time.deltaTime;
            }
            else
            {
                _grounded = false;
            }
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded || _doubleJumpAvailable)
            {
                if (!_grounded)
                {
                    _doubleJumpAvailable = false;
                }

                _grounded = false;

                // _rigidbody2d.MovePosition((Vector2)transform.position + Vector2.up * DistanceToGround * 2.0f);
                _rigidbody2d.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void CheckHorizontalInput()
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

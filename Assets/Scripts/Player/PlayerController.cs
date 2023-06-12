using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [Header("Player input")]
    public bool PlayerInputLocked = false;

    [Header("Movement")]
    public float MoveSpeed = 1.0f;
    public float MaxMoveSpeed = 1.0f;
    public float DragSpeed = 1.0f;
    public float JumpForce = 1.0f;
    public float MaxCoyoteTime = 1.0f;
    public float DistanceToGround = 1.0f;

    [Header("Audio")]
    public AudioClip JumpSound;

    private Rigidbody2D _rigidbody2d;
    private CapsuleCollider2D _capsuleCollider2d;
    private Animator _animator;
    private AudioSource _audioSource;

    private bool _grounded = true;
    private bool _doubleJumpAvailable = true;
    private float _coyoteTime = 0.0f;

    private bool _jumpKeyDebounce = false;

    private int _ignoreEntitesAndConfinerMask = ~((1 << 3) | (1 << 6) | (1 << 8));
    private int _ignoreEntitesAndAndConfinerAndTilemapMask = ~((1 << 3) | (1 << 6) | (1 << 7) | (1 << 8));

    private void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _capsuleCollider2d = transform.GetComponent<CapsuleCollider2D>();
        _animator = transform.GetComponent<Animator>();
        _audioSource = transform.GetComponent<AudioSource>();

        FindCheckpoint();
    }

    private void FixedUpdate()
    {
        UpdatePlayerGroundedState();
        CheckHorizontalInput();
        CheckJumpInput();
    }

    private void FindCheckpoint()
    {
        GameObject[] checkpointInfoGameObjects = GameObject.FindGameObjectsWithTag("CheckpointInfo");

        if (checkpointInfoGameObjects.Length == 0)
        {
            return;
        }

        foreach (GameObject checkpointInfoGameObject in checkpointInfoGameObjects)
        {
            CheckpointInfo checkpointInfo = checkpointInfoGameObject.GetComponent<CheckpointInfo>();

            if (checkpointInfo == null)
            {
                continue;
            }

            if (checkpointInfo.CheckpointName == string.Empty)
            {
                continue;
            }

            GameObject foundCheckpointFlag = GameObject.FindGameObjectWithTag("CheckpointFlag");

            if (foundCheckpointFlag == null)
            {
                continue;
            }

            if (foundCheckpointFlag.name != checkpointInfo.CheckpointName)
            {
                continue;
            }

            transform.position = foundCheckpointFlag.transform.position + Vector3.up * 1.0f;
        }
    }

    private void UpdatePlayerGroundedState()
    {
        RaycastHit2D groundRaycastHit = Physics2D.CapsuleCast(
            transform.position,
            new Vector2(_capsuleCollider2d.size.x * 0.9f, _capsuleCollider2d.size.y),
            _capsuleCollider2d.direction,
            0.0f,
            -Vector2.up,
            DistanceToGround,
            _ignoreEntitesAndConfinerMask);

        RaycastHit2D groundRaycastHitIgnoringTilemap = Physics2D.CapsuleCast(
            transform.position,
            new Vector2(_capsuleCollider2d.size.x * 0.9f, _capsuleCollider2d.size.y),
            _capsuleCollider2d.direction,
            0.0f,
            -Vector2.up,
            DistanceToGround,
            _ignoreEntitesAndAndConfinerAndTilemapMask);

        if (groundRaycastHit.collider != null)
        {
            _grounded = true;
            _doubleJumpAvailable = true;
            _coyoteTime = 0.0f;

            if (groundRaycastHitIgnoringTilemap.collider != null)
            {
                transform.SetParent(groundRaycastHitIgnoringTilemap.transform);
            }
        }
        else
        {
            if (_coyoteTime < MaxCoyoteTime)
            {
                _coyoteTime += Time.fixedDeltaTime;
            }
            else
            {
                _grounded = false;
                transform.SetParent(null);
            }
        }

        _animator.SetBool("Grounded", _grounded);
    }

    private void CheckJumpInput()
    {
        if ((Input.GetAxis("Jump") > 0.0f) && !_jumpKeyDebounce)
        {
            _jumpKeyDebounce = true;

            if (!PlayerInputLocked && (_grounded || _doubleJumpAvailable))
            {
                if (!_grounded)
                {
                    _rigidbody2d.velocity -= Vector2.up * _rigidbody2d.velocity.y;
                    _doubleJumpAvailable = false;
                }

                _grounded = false;
                transform.SetParent(null);
                _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0.0f);
                _rigidbody2d.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

                if (JumpSound != null)
                {
                    _audioSource.PlayOneShot(JumpSound);
                }
            }
        }
        else if (Input.GetAxis("Jump") == 0.0f)
        {
            _jumpKeyDebounce = false;
        }
    }

    private void CheckHorizontalInput()
    {
        if (!PlayerInputLocked && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.0f)
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
        else
        {
            Vector2 dragForce = Vector2.right * -Mathf.Sign(_rigidbody2d.velocity.x) * DragSpeed;
            Vector2 newRigidBody2dVelocity = _rigidbody2d.velocity + dragForce * Time.fixedDeltaTime;

            if (Mathf.Abs(_rigidbody2d.velocity.x) < (dragForce * Time.fixedDeltaTime).magnitude)
            {
                _rigidbody2d.velocity = new Vector2(0.0f, _rigidbody2d.velocity.y);
            }
            else
            {
                _rigidbody2d.velocity = newRigidBody2dVelocity;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public Vector2[] Positions = new Vector2[2];
    public float TimeToMove = 1.0f;

    private Rigidbody2D _rigidbody2d;
    private float _currentMoveTime = 0.0f;
    private int _currentDesiredPositionNumber = 0;

    private void Start()
    {
        _rigidbody2d = transform.GetComponent<Rigidbody2D>();
        _currentMoveTime = TimeToMove;
    }

    private void FixedUpdate()
    {
        if (Positions.Length >= 2)
        {
            _currentMoveTime += Time.fixedDeltaTime;
            int startPositionNumber = _currentDesiredPositionNumber - 1;

            if (startPositionNumber < 0)
            {
                startPositionNumber = Positions.Length - 1;
            }

            float i = Mathf.Clamp01(_currentMoveTime / TimeToMove).EaseInOutQuad();
            // _rigidbody2d.MovePosition(Vector2.Lerp(Positions[startPositionNumber], Positions[_currentDesiredPositionNumber], i));
            transform.position = Vector2.Lerp(Positions[startPositionNumber], Positions[_currentDesiredPositionNumber], i);
        }
    }

    public void Move()
    {
        _currentMoveTime = 0.0f;
        _currentDesiredPositionNumber = (_currentDesiredPositionNumber + 1) % Positions.Length;
    }
}

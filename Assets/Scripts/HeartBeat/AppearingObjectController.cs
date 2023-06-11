using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingObjectController : MonoBehaviour
{
    [Header("Listening")]
    public bool AutoAddSelfToDownBeat = true;

    [Header("Behavior")]
    [SerializeField] private bool _appearing = true;
    [Range(0.0f, 255.0f)] public float DisappearedAlpha = 0.0f;

    private Collider2D _collider2d;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _collider2d = transform.GetComponent<Collider2D>();
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();

        UpdateAppearance();

        if (AutoAddSelfToDownBeat)
        {
            HeartBeatController heartBeatController = GameObject.FindGameObjectWithTag("HeartBeat").GetComponent<HeartBeatController>();
            heartBeatController.OnHeartDownBeat.AddListener(ToggleAppearance);
        }
    }

    public void ToggleAppearance()
    {
        _appearing = !_appearing;
        UpdateAppearance();
    }

    private void UpdateAppearance()
    {
        if (_collider2d != null)
        {
            _collider2d.enabled = _appearing;
        }

        _spriteRenderer.color = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            (_appearing ? 1.0f : (DisappearedAlpha / 255.0f)));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EasingExtensions
{
    public static float EaseInOutQuad(this float x)
    {
        return (x < 0.5f) ? (2.0f * Mathf.Pow(x, 2.0f)) : (1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f) / 2.0f);
    }
}

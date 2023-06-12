using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    private bool _triggerDebounce = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        if (playerController == null)
        {
            return;
        }

        if (_triggerDebounce)
        {
            return;
        }

        _triggerDebounce = true;
        StartCoroutine(FadeAlpha(GetComponent<SpriteRenderer>(), 2f));
    }

    IEnumerator FadeAlpha(SpriteRenderer renderer, float duration)
    {
        Color startColor = renderer.color;
        Color endColor = new Color(startColor.r,startColor.g, startColor.b,0);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, time/duration);
            yield return null;
        }
    }
}

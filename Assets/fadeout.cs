using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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

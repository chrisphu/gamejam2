using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBalloonController : MonoBehaviour
{
    public Transform AttachTo;

    private float _speechBalloonBobbleValue = 0.0f;

    void Update()
    {
        BobbleUpAndDown();
    }

    private void BobbleUpAndDown()
    {
        _speechBalloonBobbleValue += Time.deltaTime;

        if (AttachTo != null)
        {
            transform.position = AttachTo.position + new Vector3(1.5f, 2.5f, 0.0f) +
                Vector3.up * 0.1f * Mathf.Sin(_speechBalloonBobbleValue * 2.0f * Mathf.PI);
        }
        else
        {
            transform.position = Vector3.one * 100000.0f;
        }
    }
}

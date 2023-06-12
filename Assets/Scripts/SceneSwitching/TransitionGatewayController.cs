using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionGatewayController : MonoBehaviour
{
    public string SceneToTransitionTo = string.Empty;

    private void Start()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        PlayerDeathAndTransitionController playerDeathController = otherCollider.GetComponent<PlayerDeathAndTransitionController>();

        if (playerDeathController == null)
        {
            return;
            
        }

        if (Application.CanStreamedLevelBeLoaded(SceneToTransitionTo))
        {
            Debug.Log("Found!");
            playerDeathController.StartFadeOutAndMove(false, false, SceneToTransitionTo);
        }
        else
        {
            Debug.Log("Scene named " + SceneToTransitionTo + " cannot be loaded or does not exist.");
        }        
    }
}

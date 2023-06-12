using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInfo : MonoBehaviour
{
    public string CheckpointName = string.Empty;

    private void Start()
    {
        CheckForOtherInstances();
    }

    private void CheckForOtherInstances()
    {
        GameObject[] checkpointInfoObjects = GameObject.FindGameObjectsWithTag("CheckpointInfo");
        bool foundOtherCheckpoint = false;

        foreach (GameObject checkpointInfoObject in checkpointInfoObjects)
        {
            if (checkpointInfoObject != gameObject)
            {
                foundOtherCheckpoint = true;
                break;
            }
        }

        if (foundOtherCheckpoint)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}

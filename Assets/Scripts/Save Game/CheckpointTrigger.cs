using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Checkpoints.Instance.SaveCheckpoint(checkpointId);
            Debug.Log($"Checkpoint {checkpointId} saved!");
        }
    }
}

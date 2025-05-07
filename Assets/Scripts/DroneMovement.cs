using System.Collections;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public Transform[] checkpoints;
    public float moveSpeed = 5f;
    public float reachThreshold = 0.5f;

    private int currentCheckpointIndex = 0;

    void Update()
    {
        if (checkpoints.Length == 0) return;

        Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;

        // Move towards the current checkpoint
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if the checkpoint is reached
        if (Vector3.Distance(transform.position, targetCheckpoint.position) < reachThreshold)
        {
            currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
        }
    }
}

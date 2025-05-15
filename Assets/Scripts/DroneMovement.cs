using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public Transform[] checkpoints;
    public float moveSpeed = 5f;
    public float reachThreshold = 0.5f;

    private int currentCheckpointIndex = 0;
    private bool hasGeneratedNewPoints = false;
    private bool loopingBetweenFinalPoints = false;
    private List<Transform> checkpointList;

    void Start()
    {
        checkpointList = new List<Transform>(checkpoints);
    }

    void Update()
    {
        if (checkpointList.Count == 0 || currentCheckpointIndex >= checkpointList.Count) return;

        Transform targetCheckpoint = checkpointList[currentCheckpointIndex];
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetCheckpoint.position) < reachThreshold)
        {
            currentCheckpointIndex++;

            // If we've reached the last original checkpoint and haven't generated new points yet
            if (currentCheckpointIndex == checkpoints.Length && !hasGeneratedNewPoints)
            {
                GenerateAndLoopNewPoints(checkpointList[checkpointList.Count - 1]);
                hasGeneratedNewPoints = true;
                loopingBetweenFinalPoints = true;
                currentCheckpointIndex = checkpointList.Count - 2; // Start looping at new points
            }

            // Loop between last two points if we're in that phase
            if (loopingBetweenFinalPoints && currentCheckpointIndex >= checkpointList.Count)
            {
                // Loop only between the last two points
                currentCheckpointIndex = checkpointList.Count - 2;
            }
        }
    }

    void GenerateAndLoopNewPoints(Transform lastCheckpoint)
    {
        Vector3 lastPos = lastCheckpoint.position;

        float offsetXLeft = Random.Range(5f, 15f);
        float offsetXRight = Random.Range(5f, 15f);
        float offsetYLeft = Random.Range(-5f, 15f);
        float offsetYRight = Random.Range(-5f, 15f);

        Vector3 leftPoint = new Vector3(lastPos.x - offsetXLeft, lastPos.y + offsetYLeft, lastPos.z);
        Vector3 rightPoint = new Vector3(lastPos.x + offsetXRight, lastPos.y + offsetYRight, lastPos.z);

        checkpointList.Add(CreateCheckpoint(leftPoint, "LeftPoint"));
        checkpointList.Add(CreateCheckpoint(rightPoint, "RightPoint"));
    }

    Transform CreateCheckpoint(Vector3 position, string name)
    {
        GameObject checkpoint = new GameObject(name);
        checkpoint.transform.position = position;
        return checkpoint.transform;
    }
}

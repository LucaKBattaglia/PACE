using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DroneMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float reachThreshold = 0.5f;

    private int currentCheckpointIndex = 0;
    private bool hasGeneratedNewPoints = false;
    private bool loopingBetweenFinalPoints = false;
    private List<Transform> checkpointList = new List<Transform>();

    private Transform player;
    string fullName;

    void Start()
    {
        fullName = gameObject.name; // e.g., "DroneM2 (1)"
        AssignCheckpointsBasedOnDroneModel();
        player = GameObject.Find("Head").transform;
    }

    void Update()
    {
        if (checkpointList.Count == 0 || currentCheckpointIndex >= checkpointList.Count)
            return;

        Transform targetCheckpoint = checkpointList[currentCheckpointIndex];
        Vector3 direction = (targetCheckpoint.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetCheckpoint.position) < reachThreshold)
        {
            currentCheckpointIndex++;

            // Reached the final checkpoint for the first time
            if (currentCheckpointIndex == checkpointList.Count && !hasGeneratedNewPoints)
            {
                moveSpeed = Random.Range(0f, 4f);
                GenerateAndLoopNewPoints(checkpointList[checkpointList.Count - 1]);
                hasGeneratedNewPoints = true;
                loopingBetweenFinalPoints = true;
                currentCheckpointIndex = checkpointList.Count - 2;
            }

            // Loop between the two final points
            if (loopingBetweenFinalPoints && currentCheckpointIndex >= checkpointList.Count)
            {
                currentCheckpointIndex = checkpointList.Count - 2;
            }
        }
    }

    void AssignCheckpointsBasedOnDroneModel()
    {
        //THIS returns fullName anyway so it is irrelevant, waste of performance (Regex is expensive)
        //string modelName = Regex.Match(fullName, @"DroneM\d+").Value;
        /*if (string.IsNullOrEmpty(modelName))
        {
            Debug.LogWarning($"Drone '{fullName}' does not match naming convention 'DroneM#'");
            return;
        }*/


        //Check for fullName being null
        if (string.IsNullOrEmpty(fullName))
        {
            Debug.LogWarning($"Drone '{fullName}' has invalid name");
            return;
        }
        string modelNumberStr = Regex.Match(fullName, @"\d+").Value;
        //Check here for naming convention
        if (string.IsNullOrEmpty(modelNumberStr)) {
            Debug.LogWarning($"Drone '{fullName}' does not match naming convention 'DroneM#'");
            return;
        }
        if (!int.TryParse(modelNumberStr, out int modelNumber))
        {
            Debug.LogWarning($"Could not parse model number from '{fullName}'");
            return;
        }

        int startCheckpoint = (modelNumber - 1) * 4 + 1;

        for (int i = 0; i < 4; i++)
        {
            string checkpointName = $"Check Points ({startCheckpoint + i})";
            Debug.Log(checkpointName);
            GameObject checkpointObj = GameObject.Find(checkpointName);
            if (checkpointObj != null)
            {
                checkpointList.Add(checkpointObj.transform);
            }
            else
            {
               Debug.LogWarning($"Checkpoint '{checkpointName}' not found for {fullName}");
            }
        }
    }

    /// <summary>
    ///Generates a new point to the left or right of the drone in WORLD space 
    /// </summary>
    void GenerateAndLoopNewPoints(Transform lastCheckpoint)
    {
        Vector3 lastPos = lastCheckpoint.position;

        float offsetXLeft = Random.Range(5f, 10f);
        float offsetXRight = Random.Range(5f, 10f);

        Vector3 leftPoint = new Vector3(lastPos.x - offsetXLeft, lastPos.y, lastPos.z);
        Vector3 rightPoint = new Vector3(lastPos.x + offsetXRight, lastPos.y, lastPos.z);

        Debug.Log("Left point :" + leftPoint + "Right point :" + rightPoint);

        checkpointList.Add(CreateCheckpoint(leftPoint, "LeftPoint"));
        checkpointList.Add(CreateCheckpoint(rightPoint, "RightPoint"));
    }

    Transform CreateCheckpoint(Vector3 position, string name)
    {
        GameObject checkpoint = new GameObject(name);
        checkpoint.transform.position = position;

        // Make this checkpoint a child of the drone ... Not anymore it pursues a point endlessly.
        checkpoint.transform.SetParent(GameObject.Find("Check Points").transform);

        return checkpoint.transform;
    }
}

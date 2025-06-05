using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public float spawnInterval = 5f;
    public int maxToSpawn = 5;
    public int maxConcurrent = 3;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float skyboxRotationSpeed = 1.1f;

    [Header("Spawn Points")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform bossSpawnPoint;

    [Header("Drone Prefabs")]
    public GameObject droneM1;
    public GameObject droneM2;
    public GameObject droneM3;
    public GameObject boss;

    [Header("Stage Settings")]
    public List<Stage> stages = new List<Stage>();

    [Header("Safety Net Settings")]
    public Transform fallbackCheckpoint; // Assign in Inspector
    public float stageTimeout = 30f; // Time in seconds before redirect

    private int currentStage = 0;
    private int totalSpawned = 0;
    private int totalDestroyed = 0;
    private bool spawning = false;

    private List<GameObject> activeDrones = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(StageController());
    }

    IEnumerator StageController()
    {
        while (currentStage < stages.Count)
        {
            Stage stage = stages[currentStage];
            yield return StartCoroutine(SpawnStage(stage.spawnInterval, stage.maxToSpawn, stage.maxConcurrent));
            currentStage++;
        }

        SpawnBoss();
    }

    IEnumerator SpawnStage(float spawnInterval, int maxToSpawn, int maxConcurrent)
    {
        totalSpawned = 0;
        totalDestroyed = 0;
        spawning = true;
        float startTime = Time.time;
        bool fallbackTriggered = false;

        while (totalDestroyed < maxToSpawn)
        {
            activeDrones.RemoveAll(item => item == null);

            if (spawning && totalSpawned < maxToSpawn && activeDrones.Count < maxConcurrent)
            {
                SpawnDrone();
                totalSpawned++;
                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }

            totalDestroyed = totalSpawned - activeDrones.Count;

            // Trigger fallback behavior after timeout
            if (!fallbackTriggered && Time.time - startTime > stageTimeout)
            {
                fallbackTriggered = true;
                Debug.Log("Stage timeout reached. Redirecting all active drones.");

                foreach (GameObject drone in activeDrones)
                {
                    if (drone != null)
                    {
                        DroneMovement movement = drone.GetComponent<DroneMovement>();
                        if (movement != null)
                        {
                            movement.RedirectToFallback(fallbackCheckpoint);
                        }
                    }
                }
            }
        }

        spawning = false;
    }

    void SpawnDrone()
    {
        int choice = Random.Range(1, 4);
        GameObject droneToSpawn = null;
        Transform spawnPoint = null;

        switch (choice)
        {
            case 1:
                droneToSpawn = droneM1;
                spawnPoint = spawnPoint1;
                break;
            case 2:
                droneToSpawn = droneM2;
                spawnPoint = spawnPoint2;
                break;
            case 3:
                droneToSpawn = droneM3;
                spawnPoint = spawnPoint3;
                break;
        }

        GameObject newDrone = Instantiate(droneToSpawn, spawnPoint.position, spawnPoint.rotation);
        activeDrones.Add(newDrone);
    }

    void SpawnBoss()
    {
        Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
        Debug.Log("Boss Spawned!");
    }
}

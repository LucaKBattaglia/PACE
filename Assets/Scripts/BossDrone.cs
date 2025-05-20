using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrone : MonoBehaviour
{
    public GameObject baseballPrefab;
    public GameObject bigBallPrefab; // Assigned in Inspector
    public float attackRange = 20f;
    public float fireSpeed = 12f;
    public float spawnOffset = 2f;
    public float moveSpeed = 5f;
    public float attackInterval = 3f;

    private GameObject player;
    private float lastAttackTime;
    private int currentCheckpointIndex = 0;
    private List<Transform> checkpointList = new List<Transform>();
    private bool isAttacking = false;
    private bool hasGeneratedLoopPoints = false;

    void Start()
    {
        TryAssignPlayer();
        AssignBossCheckpoints();
    }

    void Update()
    {
        if (player == null)
        {
            TryAssignPlayer();
            return;
        }

        transform.LookAt(player.transform);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Movement only pauses during big shot
        if (!isAttacking && checkpointList.Count > 0)
        {
            Transform target = checkpointList[currentCheckpointIndex];
            Vector3 dir = (target.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                currentCheckpointIndex++;

                if (currentCheckpointIndex >= checkpointList.Count)
                {
                    if (!hasGeneratedLoopPoints)
                    {
                        GenerateLoopPoints(checkpointList[checkpointList.Count - 1].position);
                        moveSpeed = Random.Range(1f, 5f);
                        hasGeneratedLoopPoints = true;
                    }

                    currentCheckpointIndex = checkpointList.Count - 2;
                }
            }
        }

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackInterval)
        {
            StartCoroutine(PerformRandomAttack());
            lastAttackTime = Time.time;
        }
    }

    void TryAssignPlayer()
    {
        GameObject head = GameObject.Find("Head");
        if (head != null)
        {
            player = head;
        }
    }

    void AssignBossCheckpoints()
    {
        string[] checkpointNames = { "Check Points (13)", "Check Points (14)", "Check Points (15)" };

        foreach (string name in checkpointNames)
        {
            GameObject cp = GameObject.Find(name);
            if (cp != null)
            {
                checkpointList.Add(cp.transform);
            }
            else
            {
                Debug.LogWarning($"Boss checkpoint '{name}' not found.");
            }
        }
    }

    void GenerateLoopPoints(Vector3 center)
    {
        float offsetXLeft = Random.Range(5f, 10f);
        float offsetXRight = Random.Range(5f, 10f);

        Vector3 left = new Vector3(center.x - offsetXLeft, center.y, center.z);
        Vector3 right = new Vector3(center.x + offsetXRight, center.y, center.z);

        checkpointList.Add(CreateCheckpoint(left, "BossLeftLoopPoint"));
        checkpointList.Add(CreateCheckpoint(right, "BossRightLoopPoint"));
    }

    Transform CreateCheckpoint(Vector3 pos, string name)
    {
        GameObject point = new GameObject(name);
        point.transform.position = pos;
        point.transform.SetParent(transform);
        return point.transform;
    }

    IEnumerator PerformRandomAttack()
    {
        int attackType = Random.Range(0, 2); // 0 = big shot, 1 = triple shot

        if (attackType == 0)
        {
            isAttacking = true;
            yield return StartCoroutine(FireBigShot());
            isAttacking = false;
        }
        else
        {
            yield return StartCoroutine(FireTripleShot());
        }
    }

    IEnumerator FireBigShot()
    {
        yield return new WaitForSeconds(0.3f);

        Vector3 spawnPos = transform.position + transform.forward * spawnOffset;

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.pitch = 0.8f;
            audio.Play();
        }

        GameObject bigBall = Instantiate(bigBallPrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = bigBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * fireSpeed;
        }

        yield return new WaitForSeconds(1f); // Pause duration
    }

    IEnumerator FireTripleShot()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = transform.position + transform.forward * spawnOffset;

            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.pitch = Random.Range(0.95f, 1.05f);
                audio.Play();
            }

            GameObject ball = Instantiate(baseballPrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * fireSpeed;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

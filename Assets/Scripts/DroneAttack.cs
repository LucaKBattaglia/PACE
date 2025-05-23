using System.Collections;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject baseballPrefab;
    public float attackInterval = 2f;
    public float attackRange = 15f;
    [SerializeField] private float fireSpeed = 10f;
    [SerializeField] private float spawnOffset = 1.5f; // Distance in front of the drone to spawn the baseball

    private float lastAttackTime;

    void Start()
    {
        TryAssignPlayer();
    }

    void Update()
    {
        // Try to find player if not assigned yet (e.g. player spawned after this drone)
        if (player == null)
        {
            TryAssignPlayer();
            return;
        }

        // Always look at the player
        transform.LookAt(player.transform);

        // Attack if in range and cooldown elapsed
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackInterval)
        {
            Attack();
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

    void Attack()
    {
        Vector3 spawnPosition = transform.position + transform.forward * spawnOffset;

        //AUDIO EFFECTS
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.pitch = Random.Range(0.95f, 1.1f);
            audio.Play();
        }

        GameObject baseball = Instantiate(baseballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * fireSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

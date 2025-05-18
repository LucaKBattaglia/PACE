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

    void Update()
    {
        // Always look at the player
        transform.LookAt(player.transform);

        // Only attack if player is within range
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackInterval)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Calculate spawn position slightly in front of the drone
        Vector3 spawnPosition = transform.position + transform.forward * spawnOffset;

        GetComponent<AudioSource>().Play();
        GameObject baseball = Instantiate(baseballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * fireSpeed;
        }
    }

    // Draw the attack range as a wire sphere in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

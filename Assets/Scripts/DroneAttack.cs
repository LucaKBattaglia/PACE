using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject baseballPrefab;
    public float attackInterval = 2f; // Interval between attacks
    private float lastAttackTime;
    void Update()
    {
        transform.LookAt(player.transform); // Look at the player

        // Check if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackInterval)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // Spawn baseball projectile in the direction of the player
        GameObject baseball = Instantiate(baseballPrefab, transform.position, transform.rotation);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * 10f; // Adjust speed as needed
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BaseballDirectionTracker tracker;

    [SerializeField] private float batBoostFactor = 5f;
    [SerializeField] private float redirectForce = 10f;
    [SerializeField] private float minSpeed = 2.5f;
    [SerializeField] private string droneTag = "Drone";

    [Header("Homing Settings")]
    [SerializeField] private float homingRange = 7f;
    [SerializeField] private float homingStrength = 4f;

    private GameObject homingTarget;
    private bool homingActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tracker = GetComponent<BaseballDirectionTracker>();
    }

    void FixedUpdate()
    {
        if (homingActive && homingTarget != null)
        {
            float distance = Vector3.Distance(transform.position, homingTarget.transform.position);
            if (distance <= homingRange)
            {
                Vector3 toTarget = (homingTarget.transform.position - transform.position).normalized;
                rb.AddForce(toTarget * homingStrength, ForceMode.Force);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bat"))
        {
            var batEstimator = other.gameObject.GetComponent<BatVelocityEstimator>();
            if (batEstimator == null || tracker == null) return;

            ContactPoint contact = other.contacts[0];
            Vector3 contactPoint = contact.point;
            Vector3 batCenter = batEstimator.transform.position;

            Vector3 r = contactPoint - batCenter;
            Vector3 batPointVelocity = batEstimator.linearVelocity + Vector3.Cross(batEstimator.angularVelocity, r);

            // Find nearest drone
            homingTarget = FindNearestDrone(); // store for homing
            Vector3 toDroneDirection = Vector3.zero;
            if (homingTarget != null)
            {
                toDroneDirection = (homingTarget.transform.position - transform.position).normalized;
            }

            // Combine original shot direction and direction to drone
            Vector3 originalReverseDir = -tracker.originalDirection;
            Vector3 blendedDirection = (originalReverseDir + toDroneDirection).normalized;

            // Final velocity
            Vector3 redirection = blendedDirection * redirectForce;
            Vector3 finalVelocity = batPointVelocity * batBoostFactor + redirection;

            if (finalVelocity.magnitude < minSpeed)
            {
                finalVelocity = finalVelocity.normalized * minSpeed;
            }

            rb.velocity = finalVelocity;

            // Enable homing
            homingActive = true;
        }
    }

    GameObject FindNearestDrone()
    {
        GameObject[] drones = GameObject.FindGameObjectsWithTag(droneTag);
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var drone in drones)
        {
            float distance = Vector3.Distance(transform.position, drone.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = drone;
            }
        }

        return nearest;
    }
}

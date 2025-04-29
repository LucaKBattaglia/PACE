using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTargeting : MonoBehaviour
{
    public Transform targetDrone; // The drone to home in on
    public float curveDistance = 10f; // Distance within which the ball starts curving
    public float trackingDuration = 3f; // Time taken to adjust the trajectory
    private Rigidbody rb;
    private bool isCurving = false;
    private float curveStartTime;
    private Vector3 initialVelocity;
    private Vector3 targetDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (targetDrone == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, targetDrone.position);
        
        // If within curve distance and not already curving, start curving
        if (distanceToTarget <= curveDistance && !isCurving)
        {
            isCurving = true;
            curveStartTime = Time.time;
            initialVelocity = rb.velocity;
            targetDirection = (targetDrone.position - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        if (isCurving && targetDrone != null)
        {
            float elapsed = Time.time - curveStartTime;
            float t = Mathf.Clamp01(elapsed / trackingDuration); // Normalize time for smooth transition
            Vector3 newDirection = Vector3.Lerp(initialVelocity.normalized, targetDirection, t);
            rb.velocity = newDirection * rb.velocity.magnitude; // Maintain current speed while adjusting direction
        }
    }

}

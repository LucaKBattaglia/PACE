using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //private Vector3 speed = new Vector3();
    // Start is called before the first frame update

    private Rigidbody rb;
    private BatVelocityEstimator otherRb;
    private Vector3 newVelocity;

    [SerializeField] private float boostFactor = 5f;
    [SerializeField] private float minSpeed = 2.5f;

    [SerializeField] private float aimAssistAngle;
    [SerializeField] private float aimAssistDistance;
    [SerializeField] private float redirectStrength;

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.CompareTag("Bat")) {

    //         rb = GetComponent<Rigidbody>();
    //         otherRb = other.GetComponent<Rigidbody>();
    //         //newVelocity = Vector3.Cross(rb.velocity, otherRb.velocity);
    //         newVelocity = otherRb.tipVelocity;
    //         rb.velocity = newVelocity;
    //         //Debug.Log(otherRb.velocity);
    //     }
    // }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bat")) {
            var batEstimator = other.gameObject.GetComponent<BatVelocityEstimator>();
            if (batEstimator == null) return;

            ContactPoint contact = other.contacts[0];
            Vector3 contactPoint = contact.point;
            Vector3 batCenter = batEstimator.transform.position;

            Vector3 r = contactPoint - batCenter;
            Vector3 pointVelocity = batEstimator.linearVelocity + Vector3.Cross(batEstimator.angularVelocity, r);

            Rigidbody rb = GetComponent<Rigidbody>(); 
            Vector3 finalVelocity = pointVelocity * boostFactor;

            if(finalVelocity.magnitude < minSpeed){
                finalVelocity = finalVelocity.normalized * minSpeed;
            }

            GameObject nearestTarget = FindObjectInCone(contactPoint, finalVelocity.normalized, aimAssistAngle, aimAssistDistance);
            
            if(nearestTarget != null)
            {
                Vector3 toTarget = (nearestTarget.transform.position - contactPoint).normalized;
                finalVelocity = Vector3.Lerp(finalVelocity, toTarget * finalVelocity.magnitude, redirectStrength);
            }

            rb.velocity = finalVelocity;

            other.gameObject.GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().Play();
        }
    }

    private GameObject FindObjectInCone(Vector3 origin, Vector3 direction, float maxAngleDeg, float maxDistance) {
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");

        GameObject bestTarget = null;
        float closestDistance = Mathf.Infinity;
        float maxDot = Mathf.Cos(maxAngleDeg * Mathf.Deg2Rad);

        foreach (GameObject drone in drones)
        {
            Vector3 toDrone = (drone.transform.position - origin);
            float distance = toDrone.magnitude;
            if (distance > maxDistance) continue;

            Vector3 toDroneDir = toDrone.normalized;
            float dot = Vector3.Dot(direction, toDroneDir);

            if (dot > maxDot && distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = drone;
            }
        }
        return bestTarget;
    }
}

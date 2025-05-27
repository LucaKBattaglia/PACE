using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Text;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Devices.GearVR.Avatar;
using Liminal.SDK.VR.Utils;

public class BallMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BatVelocityEstimator otherRb;
    private Vector3 newVelocity;

    [SerializeField] private float boostFactor = 5f;
    [SerializeField] private float minSpeed = 2.5f;

    [SerializeField] private float aimAssistAngle;
    [SerializeField] private float aimAssistDistance;
    [SerializeField] private float redirectStrength;

    [SerializeField] private float ballRotationSpeed = 1f;
    private Vector3 rotationAxis;

    private void Start()
    {
        //calculate rotation axis
        rotationAxis = new Vector3(Random.value, Random.value, Random.value);
    }

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

            var device = VRDevice.Device;

            var rightHandInput = device.PrimaryInputDevice;
            var leftHandInput = device.SecondaryInputDevice;

            rightHandInput?.SendInputHaptics(frequency: .5f, amplitude: .5f, duration: 0.05f);

            //AUDIO EFFECTS
            //call Bat audio effect
            AudioSource batAudio = other.gameObject.GetComponent<AudioSource>();
            batAudio.pitch = Random.Range(0.95f, 1.2f);
            batAudio.reverbZoneMix = Random.Range(1f, 1.05f);
            batAudio.Play();
            Debug.Log(batAudio.pitch);

            //call this ball audio effect
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

    private void Update()
    {
        //Visually rotate the ball, doesn't affect direction (I think)
        transform.Rotate(rotationAxis, ballRotationSpeed * Time.deltaTime);
    }
}

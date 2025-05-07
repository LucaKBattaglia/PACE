using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVelocityEstimator : MonoBehaviour
{
    [SerializeField] private Transform tipTransform;

    public Vector3 linearVelocity { get; private set; }
    public Vector3 angularVelocity { get; private set; }
    public Vector3 tipVelocity { get; private set; }

    private Vector3 previousPosition;
    private Quaternion previousRotation;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        linearVelocity = (transform.position - previousPosition)/deltaTime;

        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        deltaRotation.ToAngleAxis(out float angleInDegrees, out Vector3 axis);
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        angularVelocity = (axis * angleInRadians)/ deltaTime;

        Vector3 r = tipTransform.position - transform.position;
        Vector3 rotationVelocity = Vector3.Cross(angularVelocity, r);
        tipVelocity = linearVelocity + rotationVelocity;

        previousPosition = transform.position;
        previousRotation = transform.rotation;

    }
}

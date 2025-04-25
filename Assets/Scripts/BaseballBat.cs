using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    public float hitForce = 10f; // Force applied to the baseball when hit

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Baseball")) // Assuming baseball has a tag "Baseball"
        {
            // Determine the direction to deflect the baseball
            Vector3 hitDirection = transform.right; // Default direction (right)
            
            // Determine if hit on left or right side of the bat
            if (other.transform.position.x < transform.position.x)
            {
                hitDirection = -transform.right; // Hit on the left, deflect left
            }

            // Apply force to the baseball in the hit direction
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = hitDirection * hitForce;
            }
        }
    }

}

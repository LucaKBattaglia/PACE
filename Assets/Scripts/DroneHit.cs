using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Baseball")) // Assuming baseball has a tag "Baseball"
        {
            Destroy(gameObject); // Destroy the drone
        }
    }
}

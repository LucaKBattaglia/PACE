using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //This is a double up, we already destroy and manage the collision in the BallDestroyer script
        /*if (other.CompareTag("Baseball")) // Assuming baseball has a tag "Baseball"
        {
            Destroy(gameObject); // Destroy the drone
            GameManager.Instance.droneHitCount++;
        }*/
    }
}

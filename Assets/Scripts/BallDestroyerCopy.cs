using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyerCopy : MonoBehaviour
{
    private float lifetime; // Adjustable time before the ball despawns

    private void Awake()
    {
        lifetime = GameManager.Instance.ballLifetime;   //Put lifetime in game manager for easy editing
    }

    void Start()
    {
        // Start coroutine to destroy the ball after a set time
        StartCoroutine(DestroyAfterTime());
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);                    // Destroy ball if player is hit
            GameManager.Instance.playerHitCount++;  // Add to player hit counter
        }

        else if (other.gameObject.CompareTag("Drone"))
        {
            Destroy(other.gameObject);              // Destroy drone
            Destroy(gameObject);                    // Destroy ball regardless
            GameManager.Instance.droneHitCount++;   // Add to drone hit count
        }
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        GameManager.Instance.ballsMissedCount++;
    }
}

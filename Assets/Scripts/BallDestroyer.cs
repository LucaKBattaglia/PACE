using System.Collections;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f; // Adjustable time before the ball despawns

    void Start()
    {
        // Start coroutine to destroy the ball after a set time
        StartCoroutine(DestroyAfterTime());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destroy ball if player is hit
        } 

        else if (other.gameObject.CompareTag("Drone"))
        {
            Destroy(other.gameObject); // Destroy drone
            Destroy(gameObject); // Destroy ball regardless
        }
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

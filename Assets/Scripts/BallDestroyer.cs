using System.Collections;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private float lifetime = 10f; // Adjustable time before the ball despawns
    [SerializeField] private AudioClip destructionSound; //Sound for when the drone is destroyed
    [SerializeField] private Transform particles;

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
            AudioSource.PlayClipAtPoint(destructionSound, other.transform.position);
            Destroy(other.gameObject); // Destroy drone
            Instantiate(particles, this.transform.position, Quaternion.identity); //Instantiate particle effect at death position
            Destroy(gameObject); // Destroy ball regardless
        }
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

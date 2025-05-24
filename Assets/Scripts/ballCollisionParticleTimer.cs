using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollisionParticleTimer : MonoBehaviour
{
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<ParticleSystem>().startLifetime;
        Debug.Log("LifeTime of particles" + timer);
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}

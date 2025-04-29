using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private bool BatHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bat")) 
        {
             BatHit = true;
        } else if (other.gameObject.CompareTag("Player")){
            //Do something 
        } else if (other.gameObject.CompareTag("Drone") && BatHit == true){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

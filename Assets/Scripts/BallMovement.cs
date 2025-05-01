using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    //private Vector3 speed = new Vector3();
    // Start is called before the first frame update

    private Rigidbody rb;
    private Rigidbody otherRb;
    private Vector3 newVelocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bat")) {

            rb = GetComponent<Rigidbody>();
            otherRb = other.GetComponent<Rigidbody>();
            newVelocity = Vector3.Cross(-rb.velocity, otherRb.velocity);
            rb.velocity = newVelocity;
            otherRb.velocity = Vector3.zero;
            //speed = 
        }
    }
}

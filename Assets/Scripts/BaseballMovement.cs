using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballMovement : MonoBehaviour
{
    public float lifespan = 8f; // Time before the ball destroys itself

    void Start()
    {
        Destroy(gameObject, lifespan); // Destroy the baseball after `lifespan` seconds
    }
}

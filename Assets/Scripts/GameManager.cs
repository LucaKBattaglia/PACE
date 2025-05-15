using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager instance is null");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public int ballsHitCount;
    public int playerHitCount;
    public int droneHitCount;
    public int ballsMissedCount;
    private float timePassed;
    public float TimePassed
    {
        get
        {
            return TimePassed;
        }
    }

    public float ballLifetime;

    // Start is called before the first frame update
    void Start()
    {
        ballsHitCount = 0;
        playerHitCount = 0;
        droneHitCount = 0;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = Time.time;
    }
}

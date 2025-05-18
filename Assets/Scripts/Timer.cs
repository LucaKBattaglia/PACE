using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f; 
    public bool timerIsRunning = false;
    public Text timerText; 

    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Timeout!");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(timeRemaining);
                
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; 

        int minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);  

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

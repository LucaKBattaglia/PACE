﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string sceneToLoad = "Main"; 
    
     public void StartTheGame()
    {
        {
            Debug.Log("Button Clicked");
             SceneManager.LoadScene(sceneToLoad);
        }
    }
}
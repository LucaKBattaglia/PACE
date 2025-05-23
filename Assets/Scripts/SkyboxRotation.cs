using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = GameManager.instance.skyboxRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}

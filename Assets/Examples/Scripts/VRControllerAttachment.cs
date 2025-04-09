using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class VRControllerAttachment : MonoBehaviour
{
    //Objects to be attached to each hand. 
    public GameObject leftHandTool;
    public GameObject rightHandTool;

    private Transform leftAnchor;
    private Transform rightAnchor;

    void Start()
    {
        //Finds the Camera rig and attaches the tools to each hand anchor
        OVRCameraRig rig = FindObjectOfType<OVRCameraRig>();

        if(rig != null )
        {
            leftAnchor = rig.leftHandAnchor;
            rightAnchor = rig.rightHandAnchor;

            
            if (leftHandTool != null)
            {
                leftHandTool.transform.SetParent(leftAnchor, false);
                leftHandTool.transform.localPosition = Vector3.zero;
                leftHandTool.transform.localRotation = Quaternion.identity;
            }

            if (rightHandTool != null)
            {
                rightHandTool.transform.SetParent(rightAnchor, false);
                rightHandTool.transform.localPosition = Vector3.zero;
                rightHandTool.transform.localRotation = Quaternion.identity;
            }
        } else
        {
            //In case Camera rig can't be found
            UnityEngine.Debug.Log("OVRCameraRig not found in scene.");
        }

        
    }

    void Update()
    {
        //Sets each object active while the controllers are connected
        if( leftHandTool != null ) { 
            leftHandTool.SetActive(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch));
        }

        if (rightHandTool != null)
        {
            rightHandTool.SetActive(OVRInput.IsControllerConnected(OVRInput.Controller.RTouch));
        }
    }
}

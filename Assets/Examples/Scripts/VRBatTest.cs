using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Currently unsure which of these are required for this exercise. Imported all that I thought may be needed, and then some :P
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;

public class VRBatTest : MonoBehaviour
{
    public GameObject leftHandTool;
    public GameObject rightHandTool;

    //Device is for input
    // private var device;

    // private var rightHandInput;
    // private var leftHandInput;

    // //Avatar is for transform and visuals
    // private var avatar;

    // private var rightHand;
    // private var leftHand;

    void Start()
    {
        var avatar = VRAvatar.Active;
            if (avatar == null)
                return;

        var rightHand = avatar.PrimaryHand;
        var leftHand = avatar.SecondaryHand;
        
        var device = VRDevice.Device;

        var rightHandInput = device.PrimaryInputDevice;
        var leftHandInput = device.SecondaryInputDevice;

        if (leftHandTool != null)
            {
                // leftHand.SetControllerVisibility(false);
                // leftHandTool.transform.SetParent(leftHand.transform, false);
                // leftHandTool.transform.localPosition = Vector3.zero;
                // leftHandTool.transform.localRotation = Quaternion.identity;
            }

            if (rightHandTool != null)
            {
                // rightHand.SetControllerVisibility(false);
                // rightHandTool.transform.SetParent(rightHand.transform, false);
                // rightHandTool.transform.localPosition = Vector3.zero;
                // rightHandTool.transform.localRotation = Quaternion.identity;
            }

        
    }

    void Update()
    {
        // if( leftHandTool != null ) { 
        //     leftHandTool.SetActive(OVRInput.IsControllerConnected(OVRInput.Controller.LTouch));
        // }

        // if (rightHandTool != null)
        // {
        //     rightHandTool.SetActive(OVRInput.IsControllerConnected(OVRInput.Controller.RTouch));
        // }
    }
}

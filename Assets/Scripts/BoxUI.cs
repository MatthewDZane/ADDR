using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BoxUI : MonoBehaviour
{
    SteamVR_Behaviour_Pose trackedObj;

    public SteamVR_Action_Boolean select = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_Behaviour_Pose>();
    }

    void Update()
    {
        if (select.GetStateDown(trackedObj.inputSource))
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
            {
                RaycastableObject raycastObject = hitInfo.collider.gameObject.GetComponent<RaycastableObject>();
            }
        }
    }
}

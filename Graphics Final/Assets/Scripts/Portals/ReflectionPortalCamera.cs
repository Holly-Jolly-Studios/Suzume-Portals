using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionPortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
	
    void LateUpdate () {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;
        
        transform.rotation = Quaternion.identity;
    }
}

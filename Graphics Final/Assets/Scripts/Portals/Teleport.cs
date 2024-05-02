using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport otherTeleport;

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;
        
        if(zPos < 0)
        {
            TeleportPlayer(other.transform);
        }
    }
    
    private void TeleportPlayer(Transform objectToTeleport)
    {
        Debug.Log("teleport");

        //objectToTeleport.GetComponent<PlayerController>()?.PauseController();
        
        // Position
        Vector3 localPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(objectToTeleport.position);
        localPosition = new Vector3(-localPosition.x, localPosition.y, -localPosition.z);
        objectToTeleport.position = otherTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPosition);

        Quaternion diff = otherTeleport.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        objectToTeleport.rotation = diff * objectToTeleport.rotation;
    }
}

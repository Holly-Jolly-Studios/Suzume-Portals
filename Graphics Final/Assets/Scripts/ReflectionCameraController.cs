using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReflectionCameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform waterPlane;
    [SerializeField] private PlayerController player;

    [SerializeField] private Transform playerCamera;
    
    private bool isOnWaterPlane = false;

    private void Start()
    {
        player = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        // If the player is on the water plane then run as usual, but if on the grass plane the camera needs to be mirrored
        isOnWaterPlane = player.GetOnWater();
        
        float transformX = mainCamera.transform.position.x;
        float transformY = waterPlane.position.y - (mainCamera.transform.position.y - waterPlane.position.y);
        float transformZ = mainCamera.transform.position.z;

        if (!isOnWaterPlane)
        {
            transform.position = new Vector3(1, 1, 0);
            Debug.Log("Not on water");
        }
        else
        {
            transform.position = new Vector3(transformX, transformY, transformZ);
            Debug.Log("water");
        }
        
        float rotationX = -mainCamera.transform.rotation.eulerAngles.x;
        float rotationY = mainCamera.transform.rotation.eulerAngles.y;
        float rotationZ = mainCamera.transform.rotation.eulerAngles.z;
        
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}

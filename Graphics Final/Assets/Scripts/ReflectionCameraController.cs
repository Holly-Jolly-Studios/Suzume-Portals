using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReflectionCameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform waterPlane;

    void Update()
    {
        float transformX = mainCamera.transform.position.x;
        float transformY = waterPlane.position.y - (mainCamera.transform.position.y - waterPlane.position.y);
        float transformZ = mainCamera.transform.position.z;

        transform.position = new Vector3(transformX, transformY, transformZ);

        float rotationX = -mainCamera.transform.rotation.eulerAngles.x;
        float rotationY = mainCamera.transform.rotation.eulerAngles.y;
        float rotationZ = mainCamera.transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}

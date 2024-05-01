using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionCameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        float transformX = mainCamera.transform.position.x;
        float transformY = -mainCamera.transform.position.y;
        float transformZ = mainCamera.transform.position.z;

        transform.position = new Vector3(transformX, transformY, transformZ);


    }
}

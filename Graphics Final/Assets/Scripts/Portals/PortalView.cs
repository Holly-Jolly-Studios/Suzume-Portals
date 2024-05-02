using System;
using Unity.Mathematics;
using UnityEngine;

public class PortalView : MonoBehaviour
{
    public PortalView otherPortal;
    public Camera portalView;
    public Shader portalShader;
    [SerializeField] private MeshRenderer portalMesh;

    private Material portalMat;

    private void Start()
    {
        // Create new target render texture for camera
        otherPortal.portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        // Create a new material on runtime based on the other portal's view
        portalMat = new Material(portalShader);
        portalMat.mainTexture = otherPortal.portalView.targetTexture;

        // Apply the material to the plane
        portalMesh.material = portalMat;
    }

    private void Update()
    {
        // // Turing a global position of the camera to a local position
        Vector3 viewerPosition = otherPortal.transform.InverseTransformPoint(Camera.main.transform.position);
        viewerPosition = new Vector3(-viewerPosition.x, viewerPosition.y, -viewerPosition.z);
        portalView.transform.localPosition = viewerPosition;
        //
        // // Set rotation based on player rotation
        Quaternion diff = transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0, 180, 0));
        portalView.transform.rotation = diff * Camera.main.transform.rotation;
        //
        // // Allows portals to see through objects, mainly the door frame
        portalView.nearClipPlane = viewerPosition.magnitude;
    }
}

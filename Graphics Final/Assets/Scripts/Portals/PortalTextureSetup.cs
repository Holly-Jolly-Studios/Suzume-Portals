using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraB;
    public Material cameraMatB;
    
    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release(); 
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        cameraMatB.mainTexture = cameraB.targetTexture;
    }

}

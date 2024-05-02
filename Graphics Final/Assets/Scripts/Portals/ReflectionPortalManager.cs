using UnityEngine;

public class ReflectionPortalManager : MonoBehaviour
{
    public Camera camera;

    public Material reflectionCameraMat;

    void Start () {
        if (camera.targetTexture != null)
        {
            camera.targetTexture.Release();
        }
        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        reflectionCameraMat.mainTexture = camera.targetTexture;
    }
}

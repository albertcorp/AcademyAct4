using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCameras : MonoBehaviour
{
    public Camera camera1; // Primera cámara
    public Camera camera2; // Segunda cámara

    public bool camera1Active;

    void Start()
    {
        if (camera1 != null && camera2 != null)
        {
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
            camera1Active = true;
        }
    }

    public void OnCamara() 
    {
        if (camera1Active)
        {
            SwitchToCamera(camera2, camera1);
            camera1Active = false;
        }
        else 
        {
            SwitchToCamera(camera1, camera2);
            camera1Active = true;
        }
    }

    void SwitchToCamera(Camera activateCamera, Camera deactivateCamera)
    {
        if (activateCamera != null && deactivateCamera != null)
        {
            activateCamera.gameObject.SetActive(true);
            deactivateCamera.gameObject.SetActive(false);
        }
    }
}

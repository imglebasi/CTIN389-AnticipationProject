using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public Camera cam;
    public bool isCamLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LockCam()
    {
        isCamLocked = !isCamLocked;
        cam.GetComponent<CameraLook>().enabled = isCamLocked;
    }
}

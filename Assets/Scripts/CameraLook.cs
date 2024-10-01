using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float sensitivity = 1f;
    public AudioManager theAudioManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse Y") * -1 * sensitivity;
        rotationY += Input.GetAxis("Mouse X") * 1 * sensitivity;
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Vector2 rotation;
    public GameObject cam;
    ControllerConnected conC;
    public bool usesCont = false;
    public bool LockMode = true;
    public float mouseSensitivity = 5f;
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        conC = GameObject.Find("ControllerUse").GetComponent<ControllerConnected>();
    }

    // Update is called once per frame
    void Update()
    {
        if (conC.isConnected == false)
        {
            usesCont = false;
        }
        else { usesCont = true;}
        if (!usesCont && LockMode==true)
        {
            float verticalRotation;
            rotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
            transform.localRotation = Quaternion.Euler(0f, rotation.x, 0f);
             verticalRotation = Mathf.Clamp(rotation.y, -90f, 90f);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
           
        }
        else
        {
            float verticalRotation;
            rotation.x += Input.GetAxis("Mouse X") * mouseSensitivity / 8;
            rotation.y += Input.GetAxis("Mouse Y") * mouseSensitivity / 8;
            transform.localRotation = Quaternion.Euler(0, rotation.x, 0);
            verticalRotation = Mathf.Clamp(rotation.y, -90f, 90f);
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
        
    }
}

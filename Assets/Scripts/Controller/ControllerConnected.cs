using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerConnected : MonoBehaviour
{
    public bool useController = false;
    public bool isConnected;
    void Start()
    {
        UpdateControllerUse();
    }

    // Update is called once per frame
    void UpdateControllerUse()
    {
        if (useController && Input.GetJoystickNames().Length != 0) {    
            isConnected = true;
        }
    }
}

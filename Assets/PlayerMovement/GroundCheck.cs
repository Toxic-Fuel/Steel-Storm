using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Movement playerMovementScript;

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == 6)
        {
            
            playerMovementScript.JumpedTimes = 0;
        }
    }
}

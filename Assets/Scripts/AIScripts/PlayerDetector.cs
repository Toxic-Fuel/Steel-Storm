using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    bool oneTime = false;
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player")
        {
            if (oneTime == false)
            {
                if (gameObject.GetComponentInParent<MeleeEnemyScript>() != null)
                {
                    gameObject.GetComponentInParent<MeleeEnemyScript>().seesPlayer = true;
                    gameObject.GetComponentInParent<MeleeEnemyScript>().ChooseAction();
                }
                else if (gameObject.GetComponentInParent<RangedEnemyScript>() != null)
                {
                    gameObject.GetComponentInParent<RangedEnemyScript>().seesPlayer = true;

                    gameObject.GetComponentInParent<RangedEnemyScript>().ChooseAction();
                }
                oneTime = true;
            }
            

        }
    }
   
}

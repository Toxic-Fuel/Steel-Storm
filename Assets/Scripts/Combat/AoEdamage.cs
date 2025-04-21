using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AoEdamage : MonoBehaviour
{
    public float Damage;
    public string[] Effects;
    public bool continous;
    public float Cooldown;
    Stopwatch sw = new Stopwatch();
    
    private void OnTriggerEnter(Collider other)
    {
        if (continous == false)
        {
            if (other.GetComponent<EntityHealth>() != null && other.tag == "Player")
            {
                other.GetComponent<EntityHealth>().TakeDamage(Damage, Effects);
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (continous == true && (sw.IsRunning == false || sw.ElapsedMilliseconds >= Cooldown*1000))
        {
            if (other.GetComponent<EntityHealth>() != null && other.tag == "Player")
            {
                other.GetComponent<EntityHealth>().TakeDamage(Damage, Effects);
                if (sw.IsRunning == false)
                {
                    sw.Start();
                }
                else
                {
                    sw.Restart();
                }
            }
        }
        
    }
}

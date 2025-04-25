using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : BasicEnemy
{
    public GameObject ragdoll;
    public override void Damaged()
    {
        
    }

    public override void OnDeath()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    
}

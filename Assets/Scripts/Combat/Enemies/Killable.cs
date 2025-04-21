using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : BasicEnemy
{
    public override void Damaged()
    {
        
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    
}

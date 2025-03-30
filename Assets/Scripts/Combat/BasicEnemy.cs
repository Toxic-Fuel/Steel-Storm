using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemy : MonoBehaviour
{
    public abstract void OnDeath();
    public abstract void Damaged();
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public string[] Effects;
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<EntityHealth>().TakeDamage(Damage, Effects);
        Debug.Log("Hit " + collision.gameObject.name + " with "+ Damage);
    }
}

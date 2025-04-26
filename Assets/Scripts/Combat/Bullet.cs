using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public string[] Effects;
    public GameObject particles;
    public float Delay = 0f;
    public bool explosive=false;
    public bool isParticle = false;
   
    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("Hit " + collision.gameObject.name);
        if (particles != null)
        {
            Instantiate(particles, transform.position, transform.rotation);
        }
        if (explosive == true)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<EntityHealth>() != null)
        {
            
            collision.gameObject.GetComponent<EntityHealth>().TakeDamage(Damage, Effects);
            

            if (Delay == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                particles.GetComponent<ParticleSystem>().Play();
                deleteAfterSeconds(Delay);

            }

        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (isParticle)
        {
            if (particles != null)
            {
                Instantiate(particles, transform.position, transform.rotation);
            }
            if (other.gameObject.GetComponent<EntityHealth>() != null)
            {

                other.gameObject.GetComponent<EntityHealth>().TakeDamage(Damage, Effects);
                Debug.Log("Hit " + other.gameObject.name + " with " + Damage);

                if (Delay == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    particles.GetComponent<ParticleSystem>().Play();
                    deleteAfterSeconds(Delay);

                }

            }

        }
    }
    
       
    private IEnumerator deleteAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

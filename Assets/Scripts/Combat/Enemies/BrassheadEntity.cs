using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrassheadEntity : BasicEnemy
{
    public AudioClip deathSFX;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public override void Damaged()
    {

    }

    public override void OnDeath()
    {
        audioSource.clip = deathSFX;
        audioSource.Play();
        gameObject.GetComponentInChildren<Animator>().SetTrigger("dead");
        GameObject.Find("VictoryScreen").GetComponentInChildren<Animator>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }


}

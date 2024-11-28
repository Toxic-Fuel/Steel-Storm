using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator animator;
    public AudioClip OpenSFX;
    public AudioClip CloseSFX;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("IsOpened", true);
        if(audioSource != null)
        {
            audioSource.clip = OpenSFX;
            audioSource.Play();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("IsOpened", false);
        if (audioSource != null)
        {
            audioSource.clip = CloseSFX;
            audioSource.Play();
        }
            
    }
}

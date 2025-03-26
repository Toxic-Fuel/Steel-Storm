using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public Animator animator;
    public WeaponHotbar weaponHotbar;
    public int maxAnims = 2;
    int curAnim = 0;
    bool isOn;
    bool prevState;
    public AudioClip lightsaberIgnition;
    public AudioClip lightsaberOff;
    public AudioClip[] lightsaberAttacks;
    public AudioSource lightsaberSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        prevState = isOn;
        isOn = weaponHotbar.itemName == "lightsaber";
        if(isOn !=prevState)
        {
            if (isOn)
            {
                animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                lightsaberSound.clip = lightsaberIgnition;
                lightsaberSound.Play();
            }
            else
            {
                animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                lightsaberSound.clip = lightsaberOff;
                lightsaberSound.Play();
            }
        }
        if (Input.GetButtonDown("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("lightSaberIdle") && isOn)
        { 
            animator.SetInteger("attackNum", curAnim);
            animator.SetTrigger("attack");
            lightsaberSound.clip = lightsaberAttacks[curAnim];
            lightsaberSound.Play();
            //animator.ResetTrigger("attack");
            if(curAnim >= maxAnims)
            {
                curAnim = 0;
            }
            else
            {
                curAnim++;
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController rb;
    ControllerConnected conC;
    public float gravity;
    //currentGravity should be hidden in editor. If someone knows how to change that.
    public float currentGravity;
    float velocity;
    public bool sprint = true;
    
    public int MaxJumpTimes = 2;
    public int JumpedTimes = 0;
    bool JumpCost = true;
    public float JumpHight = 5f;
    public float stamina = 100f;
    public Sprintbar sb;
    public float CooldownDuration = 0.2f;
    public float CooldownDuration2 = 0.2f;
    bool cooldown = true;
    bool cooldown2 = true;
    bool run = false;
    public float normal_velocity = 4;
    public bool isPlayerInBoat = false;
    public bool isWithController;
    public AudioSource audioSource;
    public WalkingAudio[] walkingAudio;
    [System.Serializable]
    public struct WalkingAudio
    {
        public string SurfaceTag;
        public AudioClip Audio;
    }
    void Start() 
    { 
        velocity = normal_velocity;
        currentGravity = gravity;
        conC = GameObject.Find("ControllerUse").GetComponent<ControllerConnected>();
        sb.Bar();
    }

    public Vector3 G;

    void Update()
    {
        RaycastHit Hit;
        LayerMask hitMask = LayerMask.GetMask("Ground");
        if(Physics.Raycast(transform.position, -transform.up, out Hit, Mathf.Infinity, hitMask))
        {
            
            foreach (WalkingAudio audio in walkingAudio)
            {
                if(Hit.collider.gameObject.tag == audio.SurfaceTag)
                {
                    audioSource.clip = audio.Audio;
                    break;
                }
            }
        }
        if(isPlayerInBoat!=true){
        if(stamina <= 0)
        {
            run = false;
        }
        else if(stamina > 0 && sprint)
        {
            run = true;
        }

        if (run && sprint)
        {
            velocity = 10;
        }
        else
        {
                velocity=normal_velocity;
        }
        if (cooldown)
        {
            if (stamina < 100)
            {
                stamina+=0.1f;
                sb.Bar();
            }
            
            StartCooldown();
        }
        
        }
        Vector3 Movement = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if(audioSource.isPlaying == false && JumpedTimes == 0)
            {
                audioSource.Play();
            }
            
            
        }
        else
        {
            audioSource.Pause();
        }
        if(JumpedTimes > 0)
        {
            audioSource.Pause();
        }
        rb.Move(Movement * velocity * Time.deltaTime);
        if(isPlayerInBoat!=true){
        if (Input.GetButtonDown("Sprint"))
        {
                if (!isWithController)
                {
                    sprint = true;
                }
                else
                {
                    sprint = !sprint;
                }
            
        }
        else if(Input.GetButtonUp("Sprint"))
        {
                if (!isWithController)
                {
                    sprint = false;
                }
            
        }
        if (sprint == true)
        {
                //audioSource.clip.
            if (cooldown2)
            {
                stamina -= 0.2f;
                sb.Bar();
                Startcooldown2();
            }
        }
        if (JumpedTimes < MaxJumpTimes && Input.GetButtonDown("Jump"))
        {
            G.y = JumpHight;
            
            JumpedTimes++;
        }
        else
        {
            
            G.y += currentGravity * Time.deltaTime;
        }
        rb.Move(G * Time.deltaTime);

    }
        if (conC.isConnected == false)
        {
            isWithController = false;
        }
        else { isWithController = true;  }
    }
    public IEnumerator StartCooldown()
    {
        cooldown = false;
        yield return new WaitForSeconds(CooldownDuration);
        cooldown = true;
    }
    public IEnumerator Startcooldown2()
    {
        cooldown2 = false;
        yield return new WaitForSeconds(CooldownDuration2);
        cooldown2= true;
    }
}

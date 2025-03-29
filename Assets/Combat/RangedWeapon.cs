using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    int loadedAmmo;
    public int TotalAmmo;
    public int MaxAmmo;
    public string[] Effects;
    public float DamagePerBullet;
    public GameObject bullet;
    public Transform bulletExit;
    public float bulletForce;
    public float spreadAngle;
    public bool automatic;
    public ParticleSystem muzzleFlash;
    public float cooldown;
    public int individualBulletsAtOnce;
    public AudioSource gunSoundSource;
    [SerializeField]
    public AudioClip shootSound;
    public bool continueousSound;
    public TMP_Text[] textObjs;
    public Animator animator;

    public Rotation mouseLook;
    public Transform cam;
    public float recoilUpDistance;
    public float recoilLeftAmountmin;
    public float recoilLeftAmountmax;
    public float recoilRightAmountMin;
    public float recoilRightAmountMax;

    private Vector3 upRecoilamountV;
    private Vector3 leftRecoilamountV;
    private Vector3 RightRecoilamountV;

    List<Quaternion> bullets;

    private Stopwatch coolDownSW = new Stopwatch();
    private void Start()
    {
        coolDownSW.Start();
        bullets = new List<Quaternion>(individualBulletsAtOnce);
        for (int i = 0; i < individualBulletsAtOnce; i++)
        {
            bullets.Add(Quaternion.Euler(Vector3.zero));
        }
        UpdateWeaponData();
    }
    private void Update()
    {
        if (automatic)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
                animator.SetBool("Shooting", true);
            }
            else
            {
                gunSoundSource.Stop();
                animator.SetBool("Shooting", false);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                animator.SetBool("Shooting", true);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                animator.SetBool("Shooting", false);
            }
        }
        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Scope", true);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("Scope", false);
        }
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Moving", true);
            if (Input.GetButton("Sprint"))
            {
                animator.SetBool("Sprinting", true);
            }
            else
            {
                animator.SetBool("Sprinting", false);
                
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        
    }
    void Shoot()
    {
        if(loadedAmmo > 0)
        {
            if (coolDownSW.ElapsedMilliseconds > cooldown * 1000)
            {
                loadedAmmo -= 1;
                UpdateWeaponData();
                PlayShootSound();
                muzzleFlash.Play();
                for (int i = 0; i < individualBulletsAtOnce; i++)
                {
                    bullets[i] = Random.rotation;
                    GameObject p = Instantiate(bullet, bulletExit.position, bulletExit.rotation);
                    p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, bullets[i], spreadAngle);
                    p.GetComponent<Rigidbody>().AddForce(p.transform.right * bulletForce);
                    p.GetComponent<Bullet>().Effects = Effects;
                    p.GetComponent<Bullet>().Damage = DamagePerBullet;
                    i++;
                }
                AddRecoil();
                coolDownSW.Restart();
            }
        }
        else
        {
            gunSoundSource.Stop();
        }
        
    }
    void PlayShootSound()
    {

        
        
        if (gunSoundSource.clip != shootSound)
        {
            gunSoundSource.clip = shootSound;
        }
        if(gunSoundSource.isPlaying == false && continueousSound)
        {
            gunSoundSource.Play();
        }
        else if (continueousSound == false)
        {
            gunSoundSource.Play();
        }
        
        
    }
    public void AddRecoil()
    {
        upRecoilamountV = new Vector3(recoilUpDistance, 0, 0);
        leftRecoilamountV = new Vector3(0, Random.Range(-recoilLeftAmountmin, -recoilLeftAmountmax), 0);
        RightRecoilamountV = new Vector3(0, Random.Range(recoilRightAmountMin, recoilRightAmountMax), 0);
        UpRecoil();
        LeftRecoil();
        RightRecoil();
    }
    public void UpRecoil()
    {
        float angleRecoilUpDistanceF = recoilUpDistance * Time.deltaTime;
        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilUpDistanceF, upRecoilamountV);

        mouseLook.rotation.y += angleRecoilUpDistanceF;

    }
    public void LeftRecoil()
    {
        float angleRecoilLeft = Random.Range(-recoilLeftAmountmin, -recoilLeftAmountmax) * Time.deltaTime;

        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilLeft, leftRecoilamountV);
        mouseLook.rotation.x += angleRecoilLeft;

    }
    public void RightRecoil()
    {
        float angleRecoilRight = Random.Range(recoilRightAmountMin, recoilRightAmountMax) * Time.deltaTime;

        cam.transform.localRotation *= Quaternion.AngleAxis(angleRecoilRight, RightRecoilamountV);
        mouseLook.rotation.x += angleRecoilRight;

    }
    public void Reload()
    {
        if (loadedAmmo > 0)
        {
            if(MaxAmmo-loadedAmmo <= TotalAmmo)
            {
                TotalAmmo -= (MaxAmmo - loadedAmmo);
                loadedAmmo = MaxAmmo;
                
            }
            else
            {
                loadedAmmo += TotalAmmo;
                TotalAmmo = 0;
                
            }
        }
        else
        {
            if (MaxAmmo <= TotalAmmo)
            {
                loadedAmmo = MaxAmmo;
                TotalAmmo -= MaxAmmo;
            }
            else if (MaxAmmo > TotalAmmo)
            {
                loadedAmmo = TotalAmmo;
                TotalAmmo = 0;
            }
        }
        UpdateWeaponData();
        
    }
    public void UpdateWeaponData()
    {
        textObjs[0].text = loadedAmmo.ToString();
        textObjs[1].text = TotalAmmo.ToString();

    }

}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    int ammo;
    public int MaxAmmo;
    public int StartingAmmo;
    public string[] Effects;
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

    }
    private void Update()
    {
        if (automatic)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        if (coolDownSW.ElapsedMilliseconds > cooldown * 1000)
        {
            PlayShootSound();
            muzzleFlash.Play();
            for (int i = 0; i < individualBulletsAtOnce; i++)
            {
                bullets[i] = Random.rotation;
                GameObject p = Instantiate(bullet, bulletExit.position, bulletExit.rotation);
                p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, bullets[i], spreadAngle);
                p.GetComponent<Rigidbody>().AddForce(p.transform.right * bulletForce);
                i++;
            }
            AddRecoil();
            coolDownSW.Restart();
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

}

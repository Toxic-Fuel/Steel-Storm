using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LobAttack : MonoBehaviour
{
    public GameObject enemy;
    public GameObject bullet;
    public GameObject turret;
    public float speed = 15f;
    public float rotationSpeed = 3f;
    public bool LowAngle = false;
    public bool rotate = false;
    Vector3 eulerToBecome;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.z));
        this.transform.rotation =  Quaternion.Slerp(this.transform.rotation, LookRotation, Time.deltaTime*rotationSpeed);
        if(rotate)
        {
            RotateTurret();
        }
            
        
        
            
        
        
       
    }
    public void CreateBullet()
    {
        GameObject shell = Instantiate(bullet, turret.transform.GetChild(0).position, turret.transform.GetChild(0).rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * turret.transform.GetChild(0).forward;
        
    }
    private void RotateTurret()
    {
        float? angle = CalculateAngle(LowAngle);
        if(angle != null)
        {
            turret.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }
        
    }
    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.8f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if(underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;
            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }
    }
}

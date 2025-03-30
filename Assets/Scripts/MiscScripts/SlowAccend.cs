using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAccend : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (Time.deltaTime*speed), transform.position.z);
    }
}

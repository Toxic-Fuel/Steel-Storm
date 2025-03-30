using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float secondsTillDecay = 2f;
    void Start()
    {
        Invoke("DecayAfterSecs", secondsTillDecay);
    }

    void DecayAfterSecs()
    {
        Destroy(gameObject);
    }
}

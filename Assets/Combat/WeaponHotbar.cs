using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHotbar : MonoBehaviour
{
    int currentItem = 0;
    public string itemName;
    public string[] items;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentItem++;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentItem--;
        }
        
        itemName = items[currentItem];
        
    }
}

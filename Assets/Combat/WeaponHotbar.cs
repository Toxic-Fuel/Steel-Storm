using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHotbar : MonoBehaviour
{
    int currentItem = 0;
    public GameObject[] weapons;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentItem < weapons.Length-1)
            {
                currentItem++;
            }
            else
            {
                currentItem = 0;
            }
            
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentItem > 0)
            {
                currentItem--;
            }
            else
            {
                currentItem = weapons.Length-1;
            }
        }
        
       
        
    }
    void WeaponUodater()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[currentItem].SetActive(true);
    }
}

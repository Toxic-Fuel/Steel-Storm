using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WeaponHotbar : MonoBehaviour
{
    int currentItem = 0;
    public GameObject[] weapons;
    public Sprite[] sprites;
    public Image[] images;
    
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
            WeaponUpdater();

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
            WeaponUpdater();
        }
       
       
        
    }
    void WeaponUpdater()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[currentItem].SetActive(true);
        images[0].sprite = sprites[currentItem];
        if (currentItem > 0)
        {
            images[1].sprite = sprites[currentItem-1];
        }
        else
        {
            images[1].sprite = sprites[currentItem + 1];
        }

    }
}

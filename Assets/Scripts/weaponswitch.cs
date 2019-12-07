using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weaponswitch : MonoBehaviour
{

    public int selectedweapon = 0;
    public GameObject weaponName;
    // Start is called before the first frame update
    void Start()
    {
        selected();
    }

    // Update is called once per frame
    void Update()
    {

        int previouswe = selectedweapon;
        if (Input.GetButtonDown("Swap"))
        {
            if(selectedweapon>= 3)
            {
                selectedweapon = 0;
            }
            else
            {
                selectedweapon++;
            }
            
        }

        if(previouswe != selectedweapon)
        {
            selected();
        }
    }

    void selected()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedweapon)
            {
                weapon.gameObject.SetActive(true);
                GameObject.Find("HUD/Pilot HUD/WeaponSelected/WeaponName").GetComponent<Text>().text = weapon.gameObject.name;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;

        }

    }
}

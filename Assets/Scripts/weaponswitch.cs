using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponswitch : MonoBehaviour
{

    public int selectedweapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        selected();
    }

    // Update is called once per frame
    void Update()
    {

        int previouswe = selectedweapon;
        if (Input.GetButtonDown("Z"))
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

            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;

        }
    }
}

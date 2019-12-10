using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class weaponswitch : MonoBehaviour
{

    //public int selectedweapon = 0;
    //public GameObject weaponName;
    private GameManager gameManager;
    public GameObject FPS;
    private CallTitan callTitan;
    public List<string> selectedWeapons;
    public int indexOfSelectedWeapon = -1;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        callTitan = FPS.GetComponent<CallTitan>();
        selectedWeapons = gameManager.selectedWeapons;
        //selected();
        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        //int previouswe = selectedweapon;
        if (Input.GetButtonDown("Swap") && !callTitan.titanEmbarked)
        {
            //if(selectedweapon>= 4)
            //{
            //    selectedweapon = 0;
            //}
            //else
            //{
            //    selectedweapon++;
            //}
            SwitchWeapon();
            
        }

        //if(previouswe != selectedweapon)
        //{
        //    selected();
        //}
    }

    void SwitchWeapon()
    {
        indexOfSelectedWeapon = (indexOfSelectedWeapon + 1) % 2;
        string selectedWeaponName = selectedWeapons[indexOfSelectedWeapon];
        foreach (Transform weapon in transform)
        {
            if (weapon.gameObject.name.Equals(selectedWeaponName))
            {
                weapon.gameObject.SetActive(true);
                GameObject.Find("HUD/Pilot HUD/WeaponSelected/WeaponName").GetComponent<Text>().text = weapon.gameObject.name;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchToTitanWeapon()
    {
        foreach (Transform weapon in transform)
        {
            if (weapon.gameObject.name.Equals("Predator Cannon"))
            {
                weapon.gameObject.SetActive(true);
                //GameObject.Find("HUD/Titan HUD/WeaponSelected/WeaponName").GetComponent<Text>().text = weapon.gameObject.name;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void SwitchToPilotWeapon()
    {
        indexOfSelectedWeapon++;
        SwitchWeapon();
    }

    //IEnumerator WaitBeforeChainginWeaponName(string newName)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    GameObject.Find("HUD/Pilot HUD/WeaponSelected/WeaponName").GetComponent<Text>().text = newName;

    //}
    //void selected()
    //{
    //    int i = 0;
    //    foreach (Transform weapon in transform)
    //    {
    //        if(i == selectedweapon)
    //        {
    //            weapon.gameObject.SetActive(true);
    //            GameObject.Find("HUD/Pilot HUD/WeaponSelected/WeaponName").GetComponent<Text>().text = weapon.gameObject.name;
    //        }
    //        else
    //        {
    //            weapon.gameObject.SetActive(false);
    //        }
    //        i++;

    //    }

    //}
}

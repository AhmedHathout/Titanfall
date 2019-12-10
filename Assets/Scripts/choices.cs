using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class choices : MonoBehaviour
{

    public GameObject b1;
    public GameObject b2;

    public Button button1;
    public Button button2;

    public GameObject button3;
    public GameObject titan;
    public GameObject titantext;
    public GameObject titantext1;


    public GameObject t1;
    public GameObject t2;
    public GameObject t3;
    public GameObject shotgun;
    public GameObject Rifle;
    public GameObject Rocket;
    public GameObject Grenade;
    int counter = 0;

    public List<string> weapons = new List<string>();

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void OnEnable()
    {

        button1.onClick.AddListener(addweapon);//adds a listener for when you click the button
        button2.onClick.AddListener(addweapon2);//adds a listener for when you click the button

    }
    void addweapon2()// your listener calls this function
    {
        weapons.Add(b2.GetComponent<Text>().text);
        Debug.Log(b2.GetComponent<Text>().text + " added");

        if (counter ==1)
        {
            gameManager.LoadLevel1();
            gameManager.selectedWeapons = weapons;
        }
        shotgun.SetActive(false);
        Rifle.SetActive(false);
        Rocket.SetActive(true);
        Grenade.SetActive(true);

        b1.GetComponent<Text>().text = "Rocket Launcher";
        b2.GetComponent<Text>().text = "Grenade Launcher";

        t1.GetComponent<Text>().text = "Choose a Heavy Weapon";
        t2.GetComponent<Text>().text = "\n \n \n Rocket Launcher " +
            "\n - Damage: 150" + "\n - Radius of damage: 3";
        t3.GetComponent<Text>().text = "\n \n \n Grenade Launcher " +
            "\n - Damage: 125" + "\n - Radius of damage: 4";
        counter = 1;
    }

    public void addweapon()
    {
        weapons.Add(b1.GetComponent<Text>().text);
        Debug.Log(b1.GetComponent<Text>().text + " added");

        if (counter == 1)
        {
            //gm.weaponschosen(weapons);
            gameManager.LoadLevel1();
            gameManager.selectedWeapons = weapons;

        }

        shotgun.SetActive(false);
        Rifle.SetActive(false);
        Rocket.SetActive(true);
        Grenade.SetActive(true);

        b1.GetComponent<Text>().text = "Rocket Launcher";
        b2.GetComponent<Text>().text = "Grenade Launcher";

        t1.GetComponent<Text>().text = "Choose a Heavy Weapon";
        t2.GetComponent<Text>().text = "\n \n \n Rocket Launcher " +
            "\n - Damage: 150" + "\n - Radius of damage: 3";
        t3.GetComponent<Text>().text = "\n \n \n Grenade Launcher " +
            "\n - Damage: 125" + "\n - Radius of damage: 4";

        counter = 1;
    }

    public void Switch()
    {
      b1.SetActive(true);
      b2.SetActive(true);

      button1.GetComponent<Button>().gameObject.SetActive(true);
      button2.GetComponent<Button>().gameObject.SetActive(true);

      button3.SetActive(false);
      titan.SetActive(false);
      titantext.SetActive(false);
      titantext1.SetActive(false);

      t1.SetActive(true);
      t2.SetActive(true);
      t3.SetActive(true);
      shotgun.SetActive(true);
      Rifle.SetActive(true);
    }
}

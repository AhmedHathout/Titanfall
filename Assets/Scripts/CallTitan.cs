using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CallTitan : MonoBehaviour
{
    // TODO Handle not taking damage while using the defensive ability shield
    private GameManager gameManager = GameManager.instance;

    public GameObject playerTitan;
    public int titanMeter = 0;
    public static int fullTitanMeter = 100;

    public static int scalingValue = 30;

    public bool titanEmbarked = false;
    public int embarkDistance = 50;

    public int dashMeter = 0;
    public int maximumDashMeterValue = 3;

    public int dashDistance = 500;

    public int timeToIncreaseDash = 5;

    public GameObject defensiveAbilityShield;

    public int defensiveAbilityMeter = 0;
    public int defensiveAbilityMaxValue = 15; // 15 seconds to refill defensive ability
    public bool defensiveAbilityActivated = false;
    public int defensiveAbilityDuration = 10;

    public int coreAbilityMeter = 0;
    public int coreAbilityMaxValue = 100;

    public GameObject titanHUD;
    public GameObject pilotHUD;

    public int playerTitanMaxHealth = 400;
    public int playerTitanCurrentHealth = 400;
    public bool titanExists = false;

    public int playerPilotMaxHealth = 100;
    public int playerPilotCurrentHealth = 100;

    public Transform aimedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncrementDashMeter());
        StartCoroutine(IncrementDefensiveAbility());
    }

    // Update is called once per frame
    void Update()
    {
        CallInTitan();
        EmbarkTitan();
        Dash();
        activateDefensiveAbility();
        defensiveAbilityShield.SetActive(defensiveAbilityActivated);
        CoreAbility();
    }

    private void CallInTitan()
    {
        if (Input.GetButtonDown("Call Titan"))
        {
            Debug.Log("Calling Titan");
            if (titanMeter >= fullTitanMeter)
            {
                titanMeter = 0;
                playerTitan.transform.position = new Vector3(transform.position.x, playerTitan.transform.position.y, transform.position.z);
                playerTitan.SetActive(true);
                titanExists = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            titanMeter = fullTitanMeter;
        }
    }

    private void EmbarkTitan()
    {
        if (Input.GetButtonDown("Embark"))
        {
            float distanceToTitan = Vector3.Distance(transform.position, playerTitan.transform.position);

            if (distanceToTitan <= embarkDistance && !titanEmbarked)
            {
                playerTitan.SetActive(false);
                transform.localScale += new Vector3(scalingValue, scalingValue, scalingValue);
                titanEmbarked = true;
            }

            else if (titanEmbarked)
            {
                playerTitan.SetActive(true);
                titanEmbarked = false;
                transform.localScale -= new Vector3(scalingValue, scalingValue, scalingValue); ;
                playerTitan.transform.position = transform.position;
            }

            GetComponent<FirstPersonController>().titanMode = titanEmbarked;
            GetComponent<Crouch>().canCrouch = !titanEmbarked;
            titanHUD.SetActive(titanEmbarked);
            pilotHUD.SetActive(!titanEmbarked);


        }

        // TODO handle titan health
    }

    public void IncrementTitanMeter(int value)
    {
        titanMeter = Mathf.Min(titanMeter + value, fullTitanMeter);
    }

    IEnumerator IncrementDashMeter()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeToIncreaseDash);
            dashMeter = Mathf.Min(dashMeter + 1, maximumDashMeterValue);
        }
    }

    public void Dash()
    {
        if (Input.GetButtonDown("Jump") && dashMeter > 0)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            GetComponent<CharacterController>().Move(new Vector3(horizontal * 150, 0, vertical * 150));

            dashMeter -= 1;
        }

        // TODO Make Player invincible
    }

    public void activateDefensiveAbility()
    {
        if (Input.GetButtonDown("Defensive Ability"))
        {
            if (defensiveAbilityMeter >= defensiveAbilityMaxValue && titanEmbarked)
            {
                defensiveAbilityMeter = 0;
                defensiveAbilityActivated = true;
                StartCoroutine(DeactivatedefensiveAbility());
            }
        }
    }

    IEnumerator DeactivatedefensiveAbility()
    {
        yield return new WaitForSeconds(defensiveAbilityDuration);
        defensiveAbilityActivated = false;
    }

    IEnumerator IncrementDefensiveAbility()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!defensiveAbilityActivated)
            {
                defensiveAbilityMeter = Mathf.Min(defensiveAbilityMeter + 1, defensiveAbilityMaxValue);
            }
        }
    }

    private void CoreAbility()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            coreAbilityMeter = coreAbilityMaxValue;
        }
        if (Input.GetButtonDown("Core Ability"))
        {
            if (coreAbilityMeter >= coreAbilityMaxValue)
            {
                coreAbilityMeter = 0;

                GameObject nearestenemy = getNearestEnemy();

                if (nearestenemy == null)
                {
                    return;
                }

                GetComponent<FirstPersonController>().autoAim = true;
                aimedEnemy.position = new Vector3(nearestenemy.transform.position.x,
                    nearestenemy.transform.position.y + 30,
                    nearestenemy.transform.position.z);
                transform.LookAt(aimedEnemy);
                StartCoroutine(ResetCamera());

                // TODO make the player aim at that enemy 
            }
        }
    }

    private GameObject getNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = 100000000000;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);

            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void TakeDamage(int damage)
    {
        if (titanEmbarked)
        {
            playerTitanCurrentHealth -= damage;
            if (playerTitanCurrentHealth <= 0)
            {
                // TODO I think there should be some animation for the titan dying
                Destroy(playerTitan);
                titanExists = false;
            }
        }
        else
        {
            playerPilotCurrentHealth -= damage;
            if (playerPilotCurrentHealth <= 0)
            {
                gameManager.LoadGameOver();
            }
        }
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<FirstPersonController>().autoAim = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CallTitan : MonoBehaviour
{
    // TODO Handle not taking damage while using the defensive ability shield

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
        }

        // TOOO Change HUD
        // TODO handle titan health
    }

    public void incrementTitanMeter(int value)
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

            transform.Translate(5000, 0, 5000);
            //transform.position = new Vector3(transform.position.x + 5000, transform.position.y, transform.position.z);
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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class CallTitan : MonoBehaviour
{
    // TODO Handle not taking damage while using the defensive ability shield
    private GameManager gameManager = GameManager.instance;

    public int numberOfEnemiesKilled = 0;
    private int totalNumberOfEnemies = 15;

    public GameObject playerTitan;
    public int titanFallMeter = 0;
    public static int titanFallMaxValue = 100;

    public static int scalingValue = 30;

    public bool titanEmbarked = false;
    public int embarkDistance = 50;

    public int dashMeter = 0;
    public int dashMeterMaxValue = 3;

    public int dashDistance = 500;

    public int timeToIncreaseDash = 5;

    public GameObject defensiveAbilityShield;

    public int defensiveAbilityMeter = 0;
    public int defensiveAbilityMaxValue = 15; // 15 seconds to refill defensive ability
    public bool defensiveAbilityActivated = false;
    public int defensiveAbilityDuration = 10;

    public int coreAbilityMeter = 0;
    public int coreAbilityMaxValue = 100;
    public bool coreAbilityActivated = false;

    public GameObject titanHUD;
    public GameObject pilotHUD;

    public int playerTitanMaxHealth = 400;
    public int playerTitanCurrentHealth = 400;
    public bool titanExists = false;

    public int playerPilotMaxHealth = 100;
    public int playerPilotCurrentHealth = 100;

    public Transform aimedEnemy;
    
    public GameObject pilotHealthBarImage;
    public GameObject titanHealthBarImager;
    public GameObject dashBarImage;
    public GameObject titanFallBarImage;
    public GameObject defensiveAbilityBarText;
    public GameObject coreAbilityBarImage;

    [HideInInspector]
    public int killingPilotPoints = 10;
    [HideInInspector]
    public int killingTitanPoints = 50;

    public int secondsPassedSinceLastDamage = 0;
    private int timeToRegenerateHealth = 3;
    private int amountOfHealthToRegenerate = 5;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        StartCoroutine(IncrementDashMeter());
        StartCoroutine(IncrementDefensiveAbility());
        StartCoroutine(IncreaseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        CallInTitan();
        EmbarkTitan();
        Dash();
        ActivateDefensiveAbility();
        defensiveAbilityShield.SetActive(defensiveAbilityActivated);
        CoreAbility();

        pilotHealthBarImage.GetComponent<Image>().fillAmount = playerPilotCurrentHealth  * 1f/ playerPilotMaxHealth;
        titanHealthBarImager.GetComponent<Image>().fillAmount = playerTitanCurrentHealth * 1f/ playerTitanMaxHealth;
        dashBarImage.GetComponent<Image>().fillAmount = dashMeter * 1f/ dashMeterMaxValue;
        titanFallBarImage.GetComponent<Image>().fillAmount = titanFallMeter * 1f/ titanFallMaxValue;
        defensiveAbilityBarText.GetComponent<Text>().text = (defensiveAbilityMaxValue - defensiveAbilityMeter) + "";
        coreAbilityBarImage.GetComponent<Image>().fillAmount = coreAbilityMeter * 1f / coreAbilityMaxValue;

    }

    private void CallInTitan()
    {
        if (Input.GetButtonDown("Call Titan"))
        {
            if (titanFallMeter >= titanFallMaxValue && !titanExists)
            {
                titanFallMeter = 0;
                playerTitan.transform.position = new Vector3(transform.position.x, playerTitan.transform.position.y, transform.position.z);
                playerTitan.SetActive(true);
                titanExists = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            titanFallMeter = titanFallMaxValue;
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
                health.currentHealth = playerTitanCurrentHealth;
            }

            else if (titanEmbarked)
            {
                playerTitan.SetActive(true);
                titanEmbarked = false;
                transform.localScale -= new Vector3(scalingValue, scalingValue, scalingValue); ;
                playerTitan.transform.position = transform.position;
                health.currentHealth = playerPilotCurrentHealth;
            }

            GetComponent<FirstPersonController>().titanMode = titanEmbarked;
            GetComponent<Crouch>().canCrouch = !titanEmbarked;
            titanHUD.SetActive(titanEmbarked);
            pilotHUD.SetActive(!titanEmbarked);
        }
    }

    public void IncrementTitanMeter(int value)
    {
        titanFallMeter = Mathf.Min(titanFallMeter + value, titanFallMaxValue);
    }

    IEnumerator IncrementDashMeter()
    {
        while(true)
        {
            yield return new WaitForSeconds(timeToIncreaseDash);
            dashMeter = Mathf.Min(dashMeter + 1, dashMeterMaxValue);
        }
    }

    public void Dash()
    {
        if (Input.GetButtonDown("Jump") && dashMeter > 0 && titanEmbarked)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            GetComponent<CharacterController>().Move(new Vector3(transform.right.x * horizontal * 150, 0,transform.forward.z * vertical * 150));

            dashMeter -= 1;
            StartCoroutine(MakePlayerInvincible());
        }
    }

    public void ActivateDefensiveAbility()
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
            if (coreAbilityMeter >= coreAbilityMaxValue && titanEmbarked)
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
        coreAbilityActivated = true;
        yield return new WaitForSeconds(2f);
        coreAbilityActivated = false;
        GetComponent<FirstPersonController>().autoAim = false;
    }

    public void PlayerKilledEnemy(int amountToIncrease)
    {
        numberOfEnemiesKilled++;

        if (titanEmbarked && !coreAbilityActivated)
        {
            coreAbilityMeter = Mathf.Min(coreAbilityMeter + amountToIncrease, coreAbilityMaxValue);
        }

        titanFallMeter = Mathf.Min(titanFallMeter + amountToIncrease, titanFallMaxValue);
    }

    private IEnumerator IncreaseHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            secondsPassedSinceLastDamage += 1;
            if (secondsPassedSinceLastDamage == timeToRegenerateHealth)
            {
                secondsPassedSinceLastDamage = 0;
                playerPilotCurrentHealth += amountOfHealthToRegenerate;
            }
        }
    }

    public void SetHealth(int newHealth)
    {
        if (titanEmbarked)
        {
            playerTitanCurrentHealth = newHealth;
        }
        else
        {
            playerPilotCurrentHealth = newHealth;
        }
    }

    private IEnumerator MakePlayerInvincible()
    {
        // This is just in case we make the player invincible for the cheat
        if (health.isInvincible)
        {
            yield return new WaitForSeconds(0.1f);
        }
        health.isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        health.isInvincible = false;
    }
}
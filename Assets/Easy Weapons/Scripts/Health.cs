/// <summary>
/// Health.cs
/// Author: MutantGopher
/// This is a sample health script.  If you use a different script for health,
/// make sure that it is called "Health".  If it is not, you may need to edit code
/// referencing the Health component from other scripts
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private GameManager gameManager = GameManager.instance;
    public GameObject FPS;

	public bool canDie = true;					// Whether or not this health can die
	
	public float startingHealth = 100.0f;		// The amount of health to start with
	public float maxHealth = 100.0f;			// The maximum amount of health
	public float currentHealth;				// The current ammount of health

	public bool replaceWhenDead = false;		// Whether or not a dead replacement should be instantiated.  (Useful for breaking/shattering/exploding effects)
	public GameObject deadReplacement;			// The prefab to instantiate when this GameObject dies
	public bool makeExplosion = false;			// Whether or not an explosion prefab should be instantiated
	public GameObject explosion;				// The explosion prefab to be instantiated

    public bool isTitan;
	public bool isPlayer = false;				// Whether or not this health is the player
	public GameObject deathCam;					// The camera to activate when the player dies

	private bool dead = false;					// Used to make sure the Die() function isn't called twice
    public bool isInvincible = false;

    private CallTitan callTitan;
	// Use this for initialization
	void Start()
	{
		// Initialize the currentHealth variable to the value specified by the user in startingHealth
		currentHealth = startingHealth;
        callTitan = GetComponent<CallTitan>();
	}

	public void ChangeHealth(float amount)
	{
        // Change the health by the amount specified in the amount variable
        // titan gets damage by the rocket and grenade launcher only 
       
        if (isInvincible)
        {
            return;
        }

        if ((!isTitan) || (amount <= -100)) { 
            currentHealth += amount; 
        }

        GetComponentInChildren<Image>().fillAmount = currentHealth * 1f / maxHealth;

        //if (currentHealth > 0 && amount < 0)
        //{

        //}
		// If the health runs out, then Die.
		if (currentHealth <= 0 && !dead && canDie)
			Die();

        if (isPlayer)
        {
            callTitan.secondsPassedSinceLastDamage = 0;
            callTitan.SetHealth((int)currentHealth);
        }

        TitanCon titanController = GetComponent<TitanCon>();
        if (titanController != null && amount <= -100)
        {
            titanController.anim.SetBool("isHit", true);
            StartCoroutine(GetHit());
        }

        // Make sure that the health never exceeds the maximum health
        else if (currentHealth > maxHealth)
			currentHealth = maxHealth;
	}

	public void Die()
	{
		// This GameObject is officially dead.  This is used to make sure the Die() function isn't called again
		dead = true;

		// Make death effects
		if (replaceWhenDead)
			Instantiate(deadReplacement, transform.position, transform.rotation);
		if (makeExplosion)
			Instantiate(explosion, transform.position, transform.rotation);

		//if (isPlayer && deathCam != null)
		//	deathCam.SetActive(true);

        if (isPlayer)
        {
            gameManager.LoadGameOver();
        }

        TitanCon titanController = GetComponent<TitanCon>();
        if (titanController != null) {
            titanController.anim.SetBool("isDead", true);
            CallTitan callTitan = FPS.GetComponent<CallTitan>();
            callTitan.PlayerKilledEnemy(callTitan.killingTitanPoints);
        }

        else
        {
            GetComponent<PilotEnemyCon>().anim.SetBool("isDead", true);
            CallTitan callTitan = FPS.GetComponent<CallTitan>();
            callTitan.PlayerKilledEnemy(callTitan.killingPilotPoints);
        }

        StartCoroutine(DestroyEnemy());

		// Remove this GameObject from the scene
		
	}

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    IEnumerator GetHit()
    {
        yield return new WaitForSeconds(1);
        TitanCon titanController = GetComponent<TitanCon>();
        titanController.anim.SetBool("isHit", false);
    }
}

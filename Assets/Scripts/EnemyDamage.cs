using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int currentHealth;
    private int maxHealthValue;

    public int pilotDyingMeterIncrementer = 10;
    public int titanDyingMeterIncrementer = 50;

    private void Start()
    {
        if (this.name.Contains("Solidier"))
        {
            maxHealthValue = 100;
        }
        else if (this.name.Contains("Warrior"))
        {
            maxHealthValue = 400;
        }
        else
        {
            Debug.LogWarning("Unknown Enemy");
        }
        currentHealth = maxHealthValue;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (this.name.Contains("Solidier"))
            {
                FindObjectOfType<CallTitan>().titanFallMeter += pilotDyingMeterIncrementer;
            }
            else if (this.name.Contains("Warrior"))
            {
                FindObjectOfType<CallTitan>().titanFallMeter += titanDyingMeterIncrementer;
            }
            else
            {
                Debug.LogWarning("Unknown Enemy");
            }
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        // TODO Dying animation
        yield return new WaitForSeconds(2f);
        Destroy(this);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health;
    public GameObject enemies;

    private void Start()
    {
        if (enemies.tag == "enemy")
        {
            health = 100f;

        }

        if (enemies.tag == "Titan")
        {
            health = 400f;

        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

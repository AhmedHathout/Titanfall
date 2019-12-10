using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PilotEnemyCon : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public Animator anim;
    public Transform[] points;
    public int pointIndex = 0;
    public bool chasing;
    //public int health;

    [Tooltip("0 is assult rifle, 1 is shotgun")]
    public int weaponType;
    private int assualtRifleDamage = 10;
    private int shotgunDamage = 50;
    public GameObject FPS;

    public bool isFiring = false;
    public bool isDead = false;
    private Health enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        chasing = false;
        enemyHealth = GetComponent<Health>();
    }

    void GotoNext()
    {
        agent.destination = points[pointIndex].position;
        pointIndex = (pointIndex + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasing)
        {
            if (agent.remainingDistance < 20)
            {
                GotoNext();
            }
            transform.Translate(0, 0, Time.deltaTime * 45);
        }
        if (transform.position.z - FindObjectOfType<Crouch>().transform.position.z < 332 && !chasing)
        {
            agent.destination = target.position;
            chasing = true;
        }
        if (agent.remainingDistance > 200 && chasing)
        {
            agent.destination = target.position;
            transform.Translate(0, 0, Time.deltaTime * 90);
            anim.SetBool("isRunning", true);
            anim.SetBool("isShooting", false);
        }
        if (agent.remainingDistance < 200 && chasing)
        {
            agent.destination = target.position;
            anim.SetBool("isShooting", true);
            
            if (!isFiring)
            {
                StartCoroutine(Fire());
            }
        }
        if (enemyHealth.currentHealth <= 0)
        {
            isDead = true;
            //anim.SetBool("isDead", true);
            //agent.destination = transform.position;
        }
    }

    IEnumerator Fire()
    {
        isFiring = true;
        yield return (new WaitForSeconds(1.5f));

        if (agent.remainingDistance < 200 && !isDead)
        {
            anim.SetTrigger("isFire");
            Health health = FPS.GetComponent<Health>();
            if (weaponType == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    health.ChangeHealth(-assualtRifleDamage);
                    yield return new WaitForSeconds(0.2f);
                }
            }

            else
            {
                health.ChangeHealth(-shotgunDamage);
            }
        }
        
        isFiring = false;
    }
}

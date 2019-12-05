using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TitanCon : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    public Transform[] points;
    public int pointIndex = 0;
    public Animator anim;
    public bool chasing;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        chasing = false;
        health = 400;
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
            transform.Translate(0, 0, 2);
        }
        if (transform.position.z - FindObjectOfType<Crouch>().transform.position.z < 332 && !chasing)
        {
            agent.destination = target.position;
            chasing = true;
        }
        if (agent.remainingDistance > 200 && chasing)
        {
            agent.destination = target.position;
            transform.Translate(0, 0, 2);
            anim.SetBool("isRunning", true);
            anim.SetBool("isShooting", false);
        }
        if(agent.remainingDistance < 200 && chasing)
        {
            agent.destination = target.position;
            anim.SetBool("isShooting", true);
            StartCoroutine("Fire");
        }
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            agent.destination = transform.position;
        }
    }

    IEnumerator Fire()
    {
        yield return (new WaitForSeconds(1.5f));
        anim.SetTrigger("isFire");
    }
}

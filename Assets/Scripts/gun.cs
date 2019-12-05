
using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 65f ;
    public float ammo = 35f;
    public float rate = 10f;
    public ParticleSystem fire; 
    public Camera cam;
    private float next = 0f;

    public GameObject impact;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")&& Time.time >= next )
        {
            next = Time.time + 1f / rate;
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit hit;
        fire.Play();
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (hit.transform.tag == "enemy" )
            {
                enemy.TakeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal *50f);
            }

            GameObject i = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(i, 2f);
        }
    }
}


using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float damage = 70f;
    public float range = 4f;
    public float ammo = 12f;
    public float rate = 3f;
    public ParticleSystem fire;
    public Camera cam;
    private float next = 0f;

    public GameObject impact;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= next)
        {
            next = Time.time + 1f / rate;
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit hit;
        fire.Play();
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            GameObject i = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(i,2f);
        }
    }
}

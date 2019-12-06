using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CallTitan : MonoBehaviour
{

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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncrementDashMeter());
    }

    // Update is called once per frame
    void Update()
    {
        CallInTitan();
        EmbarkTitan();
        Dash();
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

}
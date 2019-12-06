using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CallTitan : MonoBehaviour
{

    public GameObject playerTitan;
    public int titanMeter = 0;
    public static int fullMeter = 100;

    public static int scalingValue = 30;

    public bool titanEmbarked = false;
    public int embarkDistance = 50;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CallInTitan();
        EmbarkTitan();
    }

    private void CallInTitan()
    {
        if (Input.GetButtonDown("Call Titan"))
        {
            Debug.Log("Calling Titan");
            if (titanMeter >= fullMeter)
            {
                titanMeter = 0;
                playerTitan.transform.position = new Vector3(transform.position.x, playerTitan.transform.position.y, transform.position.z);
                playerTitan.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            titanMeter = fullMeter;
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


}
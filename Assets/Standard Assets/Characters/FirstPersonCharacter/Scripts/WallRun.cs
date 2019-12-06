using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WallRun : MonoBehaviour
{
    private bool isWallR = false;
    private bool isWallL = false;

    private RaycastHit hitR;
    private RaycastHit hitL;

    private int jumpCount = 0;

    private CharacterController cc;
    private FirstPersonController FPS;
    private Rigidbody rb;

    public float runTime;

    private float m_OriginalGravityMultiplier;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        FPS = GetComponent<FirstPersonController>();
        rb = GetComponent<Rigidbody>();

        m_OriginalGravityMultiplier = FPS.m_GravityMultiplier;
    }

    private void Update()
    {
        if (cc.isGrounded)
        {
            jumpCount = 0;
        }

        if (!cc.isGrounded && jumpCount <= 1)
        {

            if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
            {
                if (hitL.transform.tag == "Wall")
                {
                    isWallL = true;
                    isWallR = false;

                    jumpCount += 1;

                    rb.useGravity = false;
                    FPS.m_GravityMultiplier = 0;
                    StartCoroutine(AfterRun());
                }
            }

            if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
            {
                if (hitR.transform.tag == "Wall")
                {
                    isWallR = true;
                    isWallL = false;

                    jumpCount += 1;

                    rb.useGravity = false;
                    FPS.m_GravityMultiplier = 0;
                    StartCoroutine(AfterRun());
                }
            }
        }
    }

    IEnumerator AfterRun()
    {
        yield return new WaitForSeconds(runTime);
        isWallL = false;
        isWallR = false;
        rb.useGravity = true;
        FPS.m_GravityMultiplier = m_OriginalGravityMultiplier;
    }
}

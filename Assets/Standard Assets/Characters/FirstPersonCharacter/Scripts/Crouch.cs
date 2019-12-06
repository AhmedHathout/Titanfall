using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;

    private bool m_Crouch = false;
    private float m_OriginalHeight;
    public bool canCrouch = true;

    [SerializeField]
    private float m_CrouchHeight;

    public KeyCode crouchKey = KeyCode.C;

    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        m_OriginalHeight = m_CharacterController.height;
        m_CrouchHeight = m_OriginalHeight * 0.5f;
    }

    private void Update()
    {
        // This if is to handle running while crouching. The player will stand up and run.
        if (!firstPersonController.m_IsWalking)
        {
            m_Crouch = false;
        }
        if (Input.GetKeyDown(crouchKey))
        {
            m_Crouch = !m_Crouch;
        }
        CheckCrouch();
    }

    private void CheckCrouch()
    {
        if (m_Crouch == true && canCrouch)
        {
            m_CharacterController.height = m_CrouchHeight;
        }
        else
        {
            m_CharacterController.height = m_OriginalHeight;
        }
    }
}

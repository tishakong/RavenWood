﻿// Script by Marcelli Michele

using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;
    Animator anim;

    public bool LockOpen = false;
    public GameObject drawer;

    public int[] _numberPassword = { 0, 0, 0, 0 };

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
        anim = GetComponent<Animator>();
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            // Correct password entered
            Debug.Log("Password correct");

            anim.SetBool("isOpen", true);
            LockOpen = true;
            Invoke("DisableLock", 2f);
            EnableDrawer();

            // Disable blinking effect after correct password
            DisableBlinkingEffect();
        }
    }

    private void DisableBlinkingEffect()
    {
        foreach (var ruller in _moveRull._rullers)
        {
            PadLockEmissionColor emissionColor = ruller.GetComponent<PadLockEmissionColor>();

            // Check if the component exists before calling the method
            if (emissionColor != null)
            {
                emissionColor._isSelect = false;
                emissionColor.DisableBlinkingMaterial();
            }
        }
    }

    void DisableLock()
    {
        gameObject.SetActive(false);
    }

    void EnableDrawer()
    {
        Collider drawerCollider = drawer.GetComponent<Collider>();

        if (drawerCollider != null)
        {
            drawerCollider.enabled = true;
        }
        else
        {
            Debug.Log("비어있음");
        }
    }
}

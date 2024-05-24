using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDetector : MonoBehaviour
{
    [SerializeField] private Signaling _signaling;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("IN");

        if (other.TryGetComponent(out PlayerInput player))
            _signaling.ActivateSignaling(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OUT");

        if (other.TryGetComponent(out PlayerInput player))
            _signaling.ActivateSignaling(false);
    }
}

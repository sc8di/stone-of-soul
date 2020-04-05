using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEndVFX : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject endVFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            endVFX.SetActive(true);
        }
    }
}

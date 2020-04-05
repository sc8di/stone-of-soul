using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] targets;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            foreach (GameObject target in targets)
            {
                Debug.Log("Activate");
                target.SendMessage("Activate");
            }
        }
    }
}

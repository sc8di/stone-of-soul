using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Managers.Mission == null)
        {
            Debug.Log("End of this level");
            return;
        }

        if(other.gameObject == player) Managers.Mission.ReachObjective();
    }
}
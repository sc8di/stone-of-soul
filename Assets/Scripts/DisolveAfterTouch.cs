using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveAfterTouch : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Destroy(gameObject);
        }
    }
}

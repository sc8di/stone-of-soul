using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveFromTime : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) StartCoroutine(DisolveAfterTime());
    }

    private IEnumerator DisolveAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

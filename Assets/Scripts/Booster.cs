using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject splash;
    public float boostSpeed = 20f;

    private Rigidbody playerRB;
    private Material emission;
    private Material distortion;
    private bool activate = false;

    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
        emission = GetComponent<MeshRenderer>().materials[2];
        distortion = GetComponent<MeshRenderer>().materials[0];

        Activate();
    }

    private void Activate()
    {
        if (activate)
        {
            emission.SetColor("_Color", Color.cyan * 6);
            distortion.SetFloat("_distortionPower", .75f);
            circle.SetActive(true);
            splash.SetActive(true);
        }
        else
        {
            emission.SetColor("_Color", Color.black);
            distortion.SetFloat("_distortionPower", 0f);
            circle.SetActive(false);
            splash.SetActive(false);
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            activate = true;
            Activate();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (activate && other.gameObject == player)
        {
            playerRB.AddForce(Vector3.up * boostSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            activate = false;
            Activate();
        }
    }
}
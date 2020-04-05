using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("-1 live");

            if (Managers.Player == null)
            {
                Debug.Log("Managers not exists.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
            
            Managers.Player.ChangeHealth(-1);
        }
    }
}

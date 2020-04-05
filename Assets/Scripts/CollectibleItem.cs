using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private VisualEffect before;
    [SerializeField] private VisualEffect after;

    public float timeDelay = .2f;
    public int countScoreAdd = 100;

    private bool activate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (Managers.Player == null) return;
        
        if (other.gameObject == player && !activate)
        {
            before.SetInt("Capacity", 0);
            after.SetInt("Capacity", 100000);
            StartCoroutine(AddScore());
            activate = true;
        }
    }

    private IEnumerator AddScore()
    {
        float curTime = 0;
        int curScoreAdd = 0;
        do
        {
            if (curTime >= timeDelay)
            {
                Managers.Player.ChangeScore(1);
                curScoreAdd += 1;
            }
            curTime += Time.deltaTime;
            yield return null;
        } while (curScoreAdd <= countScoreAdd);
        
        yield return new WaitForSeconds(1.5f);
        after.SetInt("Capacity", 0);
        Destroy(this);
    }
}
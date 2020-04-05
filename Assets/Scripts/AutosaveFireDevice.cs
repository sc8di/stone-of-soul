using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class AutosaveFireDevice : MonoBehaviour
{
    [SerializeField] private VisualEffect[] fireSmall;
    [SerializeField] private VisualEffect fireLarge;

    private bool _activate = false;

    public void Activate()
    {
        if (!_activate)
        {
            fireLarge.SetInt("Capacity", 0);
            
            foreach (VisualEffect fire in fireSmall)
            {
                fire.SetInt("Capacity", 100000);
            }
            _activate = true;
        }
    }
}

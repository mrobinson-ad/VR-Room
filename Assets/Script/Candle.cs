using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public ParticleSystem flame;

    public Collider lighter;
    public bool isOn = false;
    public event Action OnCandleChange;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other == lighter && lighter.gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            
            flame.Play();
            isOn = true;
            OnCandleChange?.Invoke();
        }
        else 
        {
            flame.Stop();
            isOn = false;
            OnCandleChange?.Invoke();
        }
    }
}

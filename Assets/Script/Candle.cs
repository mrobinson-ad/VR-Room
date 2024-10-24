using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public ParticleSystem flame;

    public Collider lighter;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other == lighter && lighter.gameObject.GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            flame.Play();
        }
    }
}

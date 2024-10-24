using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public ParticleSystem water;

    public ParticleSystem fire;

    public GameObject light;

    public GameObject key;

    public Transform can;

    void Update()
    {
        Vector3 rotation = can.eulerAngles;
        float normalizedXRotation = NormalizeAngle(rotation.x);

        if (normalizedXRotation > 30f)
        {
            if (!water.isPlaying)
            {
                water.Play();
            }
        }
        else
        {
            if (water.isPlaying)
            {
                water.Stop();
            }
        }
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180)
            angle -= 360;
        return angle;
    }

    private void OnParticleTrigger() 
    {
        if (fire.isPlaying)
        {
            fire.Stop();
            light.SetActive(false);
            key.SetActive(true);
        }
    }

}

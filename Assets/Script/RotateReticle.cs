using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateReticle : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(0, Time.deltaTime * 20, 0);
    }
}

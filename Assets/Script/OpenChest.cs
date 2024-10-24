using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Transform lid;

    public void OpenLid()
    {
        StartCoroutine(LidCo());
    }

    private IEnumerator LidCo()
    {
        float startRotation = lid.localEulerAngles.x;       
        float elapsedTime = 0f;
        while (elapsedTime < 1.5)
        {
            elapsedTime += Time.deltaTime;
            float newXRotation = Mathf.Lerp(0, -60f, elapsedTime / 1.5f);
            lid.localEulerAngles = new Vector3(newXRotation, lid.localEulerAngles.y, lid.localEulerAngles.z);
            yield return null; 
        }
        lid.localEulerAngles = new Vector3(-60f, lid.localEulerAngles.y, lid.localEulerAngles.z);
    }
}

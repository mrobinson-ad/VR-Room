using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour
{
    public Transform cover;

    public void OpenNote()
    {
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        float startRotation = cover.localEulerAngles.z;       
        float elapsedTime = 0f;
        while (elapsedTime < 2.5f)
        {
            elapsedTime += Time.deltaTime;
            float newZRotation = Mathf.Lerp(0, -185, elapsedTime / 1.5f);
            cover.localEulerAngles = new Vector3(cover.localEulerAngles.x, cover.localEulerAngles.y, newZRotation);
            yield return null; 
        }
        cover.localEulerAngles = new Vector3(cover.localEulerAngles.x, cover.localEulerAngles.y, -185);
    }
}

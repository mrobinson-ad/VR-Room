using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI time;

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        time.text = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2");
        yield return new WaitForSeconds(60);
        StartCoroutine(UpdateTime());
    }
}

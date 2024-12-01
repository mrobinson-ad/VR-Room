using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public Candle blueCandle;
    public Candle orangeCandle;
    public Candle purpleCandle;
    public Transform door;

    private void OnEnable()
    {
        blueCandle.OnCandleChange += CheckCandles;
        orangeCandle.OnCandleChange += CheckCandles;
        purpleCandle.OnCandleChange += CheckCandles;
    }

    private void OnDisable()
    {
        blueCandle.OnCandleChange -= CheckCandles;
        orangeCandle.OnCandleChange -= CheckCandles;
        purpleCandle.OnCandleChange -= CheckCandles;
    }
    public void OpenDoor()
    {
        StartCoroutine(DoorCo());
    }

    private void CheckCandles()
    {
        if (!blueCandle.isOn && orangeCandle.isOn && purpleCandle.isOn)
        {
            OpenDoor();
        }
    }

    private IEnumerator DoorCo()
    {    
        float elapsedTime = 0f;
        while (elapsedTime < 1.5)
        {
            elapsedTime += Time.deltaTime;
            float newYRotation = Mathf.Lerp(0, -110f, elapsedTime / 1.5f);
            door.localEulerAngles = new Vector3(door.localEulerAngles.x, newYRotation, door.localEulerAngles.z);
            yield return null; 
        }
        door.localEulerAngles = new Vector3(door.localEulerAngles.z, -110f, door.localEulerAngles.z);
    }

}

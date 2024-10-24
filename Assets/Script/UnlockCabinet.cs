using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;

public class UnlockCabinet : MonoBehaviour
{
    public XRGrabInteractable[] doors;

    public Transform socket;

    public void StartUnlock()
    {
        StartCoroutine(Unlock());
    }

    private IEnumerator Unlock()
    {
        float startRotation = socket.localEulerAngles.y;       
        float elapsedTime = 0f;
        while (elapsedTime < 1.5)
        {
            elapsedTime += Time.deltaTime;
            float newYRotation = Mathf.Lerp(0, 90f, elapsedTime / 1.5f);
            socket.localEulerAngles = new Vector3(socket.localEulerAngles.x, newYRotation, socket.localEulerAngles.z);
            yield return null;  
        }
        socket.localEulerAngles = new Vector3(socket.localEulerAngles.x, 90, socket.localEulerAngles.z);
        foreach( XRGrabInteractable door in doors)
        {
            door.interactionLayers = InteractionLayerMask.GetMask("Default");
        }
    }
    

}

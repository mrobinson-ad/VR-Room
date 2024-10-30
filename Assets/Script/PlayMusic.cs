using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayMusic : MonoBehaviour
{
    public GameObject vinylRef;
    public XRSocketInteractor socket;

    public Transform attach;

    public Transform handle;

    public bool isPlaying = false;

    /// <summary>
    /// Toggles playing status
    /// </summary>
    public void Toggle()
    {
        if (socket.hasSelection)
        {
            isPlaying = !isPlaying;
            StartCoroutine(SetHandle(isPlaying));
        }
    }

    /// <summary>
    /// Spins the vinyl when music is playing
    /// </summary>
    private void Update()
    {
        if (isPlaying)
            attach.Rotate(0, Time.deltaTime * 30, 0);
    }

    /// <summary>
    /// Sets the clip attached to the vinyl on the audio manager when socketed
    /// </summary>
    public void SetClip()
    {
        XRBaseInteractable vinyl = (XRBaseInteractable)socket.interactablesSelected[0];
        AudioClip clip = vinyl.gameObject.GetComponent<AudioSource>().clip;
        AudioManager.Instance.audioSource.clip = clip;
    }

    /// <summary>
    /// If vinyls are active unsets the clip from the audio manager when unsocketed
    /// </summary>
    public void UnsetClip()
    {
        if (vinylRef.activeInHierarchy)
        {
            AudioManager.Instance.audioSource.clip = null;
            if (isPlaying)
            {
                isPlaying = false;
                StartCoroutine(SetHandle(false));
            }
        }
    }
    
    /// <summary>
    /// Lerps the position of the handle needle depending on the play status
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private IEnumerator SetHandle(bool c)
    {
        float startRotation = handle.localEulerAngles.y;
        float endRotation = c ? 55f : 0f;
        float elapsedTime = 0f;
        while (elapsedTime < 1.5)
        {
            elapsedTime += Time.deltaTime;
            float newYRotation = Mathf.Lerp(startRotation, endRotation, elapsedTime / 1.5f);
            handle.localEulerAngles = new Vector3(handle.localEulerAngles.x, newYRotation, handle.localEulerAngles.z);
            yield return null; 
        }
        handle.localEulerAngles = new Vector3(handle.localEulerAngles.x, endRotation, handle.localEulerAngles.z);
        if (isPlaying != AudioManager.Instance.IsPlaying())
            AudioManager.Instance.TogglePlay();
    }
}

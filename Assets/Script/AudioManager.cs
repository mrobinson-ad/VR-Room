using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;


    private void Awake() 
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


    public void TogglePlay()
    {
        bool isPlaying = !IsPlaying();
        SetPlay(isPlaying);
    }

    public void SetPlay(bool playAudio)
    {
        if (playAudio)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}

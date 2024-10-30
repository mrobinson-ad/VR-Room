using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

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

    /// <summary>
    /// Toggles playing status
    /// </summary>
    public void TogglePlay()
    {
        bool isPlaying = !IsPlaying();
        SetPlay(isPlaying);
    }

    /// <summary>
    /// Plays or pauses depending on playing status
    /// </summary>
    /// <param name="playAudio"></param>
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
    
    /// <summary>
    /// Returns playing status
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}

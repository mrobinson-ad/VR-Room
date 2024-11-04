using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    public AudioSource audioSource;
    public AudioMixer audioMixer;
    private float musicVolume;

    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = Mathf.Log(value) * 20;
            PlayerPrefs.SetFloat("Master", value);
            audioMixer.SetFloat("Master", musicVolume);
        }
    }

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

        MusicVolume = PlayerPrefs.GetFloat("Master", 0.5f);
    }

    private void Start() 
        {
            audioMixer.SetFloat("Master", MusicVolume);
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

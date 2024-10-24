using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    public VideoPlayer tvScreen;

    public void ToggleVideo()
    {
        bool isPlaying = !IsPlaying();
        SetPlay(isPlaying);
    }

    public void SetPlay(bool playVideo)
    {
        if (playVideo)
        {
            tvScreen.Play();
        }
        else
        {
            tvScreen.Pause();
        }
    }

    public bool IsPlaying()
    {
        return tvScreen.isPlaying;
    }
}

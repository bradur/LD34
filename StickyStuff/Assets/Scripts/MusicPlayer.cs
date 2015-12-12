// Date   : 12.12.2015 15:39
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    [SerializeField]
    private AudioSource themeSong;
    [SerializeField]
    private bool mute = false;

    public void StartPlaying()
    {
        if (!themeSong.isPlaying) {
            if (!mute)
            {
                themeSong.Play();
            }
        }
        else
        {
            if (!mute)
            {
                themeSong.UnPause();
            }
        }
    }

    public void Pause()
    {
        if (!themeSong.isPlaying)
        {
            themeSong.Pause();
        }
    }
}

// Date   : 12.12.2015 15:39
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    [SerializeField]
    private AudioSource themeSong;

    public void StartPlaying()
    {
        if (!themeSong.isPlaying) { 
            themeSong.Play();
        }
        else
        {
            themeSong.UnPause();
        }
    }

    public void Pause()
    {
        themeSong.Pause();
    }
}

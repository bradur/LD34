// Date   : 12.12.2015 15:40
// Project: Sticky Stuff
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    Button,
    Turn,
    Finish,
    ObjectComesIntoView,
    Collision
}

public class SoundPlayer : MonoBehaviour {

    public List<AudioSource> buttonSounds = new List<AudioSource>();
    public List<AudioSource> turnSounds = new List<AudioSource>();
    public List<AudioSource> finishSounds = new List<AudioSource>();
    public List<AudioSource> objectComesIntoViewSounds = new List<AudioSource>();
    public List<AudioSource> collisionSounds = new List<AudioSource>();

    [SerializeField]
    private bool mute = false;
    private List<AudioSource> soundList = new List<AudioSource>();

    public void PlaySound(SoundType soundType)
    {
        Play(soundType);
    }

    public void PlayLeveledSound(SoundType soundType, int soundLevel)
    {
        Play(soundType, soundLevel);
    }

    private void Play(SoundType soundType, int soundLevel = -1)
    {
        if (!mute)
        {
            if (soundType == SoundType.Button)
            {
                soundList = buttonSounds;
            }
            else if (soundType == SoundType.Turn)
            {
                soundList = turnSounds;
            }
            else if (soundType == SoundType.Finish)
            {
                soundList = finishSounds;
            }
            else if (soundType == SoundType.ObjectComesIntoView)
            {
                soundList = objectComesIntoViewSounds;
            }
            else if (soundType == SoundType.Collision)
            {
                soundList = collisionSounds;
            }

            if (soundList.Count > 0)
            {
                if (soundLevel == -1)
                {
                    soundList[Random.Range(0, soundList.Count)].Play();
                }
                else
                {
                    if (soundLevel >= soundList.Count)
                    {
                        soundLevel = soundList.Count - 1;
                    }
                    soundList[soundLevel].Play();
                }
            }
            else
            {
                Debug.Log("ERROR: Empty sound list!");
            }
        }
    }

}

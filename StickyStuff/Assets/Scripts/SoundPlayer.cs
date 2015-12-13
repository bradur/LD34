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

    private AudioSource loopedSound;

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

    public void StopLoopedSound()
    {
        if (!mute) { 
            if (loopedSound.isPlaying)
            {
                loopedSound.Stop();
            }
        }
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
                int soundIndex = 0;
                if (soundLevel == -1)
                {
                    soundIndex = Random.Range(0, soundList.Count);
                }
                else
                {
                    if (soundLevel >= soundList.Count)
                    {
                        soundLevel = soundList.Count - 1;
                    }
                    soundIndex = soundLevel;
                }
                if (soundList[soundIndex].loop)
                {
                    if (loopedSound != null)
                    {
                        if (!loopedSound.isPlaying)
                        {
                            loopedSound = soundList[soundIndex];
                            soundList[soundIndex].Play();
                        }
                    }
                    else
                    {
                        loopedSound = soundList[soundIndex];
                        soundList[soundIndex].Play();
                    }

                }
                else
                {
                    soundList[soundIndex].Play();
                }
            }
            else
            {
                Debug.Log("ERROR: Empty sound list!");
            }
        }
    }

}

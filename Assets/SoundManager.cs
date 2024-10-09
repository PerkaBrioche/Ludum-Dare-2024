using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource SOURCE_Audio;
    public AudioSource SOURCE_Musique;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public List<AudioClip> List_EncreAudio;
    public List<AudioClip> List_OeilAudio;
    public List<AudioClip> List_FantassinAudio;
    public List<AudioClip> List_PoingAudio;

    public void PlaySoundEncre(int Index)
    {
        SOURCE_Audio.PlayOneShot(List_EncreAudio[Index]);
    }
    public void PlaySoundOeil(int Index)
    {
        SOURCE_Audio.PlayOneShot(List_OeilAudio[Index]);
    }
    public void PlaySoundFantassin(int Index)
    {
        SOURCE_Audio.PlayOneShot(List_FantassinAudio[Index]);
    }
    public void PlaySoundpoing(int Index)
    {
        SOURCE_Audio.PlayOneShot(List_PoingAudio[Index]);
    }

    public void PlaySoundClip(AudioClip Clip)
    {
        SOURCE_Audio.PlayOneShot(Clip);
    }

    public void stopSound()
    {
        SOURCE_Audio.Stop();
        SOURCE_Musique.Stop();
        SOURCE_Audio.enabled = false;
        SOURCE_Musique.enabled = false;
    }
    
    
}

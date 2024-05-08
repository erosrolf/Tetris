using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Awake()
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

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(musicSounds, m => m.name == name);
        if (music == null)
        {
            Debug.Log($"Sound {name} not found!");
        }
        else
        {
            musicSource.clip = music.clip;
            musicSource.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound music = Array.Find(musicSounds, m => m.name == name);
        if (music == null)
        {
            Debug.Log($"Sound {name} not found!");
        }
        else
        {
            musicSource.clip = music.clip;
            musicSource.Stop();
        }
    }

    public void PauseMusic() => musicSource.Pause();

    public void UnPauseMusic()
    {
        Debug.Log("unpause");
        Debug.Log(musicSource.clip);
        musicSource.UnPause();
    }

    public void PlaySFX(string name, AudioSource source = null)
    {
        Sound sfx = Array.Find(sfxSounds, s => s.name == name);
        if (sfx == null)
        {
            Debug.Log($"Sound {name} not found!");
        }
        else
        {
            AudioSource audioSource = source ?? sfxSource;
            audioSource.clip = sfx.clip;
            audioSource.Play();
        }
    }
}

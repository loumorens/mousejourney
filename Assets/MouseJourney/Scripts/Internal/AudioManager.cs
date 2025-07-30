using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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

 
    public void PlayMusic(String name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound file not found");
        }
        else
        {

            musicSource.clip = s.clip;
            musicSource.Play();
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                float vol = PlayerPrefs.GetFloat("musicVolume");
                if (vol == 0)
                {
                    musicSource.mute = true;
                }
                else
                {
                    MusicVolume(vol);
                }
            }
            else
            {
                MusicVolume(0.25f);
            }
        }
    }

    public void PlaySFX(String name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sounf file not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}


using System;
using UnityEngine;
using UnityEngine.Rendering;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public MusicData[] musicSounds;
    public Sound[] sfxSounds;
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


    public void PlayMusicFromScene(String sceneName)
    {
        MusicData md = Array.Find(musicSounds, x => x.scene.name == sceneName);
        PlayMusic(md);
    }
    public void PlayMusicFromName(String sceneName)
    {
        MusicData md = Array.Find(musicSounds, x => x.name == name);
        PlayMusic(md);

    }
    private void PlayMusic(MusicData md)
    {
        if (md == null)
        {
            Debug.Log("Sound file not found");
        }
        else
        {

            musicSource.clip = md.clip;
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
    public void stopMusic(String name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Muisic not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Stop();
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

    private String searchMusicToPlayForScene(String sceneName)
    {

        MusicData scene = Array.Find(musicSounds, x => x.scene.name == sceneName);
        if (scene != null)
        {
            return scene.name;
        }
        else
        {
            Debug.Log("Sounf file not found");
            return "";
        }
    }
}


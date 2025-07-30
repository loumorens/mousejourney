using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer MouseGameMixer;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private Toggle musicMute;
    [SerializeField] private Toggle sfxMute;


    private void Start()
    {
        Debug.Log("OptionMenu::start");
        LoadVolume();
        LoadMuted();
    }

    public void SetMusicVolume()
    {
        Debug.Log("OptionMenu::SetMusicVolume()");
        float volume = musicVolumeSlider.value;
        setMusicVolume(volume);

    }
    private void setMusicVolume(float volume)
    {
        Debug.Log("OptionMenu::SetMusicVolume(volume) :: " + volume);
        MouseGameMixer.SetFloat("MusicVolume", volume);
        AudioManager.Instance.MusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        Debug.Log("OptionMenu::SetSFXVolume()");
        float volume = sfxVolumeSlider.value;
        setSfxVolume(volume);
    }

    private void setSfxVolume(float volume)
    {
        Debug.Log("OptionMenu::setSfxVolume(volume) :: " + volume);
        MouseGameMixer.SetFloat("SFXVolume", volume);
        AudioManager.Instance.SfxVolume(volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        Debug.Log("OptionMenu::LoadVolume");
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            Debug.Log("OptionMenu::LoadVolume::musicVolume exist ::" + musicVolumeSlider.value);
        }
        else
        {
            setMusicVolume(0.25f);
            Debug.Log("OptionMenu::LoadVolume::musicVolume doesn't exist - default value 0.25f");
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            Debug.Log("OptionMenu::LoadVolume::sfxVolume exist ::" + sfxVolumeSlider.value);
        }
        else
        {
            setSfxVolume(0.5f);
            Debug.Log("OptionMenu::LoadVolume::setSfxVolume doesn't exist - default value 0.25f");

        }

    }
    private void LoadMuted()
    {
        Debug.Log("OptionMenu::LoadMuted");
        if (PlayerPrefs.HasKey("musicMute"))
        {
            Debug.Log("OptionMenu::LoadVolume::musicMute exist ::" + PlayerPrefs.GetInt("musicMute"));
            if (PlayerPrefs.GetInt("musicMute") == 0)
            {
                musicMute.isOn = true;
            }
            else
            {
                musicMute.isOn = false;
            }
        }
        if (PlayerPrefs.HasKey("sfxMute"))        
        {
            Debug.Log("OptionMenu::LoadVolume::sfxMute exist ::" + PlayerPrefs.GetInt("sfxMute"));
            if (PlayerPrefs.GetInt("sfxMute") == 0)
            {
                sfxMute.isOn = true;
            }
            else
            {
                sfxMute.isOn = false;
            }
        }
    }

    public void MusicMute()
    {
        Debug.Log("OptionMenu::MusicMute");
        AudioManager.Instance.ToggleMusic();
        if (musicMute.isOn)
        {
            PlayerPrefs.SetInt("musicMute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("musicMute", 0);
        }
    }

    public void SFXMute()
    {
        Debug.Log("OptionMenu::SFXMute");
        AudioManager.Instance.ToggleSFX();
        if (sfxMute.isOn)
        {
            PlayerPrefs.SetInt("sfxMute", 1);
            Debug.Log("OptionMenu::SFXMute::true");
        }
        else
        {
            PlayerPrefs.SetInt("sfxMute", 0);
            Debug.Log("OptionMenu::SFXMute::false");
        }
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer MouseGameMixer;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;



    private void Start()
    {
        Debug.Log("OptionMenu::start");
        LoadVolume();
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

}

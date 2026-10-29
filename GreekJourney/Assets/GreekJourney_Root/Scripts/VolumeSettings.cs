using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        SetmusicVolume();
    }

    public void SetmusicVolume()
    {
        float volume = MusicSlider.value;
        AudioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    public void SetSFXVolume()
    {
        float volume = MusicSlider.value;
        AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bGMSlider;
    public Slider sESlider;

    public  float bgmVolume=0;
    public  float seVolume=0;
    private void Start()
    {

        audioMixer.GetFloat("BGM_Volume", out float bgmVolume);
        bGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SE_Volume", out float seVolume);
        sESlider.value = seVolume;
    }

    public void SetBGM(float volume)
    {
        bGMSlider.value = volume;
        audioMixer.SetFloat("BGM_Volume", volume);
    }

    public void SetSE(float volume)
    {
        sESlider.value = volume;
        audioMixer.SetFloat("SE_Volume", volume);
    }

    public void SaveVolume() 
    {
        OptionStatus.SetBGM_Volume(bGMSlider.value);
        OptionStatus.SetSE_Volume(sESlider.value);
    }
}

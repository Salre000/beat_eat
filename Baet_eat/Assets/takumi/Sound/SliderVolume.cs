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

    public static float bgmVolume=0;
    public  float seVolume=0;

    private void Start()
    {
        bGMSlider.value = bgmVolume;
        sESlider.value = seVolume;

        audioMixer.SetFloat("BGM_Volume",bgmVolume);

        audioMixer.SetFloat("SE_Volume",seVolume);
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM_Volume", volume);

        bGMSlider.value = bgmVolume;
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE_Volume", volume);
        sESlider.value = seVolume;
    }
}

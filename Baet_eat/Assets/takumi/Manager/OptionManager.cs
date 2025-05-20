using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField]SliderVolume _sliderVolume;
    private void Start()
    {
        OptioStatus.Initialize();
        SetOptionData();

    }

    public void SetOptionData() 
    {
        _sliderVolume.SetBGM(OptioStatus.GetBGM_Volume());
        _sliderVolume.SetSE(OptioStatus.GetSE_Volume());

    }
}

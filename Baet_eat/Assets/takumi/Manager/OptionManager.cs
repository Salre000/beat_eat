using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField]SliderVolume _sliderVolume;

    private int nowPage = 0;

    private List<GameObject> PageList = new List<GameObject>();
    private void Start()
    {
        OptioStatus.Initialize();
        SetOptionData();

        for (int i = 0; i < _sliderVolume.gameObject.transform.childCount; i++) 
            PageList.Add(_sliderVolume.gameObject.transform.GetChild(i).gameObject);
        NowPageShow();
    }

    public void SetOptionData() 
    {
        _sliderVolume.SetBGM(OptioStatus.GetBGM_Volume());
        _sliderVolume.SetSE(OptioStatus.GetSE_Volume());

    }
    private void NowPageShow() 
    {

        for(int i = 0; i < PageList.Count; i++) 
        {
            PageList[i].SetActive(false);

            if (i != nowPage * 2 && i != nowPage + 1) continue;

            PageList[i].SetActive(true);


        }


    }
    public void AddPage() { nowPage += 1;if (nowPage > 2) nowPage = 2; NowPageShow(); }
    public void SbuPage() { nowPage -= 1; if (nowPage < 0) nowPage = 0; NowPageShow(); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField]SliderVolume _sliderVolume;

    [SerializeField]private int nowPage = 0;

    private List<GameObject> PageList = new List<GameObject>();
    private void Start()
    {
        OptioStatus.Initialize();
        SetOptionData();

        for (int i = 0; i < _sliderVolume.gameObject.transform.childCount; i++) 
            PageList.Add(_sliderVolume.gameObject.transform.GetChild(i).gameObject);

        for(int i = 1; i < PageList.Count - 1; i++) 
        {
            PageList[i].transform.GetChild(PageList[i].transform.childCount - 1).GetComponent<Button>().onClick.AddListener(i%2==0? SbuPage: AddPage);

        }

        NowPageShow();
    }

    private void FixedUpdate()
    {
        //Debagよう
        if (Input.GetKeyDown(KeyCode.Escape)) ChengeActive();


    }

    public void ChengeActive() 
    {
        _sliderVolume.gameObject.SetActive(!_sliderVolume.gameObject.activeSelf);
    }

    //システム敵に音量を設定する関数
    public void SetOptionData() 
    {
        _sliderVolume.SetBGM(OptioStatus.GetBGM_Volume());
        _sliderVolume.SetSE(OptioStatus.GetSE_Volume());

    }
    //描画するページだけをアクティブに変更する関数
    private void NowPageShow() 
    {

        for(int i = 0; i < PageList.Count; i++) 
        {
            PageList[i].SetActive(false);

            if (i != nowPage * 2 && i != nowPage * 2 + 1) continue;

            PageList[i].SetActive(true);


        }


    }
    public void AddPage() 
    { nowPage += 1;if (nowPage > 2) nowPage = 2; NowPageShow(); }
    public void SbuPage() 
    { nowPage -= 1; if (nowPage < 0) nowPage = 0; NowPageShow(); }
}

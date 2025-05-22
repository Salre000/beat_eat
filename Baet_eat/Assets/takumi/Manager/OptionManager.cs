using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] SliderVolume _sliderVolume;

    [SerializeField] private int nowPage = 0;
    [SerializeField] private int nextPage = 0;

    [SerializeField] private List<GameObject> PageList = new List<GameObject>();

    [SerializeField] private List<RectTransform> PageRoot = new List<RectTransform>();
    [SerializeField] private GameObject DC;
    private RectTransform Bell;

    private void Awake()
    {
        OptisonUility.optionManager = this;
    }
    private void Start()
    {
        OptioStatus.Initialize();
        SetOptionData();

        //ページ以外の子供オブジェクトの数だけ低く
        for (int i = 0; i < _sliderVolume.gameObject.transform.childCount - 5; i++)
            PageList.Add(_sliderVolume.gameObject.transform.GetChild(i).gameObject);

        for (int i = 1; i < PageList.Count - 1; i++)
        {
            PageList[i].transform.GetChild(PageList[i].transform.childCount - 1).GetComponent<Button>().onClick.AddListener(i % 2 == 0 ? SbuPage : AddPage);

        }

        Bell = _sliderVolume.gameObject.transform.GetChild(_sliderVolume.gameObject.transform.childCount-1).GetComponent<RectTransform>();

        NowPageShow();
    }

    bool flipFlag = false;

    private void FixedUpdate()
    {
        PageFlip();
        DCSizeChenge();
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

        for (int i = 0; i < PageList.Count; i++)
        {
            PageList[i].SetActive(false);

            if (i != nowPage * 2 && i != nowPage * 2 + 1) continue;

            PageList[i].SetActive(true);


        }

        switch (nowPage) 
        {
            case 0:

                //DC.SetActive(false);
                break;
        }


    }

    private void DCSizeChenge() 
    {
        if (DC.transform.localScale.x > 1) return;
        DC.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);


    }

    public void DCStart() { DC.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); }
    int offsetA = 0;
    int offsetB = 1;

    private void PageFlipStart()
    {

        if (nextPage > nowPage)
        {
            offsetA = 1;
            offsetB = 0;
        }
        else
        {
            offsetA = 0;
            offsetB = 1;

        }

        PageList[nextPage * 2 + offsetA].transform.SetParent(PageRoot[3]);
        PageList[nextPage * 2 + offsetA].SetActive(true);
        PageList[nowPage * 2 + offsetB].transform.SetParent(PageRoot[2]);
        PageList[nowPage * 2 + offsetA].transform.SetParent(PageRoot[1]);
        PageList[nextPage * 2 + offsetB].transform.SetParent(PageRoot[0]);

        PageList[nextPage * 2 + offsetB].transform.eulerAngles = new Vector3(0, 90, 0);



    }
    private void PageFlip()
    {

        if (!flipFlag || nextPage == nowPage) return;

        if (PageList[nowPage * 2 + offsetA].activeSelf) PageList[nowPage * 2 + offsetA].transform.Rotate(0, 1, 0);

        if (PageList[nowPage * 2 + offsetA].transform.eulerAngles.y < 90) return;

        if (PageList[nowPage * 2 + offsetA].activeSelf)
        {
            PageList[nowPage * 2 + offsetA].SetActive(false);
            PageList[nextPage * 2 + offsetB].SetActive(true);
        }
        if (PageList[nextPage * 2 + offsetB].activeSelf) PageList[nextPage * 2 + offsetB].transform.Rotate(0, -1, 0);

        if (PageList[nextPage * 2 + offsetB].transform.eulerAngles.y < 91) return;

        for (int i = 0; i < PageList.Count; i++)
        {
            PageList[i].transform.SetParent(_sliderVolume.transform);

            PageList[i].transform.eulerAngles = Vector3.zero;

        }

        flipFlag = false;

        nowPage = nextPage;
        NowPageShow();

        //同じ親へのペアレント設定は無効化されるから一度nullを入れる
        Bell.transform.parent = null;
        Bell.transform.parent = (_sliderVolume.transform);




    }

    public void AddPage()
    { nextPage += 1; if (nextPage > 2) {nextPage = 2; return; } PageFlipStart(); flipFlag = true; }
    public void SbuPage()
    { nextPage -= 1; if (nextPage < 0) { nextPage = 0;return; } PageFlipStart(); flipFlag = true; }
}

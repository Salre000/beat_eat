using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    //スコアを表示するテキスト
    [SerializeField] private List<TextMeshProUGUI> scoreTexts = new List<TextMeshProUGUI>(MusicManager.CAPACITY);

    //クリアランクの見た目のテキスト
    [SerializeField] private List<TextMeshProUGUI> ClearRank = new List<TextMeshProUGUI>(MusicManager.CAPACITY);
    [SerializeField] private List<TextMeshProUGUI> ClearRankOutLine = new List<TextMeshProUGUI>(MusicManager.CAPACITY);

    [SerializeField] private List<TextMeshProUGUI> Plus = new List<TextMeshProUGUI>(MusicManager.CAPACITY);

    //クリアランクのカラーのオブジェクト
    [SerializeField] private List<GameObject> S = new List<GameObject>(MusicManager.CAPACITY);
    [SerializeField] private List<GameObject> A = new List<GameObject>(MusicManager.CAPACITY);
    [SerializeField] private List<GameObject> B = new List<GameObject>(MusicManager.CAPACITY);
    [SerializeField] private List<GameObject> C = new List<GameObject>(MusicManager.CAPACITY);
    [SerializeField] private List<GameObject> D = new List<GameObject>(MusicManager.CAPACITY);


    public void Awake()
    {
        //initializeの引数は曲の数
        ScoreStaus.Initialize(Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData.Count);

        MusicManager.instance.GetMusicCards();

    }

    public void Start()
    {
        List<GameObject> card = MusicManager.instance.GetMusicCards();




        for (int i = 0; i < MusicManager.CAPACITY; i++)
        {

            scoreTexts.Add(card[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());

            Plus.Add(card[i].transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>());

            ClearRank.Add(card[i].transform.GetChild(6).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            ClearRankOutLine.Add(card[i].transform.GetChild(6).transform.GetChild(1).GetComponent<TextMeshProUGUI>());

            S.Add(ClearRank[ClearRank.Count - 1].transform.GetChild(4).gameObject);
            A.Add(ClearRank[ClearRank.Count - 1].transform.GetChild(3).gameObject);
            B.Add(ClearRank[ClearRank.Count - 1].transform.GetChild(2).gameObject);
            C.Add(ClearRank[ClearRank.Count - 1].transform.GetChild(1).gameObject);
            D.Add(ClearRank[ClearRank.Count - 1].transform.GetChild(0).gameObject);



            SwitchRank(i, ScoreStaus.GetDessertClearRanks(i, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()));
            scoreTexts[scoreTexts.Count-1].text = ScoreStaus.GetDessertScore(i, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()).ToString();

        }

    }

    public void FixedUpdate()
    {

    }


    public void ChengeDifficulty()
    {


        for (int i = 0; i < MusicManager.CAPACITY; i++)
        {
            SwitchRank(i, ScoreStaus.GetDessertClearRanks(i, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()));

            scoreTexts[i].text= ScoreStaus.GetDessertScore(i, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()).ToString();


        }


    }

    private void SwitchRank(int ID, publicEnum.ClearRank clearRank)
    {
        Plus[ID].gameObject.SetActive(false);

        S[ID].gameObject.SetActive(false);
        A[ID].gameObject.SetActive(false);
        B[ID].gameObject.SetActive(false);
        C[ID].gameObject.SetActive(false);
        D[ID].gameObject.SetActive(false);


        switch (clearRank)
        {
            case publicEnum.ClearRank.None:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "ー";
                break;
            case publicEnum.ClearRank.SPlus:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "S";
                Plus[ID].gameObject.SetActive(true);
                S[ID].gameObject.SetActive(true);

                break;
            case publicEnum.ClearRank.S:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "S";
                S[ID].gameObject.SetActive(true);

                break;
            case publicEnum.ClearRank.A:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "A";
                A[ID].gameObject.SetActive(true);


                break;
            case publicEnum.ClearRank.B:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "B";
                B[ID].gameObject.SetActive(true);

                break;
            case publicEnum.ClearRank.C:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "C";
                C[ID].gameObject.SetActive(true);

                break;
            case publicEnum.ClearRank.D:
                ClearRankOutLine[ID].text = ClearRank[ID].text = "D";
                D[ID].gameObject.SetActive(true);

                break;
        }

    }





}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //難易度ごとのクリア状況のパーフェクト時に使うオブジェクト配列
    private List<List<GameObject>> PlusDifficulty = new List<List<GameObject>>(MusicManager.CAPACITY);

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

    [SerializeField] private RectTransform Content;

    //音楽のレベルを見せるテキスト
    [SerializeField] private List<TextMeshProUGUI> Level = new List<TextMeshProUGUI>(MusicManager.CAPACITY);


    [SerializeField] Color[] DifficultyColor = new Color[3];

    public void Awake()
    {

        ScoreUtility.scoreManager = this;
        //initializeの引数は曲の数
        ScoreStatus.Initialize(Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData.Count);



    }

    public void Start()
    {
        List<GameObject> card = MusicManager.instance.GetMusicCards();

        
        Content.localPosition += new Vector3(0, 125 * (ScoreStatus.nowMusic-MusicManager.NOTMUSICNUMBER), 0);


        ScoreStatus.nowMusic = -1;


        for (int i = 0; i < MusicManager.CAPACITY; i++)
        {
            if (card.Count <= i) return;

            PlusDifficulty.Add(new List<GameObject>());

            scoreTexts.Add(card[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());

            Plus.Add(card[i].transform.GetChild(6).transform.GetChild(1).GetComponent<TextMeshProUGUI>());

            ClearRank.Add(card[i].transform.GetChild(6).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            ClearRankOutLine.Add(card[i].transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshProUGUI>());

            S.Add(ClearRank[ClearRank.Count - 1].transform.Find("DRankColor").gameObject);
            A.Add(ClearRank[ClearRank.Count - 1].transform.Find("CRankColor").gameObject);
            B.Add(ClearRank[ClearRank.Count - 1].transform.Find("BRankColor").gameObject);
            C.Add(ClearRank[ClearRank.Count - 1].transform.Find("ARankColor").gameObject);
            D.Add(ClearRank[ClearRank.Count - 1].transform.Find("SRankColor").gameObject);

            Level.Add(card[i].transform.GetChild(5).transform.GetChild(0).GetComponent<TextMeshProUGUI>());

            SetLevel(i , (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber());

            SwitchRank(i, ScoreStatus.GetDessertClearRanks(i + MusicManager.NOTMUSICNUMBER, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()));
            scoreTexts[scoreTexts.Count - 1].text = ScoreStatus.GetDessertScore(i + MusicManager.NOTMUSICNUMBER, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()).ToString();


            for (int j = 0; j < 5; j++)
            {
                PlusDifficulty[i].Add(card[i].transform.GetChild(4).transform.GetChild(j).transform.GetChild(0).gameObject);

                SetDifficulty(i , (publicEnum.Difficulty)j, ScoreStatus.GetDessertDifficulty(i + MusicManager.NOTMUSICNUMBER, (publicEnum.Difficulty)j));
            }


        }


    }

    public void FixedUpdate()
    {

    }


    public void ChengeDifficulty()
    {
        List<GameObject> card = MusicManager.instance.GetMusicCards();


        for (int i = 0; i < MusicManager.CAPACITY; i++)
        {
            if (card.Count <= i) return;
            //ここが重いらしい
            SwitchRank(i , ScoreStatus.GetDessertClearRanks(i + MusicManager.NOTMUSICNUMBER, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()));

            scoreTexts[i].text = ScoreStatus.GetDessertScore(i + MusicManager.NOTMUSICNUMBER, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber()).ToString();

            SetLevel(i, (publicEnum.Difficulty)MusicManager.instance.GetDifficultyNumber());

        }


    }

    private void SwitchRank(int ID, publicEnum.ClearRank clearRank)
    {
        Plus[ID].gameObject.SetActive(false);

        S[ID].gameObject.SetActive(false);
        A[ID].gameObject.SetActive(false);
        B[ID].gameObject.SetActive(false);
        C[ID].gameObject.SetActive(false);



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

    private void SetDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates)
    {

        PlusDifficulty[ID][(int)difficulty].SetActive(false);
        switch (clearStates)
        {
            case publicEnum.ClearStates.None:
            case publicEnum.ClearStates.Unplayed:
                PlusDifficulty[ID][(int)difficulty].transform.parent.GetComponent<Image>().color = DifficultyColor[0];
                break;
            case publicEnum.ClearStates.Clear:
                PlusDifficulty[ID][(int)difficulty].transform.parent.GetComponent<Image>().color = DifficultyColor[1];
                break;
            case publicEnum.ClearStates.FullCombo:
                PlusDifficulty[ID][(int)difficulty].transform.parent.GetComponent<Image>().color = DifficultyColor[2];
                break;
            case publicEnum.ClearStates.ALLDC:
                PlusDifficulty[ID][(int)difficulty].SetActive(true);

                break;
        }



    }

    private void SetLevel(int ID, publicEnum.Difficulty difficulty) 
    {
        Level[ID].text = ScoreStatus.GetMusicLevel(ID + MusicManager.NOTMUSICNUMBER, (int)difficulty).ToString();


    }


}

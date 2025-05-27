using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class ResultScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI MusicName;
    [SerializeField] TextMeshProUGUI DifficultyName;
    [SerializeField] Image[] Rank;
    [SerializeField] Image[] circleRank;
    [SerializeField] GameObject[] RankObject;
    [SerializeField] GameObject PlusObject;
    [SerializeField] GameObject Plus;
    [SerializeField] TextMeshProUGUI RankText;
    [SerializeField] TextMeshProUGUI RankTextOutLine;
    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Image[] Success;
    [SerializeField] Image[] Miss;

    [SerializeField] Image FullConbo;
    [SerializeField] Image ALLDC;


    [SerializeField] TextMeshProUGUI FASTALL;
    [SerializeField] TextMeshProUGUI LATEALL;

    [SerializeField] Image DC;
    [SerializeField] Image FASTD;
    [SerializeField] Image FASTY;
    [SerializeField] Image LATED;
    [SerializeField] Image LATEY;
    [SerializeField] Image Jacket;
    // Start is called before the first frame update
    void Start()
    {
        MusicData musicData = Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic];
        //äyã»ÇÃñºëOÇì¸ÇÍÇÈ
        MusicName.text = musicData.name;

        DifficultyName.text = ScoreStatus.nowDifficulty.ToString();

        SetRank();
        SetJudgment();

        ComboText.text = InGameStatus.GetCombo().ToString();

        ScoreText.text = ((int)InGameStatus.GetScore()).ToString();

        Jacket.sprite = musicData.jacket;

        SetClearStatus();

        //Ç±Ç±Ç≈ÇªÇÃã»ÇÃï€ë∂èÛãµÇì¸ÇÍÇÈ
        ScoreStatus.SetDessertScore(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, (int)InGameStatus.GetScore());

        ScoreStatus.SetDessertClearRanks(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore()));


        SaveData.SaveFoundation();
    }

    private void FixedUpdate()
    {
    }

    private void SetRank()
    {
        publicEnum.ClearRank rank =InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore());
        int RankIndex = 0;
        Plus.SetActive(false);
        PlusObject.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            circleRank[i].gameObject.SetActive(false);
            Rank[i].gameObject.SetActive(false);
            RankObject[i].SetActive(false);

        }

        switch (rank)
        {
            case publicEnum.ClearRank.SPlus:
                RankIndex = 4;
                RankTextOutLine.text = RankText.text = "S";
                PlusObject.SetActive(true);
                Plus.SetActive(true);
                break;
            case publicEnum.ClearRank.S:
                RankTextOutLine.text = RankText.text = "S";
                RankIndex = 4;
                break;
            case publicEnum.ClearRank.A:
                RankTextOutLine.text = RankText.text = "A";
                RankIndex = 3;
                break;
            case publicEnum.ClearRank.B:
                RankTextOutLine.text = RankText.text = "B";
                RankIndex = 2;
                break;
            case publicEnum.ClearRank.C:
                RankTextOutLine.text = RankText.text = "C";
                RankIndex = 1;
                break;
            case publicEnum.ClearRank.D:
                RankTextOutLine.text = RankText.text = "D";
                RankIndex = 0;
                break;
        }
        circleRank[RankIndex].gameObject.SetActive(true);
        Rank[RankIndex].gameObject.SetActive(true);
        RankObject[RankIndex].SetActive(true);
    }

    private void SetClearStatus() 
    {
        publicEnum.ClearStates clearStates = InGameStatus.CheckEnd();
        FullConbo.gameObject.SetActive(false);
        ALLDC.gameObject.SetActive(false);

        switch (clearStates)
        {
            case publicEnum.ClearStates.FullCombo:
                ALLDC.gameObject.SetActive(true);

                break;
            case publicEnum.ClearStates.ALLDC:
                FullConbo.gameObject.SetActive(true);

                break;
        }



    }

    private void SetJudgment()
    {
        float percentage = (float)InGameStatus.GetJudgments(0, 0)/(float)InGameStatus.GetNotesCount();

        DC.transform.localPosition = Vector2.Lerp(DC.transform.localPosition, Vector2.zero, percentage);
         percentage = (float)InGameStatus.GetJudgments(1, 0)/(float)InGameStatus.GetNotesCount();

        FASTD.transform.localPosition = Vector2.Lerp(FASTD.transform.localPosition, Vector2.zero, percentage);
         percentage = (float)InGameStatus.GetJudgments(2, 0)/(float)InGameStatus.GetNotesCount();

        FASTY.transform.localPosition = Vector2.Lerp(FASTY.transform.localPosition, Vector2.zero, percentage);
         percentage = (float)InGameStatus.GetJudgments(1, 1)/(float)InGameStatus.GetNotesCount();

        LATED.transform.localPosition = Vector2.Lerp(LATED.transform.localPosition, Vector2.zero, percentage);
         percentage = (float)InGameStatus.GetJudgments(2, 1)/(float)InGameStatus.GetNotesCount();

        LATEY.transform.localPosition = Vector2.Lerp(LATEY.transform.localPosition, Vector2.zero, percentage);



        for (int i = 0; i < 4; i++)
        {

            int Add = InGameStatus.GetNoesTypeSuccess(i) + InGameStatus.GetNoesTypeMIss(i);
            int sbu = InGameStatus.GetNoesTypeSuccess(i) - InGameStatus.GetNoesTypeMIss(i);

            float rete = (float)InGameStatus.GetNoesTypeSuccess(i) / (float)Add;

            if (sbu == 0) rete = 0.5f;

            Success[i].transform.localPosition = Vector2.Lerp(Success[i].transform.localPosition, new Vector2(0,-7.5f), Mathf.Abs(rete));
            Miss[i].transform.localPosition = Vector2.Lerp(Miss[i].transform.localPosition, new Vector2(0, -7.5f), Mathf.Abs(  rete-1));

        }

        int fast=0;
        int late=0;

        for(int i = 1; i < 3; i++) 
        {
            fast += InGameStatus.GetJudgments(i, 0);
            late += InGameStatus.GetJudgments(i, 1);

        }

        FASTALL.text += fast.ToString();
        LATEALL.text += late.ToString();


    }


    int ImageMoveCount = 0;
    bool moveFlag = true;

    Vector3 TargetPos = Vector3.zero;
    Vector3 StartPos = Vector3.zero;
    Vector3 TargetSize = new Vector3(1400, 1400, 0);
    Vector3 StartSize = new Vector3(1400, 1400, 0);




}

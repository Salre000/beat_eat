using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI MusicName;
    [SerializeField] TextMeshProUGUI DifficultyName;
    [SerializeField] TextMeshProUGUI DifficultyNameJukcet;
    [SerializeField] Image JukcetColor;
    [SerializeField]
    Color[] Diffcolor = new Color[5];
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
    [SerializeField] Image BackJacket;

    [SerializeField] GameObject rankObject;

    [SerializeField] RectTransform[] ConboAnime = new RectTransform[2];
    // Start is called before the first frame update
    void Start()
    {
        //AchievementStatus.Achievement(AchievementTypeEnum.AchievementType._NewEed);

        MusicData musicData = Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic];
        //楽曲の名前を入れる
        MusicName.text = musicData.name;

        DifficultyName.text = ScoreStatus.nowDifficulty.GetName();
        DifficultyNameJukcet.text = ScoreStatus.nowDifficulty.ToString();
        JukcetColor.color = Diffcolor[(int)ScoreStatus.nowDifficulty];
        SetRank();
        SetJudgment();

        ComboText.text = InGameStatus.GetMAXCombo().ToString();

        ScoreText.text = ((int)InGameStatus.GetScore()).ToString();

        Jacket.sprite = musicData.jacket;
        BackJacket.sprite = musicData.jacket;

        SetClearStatus();

        //ここでその曲の保存状況を入れる
        ScoreStatus.SetDessertScore(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, (int)InGameStatus.GetScore());

        ScoreStatus.SetDessertClearRanks(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore()));

        SwitchDifficulty();
        SaveData.SaveFoundation();
    }

    private void SwitchDifficulty() 
    {
        publicEnum.Difficulty difficulty = ScoreStatus.nowDifficulty;
        switch (difficulty)
        {
            case publicEnum.Difficulty.Drink:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._drink);
                break;
            case publicEnum.Difficulty.Hors_d_oeuvre:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._HorsDoeuvre);
                break;
            case publicEnum.Difficulty.Soup:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._Soup);
                break;
            case publicEnum.Difficulty.MainDish:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._mainDish);
                break;
            case publicEnum.Difficulty.dessert:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._drink);
                break;
            case publicEnum.Difficulty.MAX:
                AchievementStatus.AchieventSystemSet(AchievementTypeEnum.AchievementType._drink);

                break;
        }


    }
    private void FixedUpdate()
    {
        ClearStatusAnime();
        RotetoRank();
        CheckAchievement();
    }

    private void SetRank()
    {
        publicEnum.ClearRank rank = InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore());
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
        RankObject[RankIndex].transform.parent = rankObject.transform;
        RankObject[RankIndex].transform.localPosition = Vector3.zero;

    }

    private float rotetoSpeed = 1;
    private float roteto = 0;
    private void RotetoRank()
    {
        rankObject.transform.Rotate(0, rotetoSpeed, 0);
        roteto += rotetoSpeed;
        if (roteto < 120) return;
        rotetoSpeed = 4;

        if (roteto < 360) return;
        rotetoSpeed = 1;
        roteto = 0;


    }

    private void SetClearStatus()
    {
        publicEnum.ClearStates clearStates = InGameStatus.CheckEnd();
        Debug.Log(clearStates + "FF");
        FullConbo.gameObject.SetActive(false);
        ALLDC.gameObject.SetActive(false);

        switch (clearStates)
        {
            case publicEnum.ClearStates.ALLDC:
                ALLDC.gameObject.SetActive(true);

                break;
            case publicEnum.ClearStates.FullCombo:
                FullConbo.gameObject.SetActive(true);

                break;
        }



        ScoreStatus.SetDessertDifficulty(ScoreStatus.nowMusic, ScoreStatus.nowDifficulty, clearStates);

    }
    float clearStatusAnimeCount = 0;
    float clearStatusAnimeSpeed = 10;
    private void ClearStatusAnime()
    {
        clearStatusAnimeCount++;

        if (clearStatusAnimeCount < 200) return;


        for (int i = 0; i < 2; i++) ConboAnime[i].transform.position += new Vector3(clearStatusAnimeSpeed, -clearStatusAnimeSpeed);


        if (clearStatusAnimeCount < 300) return;

        clearStatusAnimeCount = Random.Range(-200, -100);


        for (int i = 0; i < 2; i++) ConboAnime[i].transform.localPosition = new Vector3(1, -1);


    }

    private void SetJudgment()
    {
        float percentage = (float)InGameStatus.GetJudgments(0, 0) / (float)InGameStatus.GetNotesCount();

        DC.transform.localPosition = Vector2.Lerp(DC.transform.localPosition, Vector2.zero, percentage);
        percentage = (float)InGameStatus.GetJudgments(1, 0) / (float)InGameStatus.GetNotesCount();

        FASTD.transform.localPosition = Vector2.Lerp(FASTD.transform.localPosition, Vector2.zero, percentage);
        percentage = (float)InGameStatus.GetJudgments(2, 0) / (float)InGameStatus.GetNotesCount();

        FASTY.transform.localPosition = Vector2.Lerp(FASTY.transform.localPosition, Vector2.zero, percentage);
        percentage = (float)InGameStatus.GetJudgments(1, 1) / (float)InGameStatus.GetNotesCount();

        LATED.transform.localPosition = Vector2.Lerp(LATED.transform.localPosition, Vector2.zero, percentage);
        percentage = (float)InGameStatus.GetJudgments(2, 1) / (float)InGameStatus.GetNotesCount();

        LATEY.transform.localPosition = Vector2.Lerp(LATEY.transform.localPosition, Vector2.zero, percentage);



        for (int i = 0; i < 4; i++)
        {

            int Add = InGameStatus.GetNoesTypeSuccess(i) + InGameStatus.GetNoesTypeMIss(i);
            int sbu = InGameStatus.GetNoesTypeSuccess(i) - InGameStatus.GetNoesTypeMIss(i);

            float rete = (float)InGameStatus.GetNoesTypeSuccess(i) / (float)Add;

            if (sbu == 0) rete = 0.5f;

            Success[i].transform.localPosition = Vector2.Lerp(Success[i].transform.localPosition, new Vector2(0, -7.5f), Mathf.Abs(rete));
            Miss[i].transform.localPosition = Vector2.Lerp(Miss[i].transform.localPosition, new Vector2(0, -7.5f), Mathf.Abs(rete - 1));

        }

        int fast = 0;
        int late = 0;

        for (int i = 1; i < 3; i++)
        {
            fast += InGameStatus.GetJudgments(i, 0);
            late += InGameStatus.GetJudgments(i, 1);

        }

        FASTALL.text += fast.ToString();
        LATEALL.text += late.ToString();


    }

    public void SelectSceneChenge()
    {
        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);

    }

    //ゲーム中に達成したアチーブメントを見せる
    private void CheckAchievement()
    {
        if (AchievementStatus.GetAchievementNumber().Count == 0) return;
        Debug.Log("カウントが０じゃない");
        if (AchievementStatus.achievementNumber != -1) return;
        Debug.Log("アチーブメントのナンバーがゼロじゃない");

        AchievementStatus.achievementNumber = AchievementStatus.GetAchievementNumber()[0];
        Debug.Log("描画する番号"+ AchievementStatus.GetAchievementNumber()[0]);
        AchievementStatus.GetAchievementNumber().RemoveAt(0);

        SceneManager.LoadScene("AchievementScene", LoadSceneMode.Additive);



    }


}

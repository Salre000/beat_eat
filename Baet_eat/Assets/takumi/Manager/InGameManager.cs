using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField] Material tapareaMaterial;
    [SerializeField] Material tapareaMaterial2;
    [SerializeField] Material lineMaterial;
    [SerializeField] Material flashMaterial;
    [SerializeField] Image ScoreGage;
    [SerializeField] TextMeshProUGUI scoreUI;

    [SerializeField] Material invisible;
    [SerializeField] Material NotInvisible;
    [SerializeField] Image jacket;
    public Material GetNoeInvisible() { return NotInvisible; }
    public Material Getinvisible() { return invisible; }

    [SerializeField] GameObject[] Ranks;
    private GameObject Plus;

    //現在タップ可能なノーツの配列
    [SerializeField] List<NotesBase> activeObject = new List<NotesBase>();

    //タップをする位置を生成するクラス
    private CreateTapArea _tapArea;

    //レーン光らせるクラス
    private CreateLineFlash _lineFlash;

    //レーンを区切る線を生成するクラス
    private Createline _line;

    [SerializeField] GameObject areaObject;

    [SerializeField, Header("ラインの分割数")] int _divisionCount = 12+2;
    [SerializeField, Header("コンボを描画するキャンバス")] GameObject comboObject;

    //コンボの描画するテキスト
    private TextMeshProUGUI comboText;
    private ComboAnime comboAnime;

    int LongLongNotesCount = 0;
    public void AddCount() { LongLongNotesCount++; }

    private bool preStart = false;

    private void Awake()
    {
        SkillManager.instance.aoto.Execute();

        LineUtility.gameManager = this;

        InGameStatus inGame = new InGameStatus();

        _tapArea = new CreateTapArea();
        _lineFlash = new CreateLineFlash();
        _line = new Createline();

        _tapArea.SetMaterial(tapareaMaterial, tapareaMaterial2);
        _tapArea.CreateMesh(_divisionCount);
        _lineFlash.SetMaterial(flashMaterial);
        _lineFlash.SetFlashLine(_divisionCount);
        _line.SetLine(_divisionCount, areaObject, lineMaterial);
        Plus = Ranks[0].transform.GetChild(1).gameObject;

        RankNotShow();
        Ranks[4].SetActive(true);

        jacket.sprite = Resources.Load<MusicDataBase>("MusicDataBase").musicData[ScoreStatus.nowMusic].jacket;
        comboText = comboObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        comboAnime=comboText.GetComponent<ComboAnime>();

    }

    private void FixedUpdate()
    {
        CheckCombo();

        if (!preStart)
        {
            preStart = true; InGameStatus.SetUpScore(GetComponent<CreateNotes>().GetCount() + LongLongNotesCount);
        }
        if (Input.GetKey(KeyCode.T)) SoundUtility.MainBGMStop();
        if (Input.GetKey(KeyCode.R)) SoundUtility.MainBGMStart();

        for (int i = 0; i < 14; i++) { tapFlag.Add(false); }

        _lineFlash.SbuAlpha();
        _tapArea.CheckTime();

    }

    List<bool> tapFlag = new List<bool>(14);
    public void Click(int index, int id)
    {
        if (tapFlag.Count <= id || id < 0) return;

        if (tapFlag[id]) return;

        _lineFlash.AddAlpha(index-1);

        for (int i = 0; i < activeObject.Count; i++)
        {
            NotesBase notes = activeObject[i];

            if (!notes.gameObject.activeSelf) continue;

            notes.SetTouchID(id);

            if (!notes.CheckHitlane(index)) continue;

            notes.Hit();
            HandUtility.AddEndAction(() => { tapFlag[id] = false; }, id);
            tapFlag[id] = true;

            return;


        }

    }

    public float RangeToDecision(Vector3 position,float endPos) { return endPos - position.z + (OptionStatus.GetNotesHitLinePos() * 0.5f); }


    public void AddActiveObject(NotesBase gameObject) { activeObject.Add(gameObject); }
    public void SbuActiveObject(NotesBase gameObject) { activeObject.Remove(gameObject); }

    public List<NotesBase> GetActiveObject() { return activeObject; }

    public CreateTapArea GetTapArea() { return _tapArea; }

    public void ShowText(string text) { }

    private void RankNotShow()
    {
        Plus.SetActive(false);
        for (int i = 0; i < Ranks.Length; i++) { Ranks[i].SetActive(false); }
    }

    public void SetScore()
    {
        RankNotShow();
        scoreUI.text = ((int)InGameStatus.GetScore()).ToSafeString();
        float scoreRate = InGameStatus.GetScoreRate();

        publicEnum.ClearRank clearRank = InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore());

        float offset = 0;

        switch (clearRank)
        {
            case publicEnum.ClearRank.SPlus:
                Ranks[0].SetActive(true);
                Plus.SetActive(true);
                offset = 0.9f;
                break;
            case publicEnum.ClearRank.S:
                Ranks[0].SetActive(true);
                offset = 0.775f;
                break;
            case publicEnum.ClearRank.A:
                Ranks[1].SetActive(true);
                offset = 0.65f;
                break;
            case publicEnum.ClearRank.B:
                Ranks[2].SetActive(true);

                offset = 0.525f;
                break;
            case publicEnum.ClearRank.C:
                Ranks[3].SetActive(true);

                offset = 0.4f;
                break;
            case publicEnum.ClearRank.D:
                Ranks[4].SetActive(true);

                break;
        }

        float ScoreReteOffset = 0;

        if (clearRank == publicEnum.ClearRank.SPlus)
        {
            ScoreReteOffset = (InGameStatus.GetScore() % scoreRate) / scoreRate;
            ScoreReteOffset /= 20f;

            if (InGameStatus.GetScore() >= InGameStatus.GetMAXScore() - scoreRate)
            {
                offset += 0.05f;
                if (ScoreReteOffset == 0) ScoreReteOffset = 0.05f;
            }

        }
        else if (clearRank != publicEnum.ClearRank.D)
        {

            ScoreReteOffset = (InGameStatus.GetScore() % scoreRate) / scoreRate;
            ScoreReteOffset /= 8f;


        }
        else
        {

            ScoreReteOffset = (InGameStatus.GetScore()) / scoreRate;
            ScoreReteOffset /= 5f;

        }


        ScoreGage.fillAmount = ScoreReteOffset + offset;
    }

    private void CheckCombo()
    {

        if (InGameStatus.GetCombo() < 10)
        {
            if (comboObject.activeSelf) comboObject.SetActive(false);

            return;
        }
        else
        {
            if (!comboObject.activeSelf) comboObject.SetActive(true);

            if (comboText.text == InGameStatus.GetCombo().ToString()) return;
            comboText.text = InGameStatus.GetCombo().ToString();
            comboAnime.StartAnime();

            return;

        }



    }
}

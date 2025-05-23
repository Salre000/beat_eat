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
    [SerializeField] Material flashMaterial;
    [SerializeField] Image ScoreGage;
    [SerializeField] TextMeshProUGUI scoreUI;

    [SerializeField] GameObject[] Ranks;

    //現在タップ可能なノーツの配列
    [SerializeField] List<NotesBase> activeObject=new List<NotesBase>();

    //タップをする位置を生成するクラス
    private CreateTapArea _tapArea;

    //レーン光らせるクラス
    private CreateLineFlash _lineFlash;

    //レーンを区切る線を生成するクラス
    private Createline _line;

    [SerializeField] GameObject areaObject;

    [SerializeField, Header("ラインの分割数")] int _divisionCount = 12;

    int LongLongNotesCount = 0;
    public void AddCount() {  LongLongNotesCount++; }

    private bool preStart = false;  

    private void Awake()
    {

        LineUtility.gameManager = this;

        InGameStatus inGame =new InGameStatus();

        _tapArea = new CreateTapArea();
        _lineFlash = new CreateLineFlash();
        _line = new Createline();

        _tapArea.SetMaterial(tapareaMaterial, tapareaMaterial2);
        _tapArea.CreateMesh(_divisionCount);
        _lineFlash.SetMaterial(flashMaterial);
        _lineFlash.SetFlashLine(_divisionCount);
        _line.SetLine(_divisionCount,areaObject,tapareaMaterial);

    }

    private void FixedUpdate()
    {

        if (!preStart) { preStart = true;InGameStatus.SetUpScore(GetComponent<CreateNotes>().GetCount()+ LongLongNotesCount);
        }
        if (Input.GetKey(KeyCode.T)) SoundUtility.MainBGMStop();
        if (Input.GetKey(KeyCode.R)) SoundUtility.MainBGMStart();

        _lineFlash.SbuAlpha();
        _tapArea.CheckTime();

    }


    public void Click(int index,int id) 
    {
        _lineFlash.AddAlpha(index);

        for(int i=0;i< activeObject.Count; i++) 
        {
            NotesBase notes = activeObject[i];

            if (!notes.gameObject.activeSelf) continue;

            notes.SetTouchID(id);

            if (!notes.CheckHitlane(9-index)) continue;

            notes.Hit();

            return;


        }

    }

    public float RangeToDecision(Vector3 position) { return -6.25f-position.z; }


    public void AddActiveObject(NotesBase gameObject) { activeObject.Add(gameObject); }
    public void SbuActiveObject(NotesBase gameObject) { activeObject.Remove(gameObject); }

    public CreateTapArea GetTapArea() { return _tapArea; }

    public void ShowText(string text) {}

    public void SetScore() 
    {

        scoreUI.text = ((int)InGameStatus.GetScore()).ToSafeString();
        float scoreRate=InGameStatus.GetScoreRate();

        publicEnum.ClearRank clearRank = InGameStatus.GetScoreClearRank((int)InGameStatus.GetScore());

        float offset = 0;

        switch (clearRank)
        {
            case publicEnum.ClearRank.SPlus:
                offset = 0.9f;
                break;
            case publicEnum.ClearRank.S:
                offset = 0.775f;
                break;
            case publicEnum.ClearRank.A:
                offset = 0.65f;
                break;
            case publicEnum.ClearRank.B:
                offset = 0.525f;
                break;
            case publicEnum.ClearRank.C:
                offset = 0.4f;
                break;
            case publicEnum.ClearRank.D:
                break;
        }

        float ScoreReteOffset = 0;

        if(clearRank == publicEnum.ClearRank.SPlus) 
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
            ScoreReteOffset /=8f;


        }
        else
        {

            ScoreReteOffset = (InGameStatus.GetScore()) / scoreRate;
            ScoreReteOffset /= 4f;

        }


        ScoreGage.fillAmount = ScoreReteOffset + offset;
    }
}

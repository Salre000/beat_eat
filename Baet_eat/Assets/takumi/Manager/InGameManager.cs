using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class InGameManager : MonoBehaviour
{
    [SerializeField] Material tapareaMaterial;
    [SerializeField] Material tapareaMaterial2;
    [SerializeField] Material flashMaterial;

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

    ////デバッグ用
    public TextMeshProUGUI _DC;
    public TextMeshProUGUI _D;
    public TextMeshProUGUI _Y;
    public TextMeshProUGUI _G;
    public TextMeshProUGUI _M;
    public TextMeshProUGUI _S;
    public TextMeshProUGUI _MOZI;

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
        if (Input.GetKey(KeyCode.T)) SoundUtility.MainBGMStop();
        if (Input.GetKey(KeyCode.R)) SoundUtility.MainBGMStart();

        _lineFlash.SbuAlpha();
        _tapArea.CheckTime();

        //デバッグ用
        _DC.text = "DC" + (InGameStatus.GetJudgments(0, 0) + InGameStatus.GetJudgments(0, 1));
        _D.text = "D" + (InGameStatus.GetJudgments(1, 0) + InGameStatus.GetJudgments(1, 1));
        _Y.text = "Y" + (InGameStatus.GetJudgments(2, 0) + InGameStatus.GetJudgments(2, 1));
        _G.text = "G" + (InGameStatus.GetJudgments(3, 0) + InGameStatus.GetJudgments(3, 1));
        _M.text = "M" + (InGameStatus.GetJudgments(4, 0) + InGameStatus.GetJudgments(4, 1));
        _S.text = "Score" + InGameStatus.GetScore();
    }


    public void Click(int index,int id) 
    {
        _lineFlash.AddAlpha(index);

        for(int i=0;i< activeObject.Count; i++) 
        {
            NotesBase notes = activeObject[i];

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

    public void ShowText(string text) { _MOZI.text = text; }
}

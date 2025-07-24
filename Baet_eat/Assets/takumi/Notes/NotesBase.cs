using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static CreateTapArea;

public class NotesBase : MonoBehaviour
{
    [SerializeField] protected List<int> laneIndex = new List<int>();
    public void AddLaneIndex(int index) { laneIndex.Add(index); }
    public void SetLaneIndex(List<int> lane) { laneIndex = lane; }

    protected const float BaseSpeed = 20;//20

    protected int touchID = -1;

    protected float showTime = -1;

    protected int renge = 0;
    public void SetRemge(int Renge) { renge = Renge; }
    public int GetRemge() { return renge; }
    public void SetShowTime(float time) { showTime = time; }
    public float GetShowTime() { return showTime; }

    private float SpeedTime() { return 50.0f / (OptionStatus.GetNotesSpeed() * BaseSpeed); }

    public void SetTouchID(int ID) { if (touchID != -1) return; touchID = ID; }
    public void ResetTouchID() { touchID = -1; }

    protected Vector3 Vec = new Vector3(0, 0, (BaseSpeed * OptionStatus.GetNotesSpeed()) / 50.0f);

    protected float endPos = -6.25f;
    public float GetEndPos() { return endPos; } 
    public void SetEndPos(float _endPOs) { endPos = _endPOs; }

    protected virtual void MissAction() { }
    //判定タイプ
    public enum JudgmentType
    {
        DC,
        Delicious,
        Yammy,
        Good,
        Miss,
        None

    }

    public void Awake()
    {






    }
    //ノーツに触れたときに動く関数
    public virtual void Hit()
    {
        JudgmentImageUtility.SetNowJudgmentObjectPos(transform.position);
        //判定の加算をする関数
        SetJudgment(this.gameObject);

        SoundUtility.NotesNormalHitSoundPlay();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);

        showTime = -100;
        //自分を見えなくする
        this.gameObject.SetActive(false);

        InGameStatus.AddNoesTypeSuccess(NotesType);
    }

    public virtual void Hit(bool flag)
    {
        JudgmentImageUtility.SetNowJudgmentObjectPos(transform.position);
        //判定の加算をする関数
        SetJudgment(this.gameObject);

        SoundUtility.NotesLongHitSoundPlay();

        showTime = -100;

        InGameStatus.AddNoesTypeSuccess(NotesType);

    }

    protected int NotesType = 0;
    public void Update()
    {
        //this.transform.position -= Vec;

        Action();

        if (InGameStatus.GetAuto())
        {
            touchID = 0;
            if (this.transform.position.z <= GetDestryDecision() + (int)JudgmentType.Miss) Hit();
        }

        if (this.transform.position.z > GetDestryDecision()) return;

        InGameStatus.HPDamege();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);

        //自分を見えなくする
        this.gameObject.SetActive(false);

        showTime = -100;
        InGameStatus.AddNoesTypeMIss(NotesType);
        MissAction();

    }

    protected virtual double GetDestryDecision() { return -6.25 + -(int)JudgmentType.Miss; }

    protected virtual void Action() { }

    protected void SetJudgment(GameObject gameObject)
    {
        int renge = (int)LineUtility.RangeToDecision(gameObject.transform.position, endPos);
        renge = Mathf.Abs(renge);
        float rete = 1;

        if (renge >= (int)JudgmentType.Miss) renge = (int)JudgmentType.Miss;

        if (((int)LineUtility.RangeToDecision(gameObject.transform.position, endPos)>=0)) InGameStatus.SetJudgments(renge, 0);
        else InGameStatus.SetJudgments(renge, 1);
        //renge = (int)SkillManager.instance.criticalJudgmentExpands.ExecuteSetJudgment(renge);



        switch ((JudgmentType)renge)
        {
            case JudgmentType.DC:
                break;
            case JudgmentType.Delicious:
                rete = 0.8f;
                break;
            case JudgmentType.Yammy:
                rete = 0.5f;
                break;
            case JudgmentType.Good:
                rete = 0.2f;
                break;
            case JudgmentType.Miss:
                rete = 0;
                InGameStatus.HPDamege();
                break;
        }



        InGameStatus.AddScore(rete);
    }




    public virtual bool CheckHitlane(int index)
    {
        JudgmentType judgmentType = (JudgmentType)((int)LineUtility.RangeToDecision(this.transform.position, endPos));

        bool flag = laneIndex.Exists(number => number == index) && judgmentType <= JudgmentType.Good && (int)judgmentType >= -(int)JudgmentType.Good;
        return flag;
    }
    public virtual void SetMaterial(NotesMaterial material) 
    {

        GetComponent<MeshRenderer>().material = material.normal;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesBase : MonoBehaviour
{
    protected List<int> laneIndex = new List<int>();
    public void AddLaneIndex(int index) { laneIndex.Add(index); }
    public void SetLaneIndex(List<int> lane) { laneIndex = lane; }

    protected const float BaseSpeed = 20;//20

    protected int touchID = -1;

    protected float showTime = -1;
    protected int renge = 0;
    public void SetRemge(int Renge) {  renge = Renge; }
    public int GetRemge() { return renge; } 
    public void SetShowTime(float time) {  showTime = time; }
    public float GetShowTime() { return showTime- SpeedTime()-0.3f; }
    
    private float SpeedTime() {return 50.0f/( InGameStatus.GetSpeed()* BaseSpeed); }

    public void SetTouchID(int ID) { if (touchID != -1) return; touchID = ID; }
    public void ResetTouchID() { touchID = -1; }

    protected Vector3 Vec = new Vector3(0, 0, (BaseSpeed * InGameStatus.GetSpeed()) / 50.0f);

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

    public void OnEnable()
    {
        LineUtility.AddActiveObject(this);

    }
    //ノーツに触れたときに動く関数
    public void Hit(GameObject gameObject)
    {
        //判定の加算をする関数
        SetJudgment(gameObject);

        SoundUtility.NotesHitSoundPlay();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);

        //自分を見えなくする
        this.gameObject.SetActive(false);

        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        showTime = -1;
    }
    public virtual void Hit()
    {
        //判定の加算をする関数
        SetJudgment(this.gameObject);

        SoundUtility.NotesHitSoundPlay();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        //自分を見えなくする
        this.gameObject.SetActive(false);

        showTime = -1;

    }
    public virtual void Hit(bool flag) 
    {
        //判定の加算をする関数
        SetJudgment(this.gameObject);

        SoundUtility.NotesHitSoundPlay();

        showTime = -1;

        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

    }

    public void FixedUpdate()
    {
        //this.transform.position -= Vec;

        Action();

        if (this.transform.position.z > GetDestryDecision()) return;

        InGameStatus.HPDamege();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);

        //自分を見えなくする
        this.gameObject.SetActive(false);

        showTime = -1;

    }

    protected virtual double GetDestryDecision() { return -6.25 + -(int)JudgmentType.Miss; }

    protected virtual void Action() { }

    private void SetJudgment(GameObject gameObject)
    {
        int renge = (int)LineUtility.RangeToDecision(gameObject.transform.position);
        renge = Mathf.Abs(renge);
        float rete = 1;

        if (renge >= (int)JudgmentType.Miss) renge = (int)JudgmentType.Miss;

        //renge = (int)SkillManager.instance.criticalJudgmentExpands.ExecuteSetJudgment(renge);

        if (renge >= 0) InGameStatus.SetJudgments(renge, 0);
        else InGameStatus.SetJudgments(renge, 1);


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
        JudgmentType judgmentType = (JudgmentType)(int)LineUtility.RangeToDecision(this.transform.position);

        bool flag = laneIndex.Exists(number => number == index) && judgmentType <= JudgmentType.Miss && (int)judgmentType >= -(int)JudgmentType.Miss;
        return flag;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LongLongNotes : NotesBase
{
    CreateTapArea.BoxArea boxArea;
    public void SetBoxArea(CreateTapArea.BoxArea boxArea) { this.boxArea = boxArea; }
    private readonly float epsilon = 0.3f;

    private System.Action<int> SetTouchIDS;
    public void Set_SetTouchID(System.Action<int> action) { SetTouchIDS = action; }

    bool DamegeFlag = false;
    //必要
    public void FixedUpdate()
    {

        if (this.transform.position.z < GetDestryDecision())
        {
            if (!DamegeFlag)
            {
                DamegeFlag = true;
                InGameStatus.HPDamege();

            }           
        }


        int renge = (int)LineUtility.RangeToDecision(gameObject.transform.position);
        renge = Mathf.Abs(renge);

        if (renge >= 1) return;


        int ID = -1;

        List<HandManager.Hands> hands = HandUtility.GetHands();

        float left = Mathf.Min(boxArea.bottomLeft.x, boxArea.leftTop.x);
        float right = Mathf.Min(boxArea.bottomRight.x, boxArea.rightTop.x);

        float z = transform.position.z;

        //一時的に座標をゼロに合わせる
        transform.position -= new Vector3(0, 0, z);
        Vector2 leftpos = (Vector2)Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(left, 0, 0));
        Vector2 rightpos = (Vector2)Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(right, 0, 0));
        this.transform.position += new Vector3(0, 0, z);

        for (int i = 0; i < hands.Count; i++)
        {

            if (!hands[i].flag) continue;
            if (Mathf.Min(leftpos.x, rightpos.x) - 150 < hands[i].HandPosition.x && Mathf.Max(leftpos.x, rightpos.x) + 150 > hands[i].HandPosition.x) ID = i;

        }

        if (ID < 0) return;

        if (touchID == ID)
        {
            Hit();
        }
        else
        {
            SetTouchIDS(ID);
            Hit();
        }
    }

    public override void Hit()
    {
        //判定の加算をする関数
        InGameStatus.AddScore(1);

        //パーフェクトな判定
        InGameStatus.SetJudgments(0, 0);

        SoundUtility.NotesHitSoundPlay();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);

        //自分を見えなくする
        this.gameObject.SetActive(false);

        showTime = -100;


    }





    public override bool CheckHitlane(int index)
    {

        return false;


    }




}

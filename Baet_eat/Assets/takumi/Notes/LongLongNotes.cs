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

    
    public void Update()
    {
        if (this.transform.position.z < -epsilon || this.transform.position.z > epsilon) return;



        int ID = -1;

        List<HandManager.Hands> hands = HandUtility.GetHands();

        float left = Mathf.Min(boxArea.bottomLeft.x, boxArea.leftTop.x);
        float right = Mathf.Min(boxArea.bottomRight.x, boxArea.rightTop.x);


        Vector2 leftpos = (Vector2)Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(left, 0, 0));
        Vector2 rightpos = (Vector2)Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(right, 0, 0));
        Debug.Log(leftpos.x+":"+ rightpos.x);

        for (int i = 0; i < hands.Count; i++)
        {
            
            if (!hands[i].flag) continue;
            if (Mathf.Min(leftpos.x,rightpos.x)-150 < hands[i].HandPosition.x && Mathf.Max(leftpos.x, rightpos.x)+150 > hands[i].HandPosition.x)ID = i;
             

            Debug.Log("DDD" + (int)leftpos.x + ":" + rightpos.x + ":" + hands[i].HandPosition.x);

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

        showTime = -1;


    }





    public override bool CheckHitlane(int index)
    {

        return false;


    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LongLongNotes : NotesBase
{
    [SerializeField]CreateTapArea.BoxArea boxArea;
    public void SetBoxArea(CreateTapArea.BoxArea boxArea) { this.boxArea = boxArea; }
    private readonly float epsilon = 0.3f;

    public float XXX;
    public float YYY;
    private System.Action<int> SetTouchIDS;
    public void Set_SetTouchID(System.Action<int> action) { SetTouchIDS = action; }

    private System.Action<Vector3> SetNextPos;
    public void SetSetNextPos(System.Action<Vector3> action) { SetNextPos = action; }

    private System.Action endAction;
    public void SetEndAction(System.Action _endAction) { endAction = _endAction; }

    bool DamegeFlag = false;
    private MeshRenderer mesh;
    Mesh meshLong;
    public void Start()
    {
        meshLong = GetComponent<MeshFilter>().mesh;

        XXX = boxArea.leftTop.x;
        YYY = boxArea.rightTop.x;
    }
    //必要
    public void FixedUpdate()
    {
        XXX = boxArea.leftTop.x;
        YYY = boxArea.rightTop.x;

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

        if (SetNextPos !=null) SetNextPos(this.transform.position);

        if (InGameStatus.GetAuto()) { Hit();return; }

        int ID = -1;

        List<HandManager.Hands> hands = HandUtility.GetHands();

        float z = transform.position.z;

        //一時的に座標をゼロに合わせる
        transform.position -= new Vector3(0, 0, z+6.25f);

        float MaxPos = 0;
        float MinPos = 1800;

        for(int i = 0; i < meshLong.vertices.Length; i++) 
        {
            float pos=Camera.main.WorldToScreenPoint(meshLong.vertices[i]+transform.position).x;


            MaxPos = Mathf.Max(MaxPos, pos);
            MinPos = Mathf.Min(MinPos, pos);



        }

        this.transform.position += new Vector3(0, 0, z+6.25f);

        for (int i = 0; i < hands.Count; i++)
        {
            if (!hands[i].flag) continue;
            Debug.Log(MaxPos+":"+MinPos+"SSS");
            if (MinPos < hands[i].HandPosition.x && MaxPos  > hands[i].HandPosition.x) ID = i;

        }

        if (ID < 0) return;

        if (touchID == ID)
        {
            Hit();
        }
        else
        {
            //SetTouchIDS(ID);
            Hit();
        }
    }

    public override void Hit()
    {
        if (DamegeFlag) return;
        NotesType = 3;

        //判定の加算をする関数
        InGameStatus.AddScore(1);

        //パーフェクトな判定
        InGameStatus.SetJudgments(0, 0);

        //SoundUtility.NotesHitSoundPlay();

        //自身をactiveじゃない状態に変更
        LineUtility.SbuActiveObject(this);
        InGameStatus.AddNoesTypeSuccess(NotesType);

        //自分を見えなくする
        //this.gameObject.SetActive(false);

        DamegeFlag = true;

        showTime = -100;
        mesh = this.GetComponent<MeshRenderer>();

        mesh.material = LineUtility.GetInbisible();

        if (endAction != null) endAction();
    }





    public override bool CheckHitlane(int index)
    {

        return false;


    }




}

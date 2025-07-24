using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickNotes : NotesBase
{
    GameObject FilickUp;

    MeshRenderer MeshRenderer;

    protected float startPos = 0;
    protected float alpha = 0.01f;
    protected float speed = 0.01f;

    protected bool count = false;

    public void Start()
    {
        FilickUp = this.transform.GetChild(0).gameObject;
        startPos = FilickUp.transform.position.y;
        MeshRenderer = FilickUp.GetComponent<MeshRenderer>();

        MeshRenderer.material = new Material(MeshRenderer.material);
        NotesType = 3;
    }

    protected override void Action()
    {
        base.Action();
        FlickImageMove();
        FlickDecision();
    }

    public override void Hit()
    {

        SoundUtility.NotesFlickHitSoundPlay();

        //���g��active����Ȃ���ԂɕύX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(transform.position);

        //����̉��Z������֐�
        SetJudgment(this.gameObject);
        showTime = -100;
        //�����������Ȃ�����
        this.gameObject.SetActive(false);

        InGameStatus.AddNoesTypeSuccess(NotesType);

    }
    virtual public void FlickImageMove()
    {
        Vector3 pos = FilickUp.transform.localPosition;

        pos.y += speed;

        FilickUp.transform.localPosition = pos;

        Color color = MeshRenderer.material.color;

        color.a += alpha;

        MeshRenderer.material.color = color;

        if (color.a >= 1)
        {
            alpha = -0.05f;
        }
        else if (color.a <= 0)
        {

            alpha = 0.05f;

            pos = FilickUp.transform.localPosition;

            pos.y = startPos;

            FilickUp.transform.localPosition = pos;
        }
    }

    protected Vector2 flickStartPos = Vector2.zero;

    readonly float renge = 1;

   virtual public  void FlickDecision()
    {

        if (!count) return;

        if (Vector2.Distance(flickStartPos, HandUtility.handPosition(touchID)) < renge) return;

        Hit();

    }

    public override bool CheckHitlane(int index)
    {

        if (base.CheckHitlane(index))
        {
            if (!count)
            {
                //HandUtility.handPosition(touchID);
                flickStartPos = HandUtility.handPosition(touchID); //Input.GetTouch(touchID).position;

            }
            count = true;
        }


        return false;
    }
    public override void SetMaterial(NotesMaterial material)
    {
        GetComponent<MeshRenderer>().material = material.flick;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = material.flickup;


    }
}

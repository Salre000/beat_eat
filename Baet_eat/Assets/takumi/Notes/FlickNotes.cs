using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickNotes : NotesBase
{
    GameObject FilickUp;

    MeshRenderer MeshRenderer;

    private float startPos = 0;
    private float alpha = 0.01f;
    private float speed = 0.01f;

    private bool count = false;
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
        //”»’è‚Ì‰ÁŽZ‚ð‚·‚éŠÖ”
        SetJudgment(this.gameObject);

        SoundUtility.NotesFlickHitSoundPlay();

        //Ž©g‚ðactive‚¶‚á‚È‚¢ó‘Ô‚É•ÏX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        showTime = -100;
        //Ž©•ª‚ðŒ©‚¦‚È‚­‚·‚é
        this.gameObject.SetActive(false);

        InGameStatus.AddNoesTypeSuccess(NotesType);

    }
    private void FlickImageMove()
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

    Vector2 flickStartPos = Vector2.zero;

    readonly float renge = 1;

    private void FlickDecision()
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNotes : NotesBase
{
    public void Start()
    {
        
        NotesType = 1;
    }

    public override void Hit()
    {
        InGameStatus.SetJudgments(0, 0);
        InGameStatus.AddScore(1);

        SoundUtility.NotesSkillHitSoundPlay();

        //©g‚ğactive‚¶‚á‚È‚¢ó‘Ô‚É•ÏX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        InGameStatus.AddNoesTypeSuccess(NotesType);
        //©•ª‚ğŒ©‚¦‚È‚­‚·‚é
        this.gameObject.SetActive(false);

        showTime = -100;

    }
}

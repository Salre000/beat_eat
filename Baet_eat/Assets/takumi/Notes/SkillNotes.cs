using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNotes : NotesBase
{
    public override void Hit()
    {
        NotesType = 1;
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

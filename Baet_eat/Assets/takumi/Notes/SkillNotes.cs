using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNotes : NotesBase
{
    public override void Hit()
    {
        InGameStatus.SetJudgments(0, 0);

        SoundUtility.NotesHitSoundPlay();

        //©g‚ğactive‚¶‚á‚È‚¢ó‘Ô‚É•ÏX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        //©•ª‚ğŒ©‚¦‚È‚­‚·‚é
        this.gameObject.SetActive(false);

        showTime = -1;

    }
}

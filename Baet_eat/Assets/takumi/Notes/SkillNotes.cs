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

        //���g��active����Ȃ���ԂɕύX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        InGameStatus.AddNoesTypeSuccess(NotesType);
        //�����������Ȃ�����
        this.gameObject.SetActive(false);

        showTime = -100;

    }
}

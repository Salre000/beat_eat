using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNotes : NotesBase
{
    public override void Hit()
    {
        InGameStatus.SetJudgments(0, 0);

        SoundUtility.NotesHitSoundPlay();

        //���g��active����Ȃ���ԂɕύX
        LineUtility.SbuActiveObject(this);
        JudgmentImageUtility.SetNowJudgmentObjectPos(touchID);

        //�����������Ȃ�����
        this.gameObject.SetActive(false);

        showTime = -1;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static SkillManager;

public class HeelHp : SkillBase
{
    // 500‰ñ•œ
    private const int _HEEL_VOLUME = 500;
    public override void Execute()
    {
        if (!isSkillActiveFlags[1]) return;
        InGameStatus.HPHeel(_HEEL_VOLUME);
    }

    public override void Initialize()
    {
        isSkillActiveFlags[1] = false;
        description = "HP‚ª300‚ð‰º‰ñ‚é‚Æ‰ñ•œ‚·‚é";
    }
    

}

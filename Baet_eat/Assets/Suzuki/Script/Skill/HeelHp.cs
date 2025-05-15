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
        if (!isSkillActiveFlagList[1]) return;
        InGameStatus.HPHeel(_HEEL_VOLUME);
    }

    public override void Initialize()
    {
        isSkillActiveFlagList[1] = false;
    }
    

}

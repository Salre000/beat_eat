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

        InGameStatus.HPHeel(_HEEL_VOLUME);
    }

    public override void Initialize()
    {
        SkillManager.isSkillActiveFlagList[1] = false;
    }
    

}

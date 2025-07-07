using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillManager;

public class Aoto : SkillBase
{
    // 500‰ñ•œ
    private const int _HEEL_VOLUME = 500;
    public override void Execute()
    {
        if (!isSkillActiveFlags[2]) return;
        InGameStatus.AutoMode();
    }

    public override void Initialize()
    {
        InGameStatus.AutoMode(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SkillManager.SkillList[1] = false;
    }
    

}

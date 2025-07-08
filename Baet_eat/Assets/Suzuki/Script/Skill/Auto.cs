using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillManager;

public class Auto : SkillBase
{
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

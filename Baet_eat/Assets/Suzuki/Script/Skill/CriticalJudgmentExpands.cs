using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NotesBase;
using static SkillManager;

public class CriticalJudgmentExpands : SkillBase
{
    // クリティカル判定増加

    public override void Initialize()
    {
        isSkillActiveFlags[0] = false;
        description = "判定が少しだけ優しくなる";

    }

    public JudgmentType ExecuteSetJudgment(float renge)
    {
        if (!isSkillActiveFlags[0]) return (JudgmentType)(int)renge;

        if (renge < 1.5f) return JudgmentType.DC;
        else if (renge < 1.5f + (0.875f * 1)) return JudgmentType.Delicious;
        else if (renge < 1.5f + (0.875f * 2)) return JudgmentType.Yammy;
        else if (renge < 1.5f + (0.875f * 3)) return JudgmentType.Good;
        else if (renge < 1.5f + (0.875f * 4)) return JudgmentType.Miss;
        return JudgmentType.Miss;
    }
}

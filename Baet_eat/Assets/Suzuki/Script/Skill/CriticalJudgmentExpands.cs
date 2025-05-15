using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static NotesBase;

public class CriticalJudgmentExpands : SkillBase
{
    public override void Initialize()
    {
        
    }
    public override void Execute()
    {
        
    }
    protected JudgmentType GetJudgment(float renge)
    {
        if (renge < 1.5f) return JudgmentType.DC;
        else if (renge < 1.5f + (0.875f * 1)) return JudgmentType.Delicious;
        else if (renge < 1.5f + (0.875f * 2)) return JudgmentType.Yammy;
        else if (renge < 1.5f + (0.875f * 3)) return JudgmentType.Good;
        else if (renge < 1.5f + (0.875f * 4)) return JudgmentType.Miss;
        return JudgmentType.Miss;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillManager
{
    private const int _SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// 0 = クリティカル判定3割
    /// 1 = 
    /// </summary>
    public static List<bool> SkillList = new(_SKILLLIST_CAPACITY);

    public static readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();

}

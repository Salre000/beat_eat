using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillManager
{
    private const int _SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// 0 = �N���e�B�J������3��
    /// 1 = �̗�250�ȉ���500��
    /// </summary>
    public static List<bool> isSkillActiveFlagList = new(_SKILLLIST_CAPACITY);

    public static readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();
    public static readonly HeelHp heelHp = new HeelHp();

    public static void Initialize()
    {
        criticalJudgmentExpands.Initialize();
        heelHp.Initialize();
    }
}

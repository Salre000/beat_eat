using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillManager
{
    public const int SKILLLIST_CAPACITY = 5;
    /// <summary>
    /// (0 = �N���e�B�J������3��) :
    /// (1 = �̗�250�ȉ���500��) :
    /// </summary>
    public static List<bool> isSkillActiveFlagList = new(SKILLLIST_CAPACITY);

    public static readonly CriticalJudgmentExpands criticalJudgmentExpands = new CriticalJudgmentExpands();
    public static readonly HeelHp heelHp = new HeelHp();

    public static void Initialize()
    {
        for(int i=0;i<SKILLLIST_CAPACITY;i++)
            isSkillActiveFlagList.Add(false);
        criticalJudgmentExpands.Initialize();
        heelHp.Initialize();
    }
}

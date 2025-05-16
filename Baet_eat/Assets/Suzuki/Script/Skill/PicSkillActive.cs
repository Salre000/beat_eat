using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSkillActive : MonoBehaviour
{
    // 選ばれたスキルをアクティブにする
    private int _selectPicSkillNumber = 0;
    // アクティブスキルを選ぶ
    private List<bool> _isSkillActives = new(SkillManager.SKILLLIST_CAPACITY);

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _selectPicSkillNumber=SkillManager.instance.GetSelectedSkillID();
        _isSkillActives = SkillManager.isSkillActiveFlags;
        TargetSkillCheck();
    }

    private void Update()
    {
        if (_selectPicSkillNumber != SkillManager.instance.GetSelectedSkillID())
        {
            _selectPicSkillNumber=SkillManager.instance.GetSelectedSkillID();
            TargetSkillCheck();
        }
    }

    // スキルを変更したら通る
    private void TargetSkillCheck()
    {
        for(int i=0;i<SkillManager.SKILLLIST_CAPACITY;i++)
        {
            if (i != _selectPicSkillNumber)
            {
                // 選ばれていないスキルなら非アクティブ
                SkillManager.instance.SetIsSkillActiveFlags(i);
            }
            else
            {
                // 選ばれているスキルをアクティブ
                SkillManager.instance.SetIsSkillActiveFlags(i,true);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicSkillActive : MonoBehaviour
{
    // �I�΂ꂽ�X�L�����A�N�e�B�u�ɂ���
    private int _selectPicSkillNumber = 0;
    // �A�N�e�B�u�X�L����I��
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

    // �X�L����ύX������ʂ�
    private void TargetSkillCheck()
    {
        for(int i=0;i<SkillManager.SKILLLIST_CAPACITY;i++)
        {
            if (i != _selectPicSkillNumber)
            {
                // �I�΂�Ă��Ȃ��X�L���Ȃ��A�N�e�B�u
                SkillManager.instance.SetIsSkillActiveFlags(i);
                _isSkillActives[i] = false;
            }
            else
            {
                // �I�΂�Ă���X�L�����A�N�e�B�u
                SkillManager.instance.SetIsSkillActiveFlags(i,true);
                _isSkillActives[i] = true;
            }
        }
       
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class OptioStatus 
{
    /// �Ō�ɑI��ł����X�L���@int 
    /// �m�[�c�̑����@float 
    /// �m�[�c�̔���ʒu�@float
    /// �m�[�c�ƃm�[�c�̊Ԃ̐� bool
    /// BGM�̉��ʁ@float
    /// SE�̉��ʁ@float
    /// �m�[�c�̌����ځ@int
    /// SE�̉��@int
    /// 

    private static int _skillIndex;
    public static int GetSkillIndex() { return _skillIndex; }

    private static float _notesSpeed;
    private static float _notesHitLinePos;

    private static bool _notesToNotesLineFlag;

    private static float _BGM_Volume;
    private static float _SE_Volume;

    private static int _notesID;
    private static int _SEID;

    private static bool OneFlag = false;
    public static void Initialize() 
    {
        //��x�����ʂ�Ȃ��悤�ɂ��鏈��
        if (OneFlag) return;

        OneFlag = true;




    }

}
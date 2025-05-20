using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class OptioStatus 
{
    /// 最後に選んでいたスキル　int 
    /// ノーツの速さ　float 
    /// ノーツの判定位置　float
    /// ノーツとノーツの間の線 bool
    /// BGMの音量　float
    /// SEの音量　float
    /// ノーツの見た目　int
    /// SEの音　int
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
        //一度しか通らないようにする処理
        if (OneFlag) return;

        OneFlag = true;




    }

}
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

    private static int _skillIndex=0;
    public static int GetSkillIndex() { return _skillIndex; }
    public static void SetSkillIndex(int index) {  _skillIndex = index; }

    private static float _notesSpeed = 0;
    public static float GetNotesSpeed() { return _notesSpeed; }
    public static void SetNotesSpeed(float num) { _notesSpeed = num; }

    private static float _notesHitLinePos = 0;
    public static float GetNotesHitLinePos() { return _notesHitLinePos; }
    public static void SetNotesHitLinePos(float num) { _notesHitLinePos = num; }

    private static bool _notesTouchPos = false;
    public static bool GetNotesTouchPos() { return _notesTouchPos; }
    public static void SetNotesTouchPos(bool flag) { _notesTouchPos = flag; }

    private static float _notesTouchOffset = 0;
    public static float GetNotesTouchOffset() { return _notesTouchOffset; }
    public static void SetNotesTouchOffset(float num) { _notesTouchOffset = num; }


    private static bool _notesToNotesLineFlag = false;
    public static bool GetNotesToNotesLineFlag() { return _notesToNotesLineFlag; }
    public static void SetNotesToNotesLineFlag(bool flag) { _notesToNotesLineFlag = flag; }


    private static float _BGM_Volume = 0;
    public static float GetBGM_Volume() { return _BGM_Volume; }
    public static void SetBGM_Volume(float num) { _BGM_Volume = num; }

    private static float _SE_Volume = 0;
    public static float GetSE_Volume() { return _SE_Volume; }
    public static void SetSE_Volume(float num) { _SE_Volume = num; }




    private static int _notesID = 0;
    public static int GetNotesID() { return _notesID; }
    public static void SetNotesID(int num) { _notesID = num; }

    private static int _SEID = 0;
    public static int GetSEID() { return _SEID; }
    public static void SetSEID(int ID) { _SEID = ID; }


    private static bool OneFlag = false;
    public static void Initialize() 
    {
        //一度しか通らないようにする処理
        if (OneFlag) return;

        OneFlag = true;

        LoadData.LoadOpsiton();


    }

}
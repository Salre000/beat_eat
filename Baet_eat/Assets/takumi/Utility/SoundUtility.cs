using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundUtility 
{
    public static SoundManager soundManager { set;  get; }
    public static void MainBGMStop() { soundManager.MainBGMStop(); }
    public static void MainBGMStart() { soundManager.MainBGMStart(); }

    public static void NotesNormalHitSoundPlay() { soundManager.StartNotesNormalHitSound(); }
    public static void NotesFlickHitSoundPlay() { soundManager.StartNotesFlickHitSound(); }
    public static void NotesLongHitSoundPlay() { soundManager.StartNotesLongHitSound(); }
    public static void NotesSkillHitSoundPlay() { soundManager.StartNotesSkillHitSound(); }

    public static float GetNowTime() { return soundManager.GetNowTime(); }

    public static void SetObject(GameObject aa) { soundManager._sound = aa; }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundUtility 
{
    public static SoundManager soundManager { set; private get; }
    public static void MainBGMStop() { soundManager.MainBGMStop(); }
    public static void MainBGMStart() { soundManager.MainBGMStart(); }

    public static void NotesHitSoundPlay() { soundManager.StartNotesHitSound(); }

    public static float GetNowTime() { return soundManager.GetNowTime(); }

    public static void SetObject(GameObject aa) { soundManager._sound = aa; }



}
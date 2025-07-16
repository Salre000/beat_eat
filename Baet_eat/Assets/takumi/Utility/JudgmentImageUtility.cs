using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JudgmentImageUtility
{
    public static JudgmentImageManager judgmentImageManager;

    public static void SetNowJudgmentObject(int index) { judgmentImageManager.SetNowJudgmentObject(index); }
    public static void SetNowJudgmentObjectPos(Vector3 pos) { judgmentImageManager.SetImagePos(Camera.main.WorldToScreenPoint(pos)); }



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JudgmentImageUtility
{
    public static JudgmentImageManager judgmentImageManager;

    public static void SetNowJudgmentObject(int index) { judgmentImageManager.SetNowJudgmentObject(index); }
    public static void SetNowJudgmentObjectPos(Vector3 pos)
    {
        float ENDPos = -6.25f;
        if (pos.y > 0.4) ENDPos = -4f;

        pos-=new Vector3(0,0,pos.z);
        pos += new Vector3(0, 0, ENDPos);
        
        judgmentImageManager.SetImagePos(Camera.main.WorldToScreenPoint(pos));
    }



}
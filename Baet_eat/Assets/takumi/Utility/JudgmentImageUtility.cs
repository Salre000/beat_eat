using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JudgmentImageUtility
{
    public static JudgmentImageManager judgmentImageManager;

    public static void SetNowJudgmentObject(int index, int index2) { judgmentImageManager.SetNowJudgmentObject(index, index2); }
    public static void SetNowJudgmentObjectPos(Vector3 pos)
    {
        float ENDPos = -6.25f;
        if (pos.y > 0.4) ENDPos = DessertManager.TAP_AREA_DESSERT;

        pos -= new Vector3(0, 0, pos.z);
        pos += new Vector3(0, 0, ENDPos);

        EffectManager.instance.StartEffect(pos);


        judgmentImageManager.SetImagePos(Camera.main.WorldToScreenPoint(pos));
    }



}
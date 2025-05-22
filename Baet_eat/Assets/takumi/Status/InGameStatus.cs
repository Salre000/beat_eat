using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStatus
{
    private static float score = 0;
    private const float MAX_SCORE = 1010000.0f;
    //ゲーム開始時に決定する1ノーツ当たりのスコア
    private static float upScore = -1;

    private static int HP = 1000;

    private static int MAX_HP = 1000;

    private static int GetMAXHP() {  return MAX_HP; }
    private static int damege = 50;

    private static int speed = 1;

    private static int[][] judgments = new int[5][];

    private static int combo = 0;
    private static int MaxCombo = 0;

    private static System.Action ChengeHpAction;
    public void SetChengeHPUIAction(System.Action damegeUIAction) { ChengeHpAction = damegeUIAction; }

    public InGameStatus()
    {
        HP = 1000;
        score = 0;

        for (int i = 0; i < judgments.Length; i++)
        {
            judgments[i] = new int[2];
            for (int j = 0; j < judgments[i].Length; j++)
            {
                judgments[i][j] = 0;


            }
        }


    }

    public static float GetScore() { return score; }
    public static int GetHP() { return HP; }

    //判定による上昇スコアの変動ありの関数
    public static void AddScore(float rete) { score += upScore * rete; }

    public static void SetUpScore(int notesCount)
    {
        float upscore = MAX_SCORE / notesCount;

        upScore = upscore;
    }

    static bool HeelFlag = false;  
    public static void HPDamege()
    {
        HP -= damege;
        combo = 0;
        if(ChengeHpAction!=null)ChengeHpAction();
        if (HP <= 150&&!HeelFlag) 
        {
            HeelFlag = true;
            //回復する
            SkillManager.instance.heelHp.Execute();
            if (ChengeHpAction != null) ChengeHpAction();
        }

    }
    public static void HPHeel(int heel) 
    {
        if (HP + heel >= 2000)
        {
            HP = 2000;
            return;
        }
        HP += heel;
    }

    public static int GetSpeed() { return speed; }

    private static bool heelFlag = false;
    //判定の計算をする関数
    public static void SetJudgments(int index, int index2)
    {

        //ヤミー以上の判定のときに
        if (index < 3) 
        {
            combo++;
            if (combo > MaxCombo) MaxCombo = combo;
        }
        else 
        {
            combo = 0;
        }
        if (index > judgments.Length)
        {
            JudgmentImageUtility.SetNowJudgmentObject(4);
            judgments[4][index2]++; return;
        }
        judgments[index][index2]++;
        JudgmentImageUtility.SetNowJudgmentObject(index);
    }
    public static int GetJudgments(int index, int index2)
    {
        return judgments[index][index2];
    }

    public static int GetCombo() { return combo; }

    public static publicEnum.ClearRank GetScoreClearRank(int score) 
    {
        float scoreRate = (float)MAX_SCORE / (float)publicEnum.ClearRank.MAX + 2;

        int scoreRank = (int)((float)score / scoreRate);


        switch (scoreRank) 
        {
            case 0:
            case 1: return publicEnum.ClearRank.D;
            case 2: return publicEnum.ClearRank.C;
            case 3: return publicEnum.ClearRank.B;

            case 5: return publicEnum.ClearRank.A;

            case 6: return publicEnum.ClearRank.S;

            case 7: return publicEnum.ClearRank.SPlus;



        }

        return publicEnum.ClearRank.None;


    }

}
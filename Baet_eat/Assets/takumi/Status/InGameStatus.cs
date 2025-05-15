using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStatus
{
    private static float score = 0;
    private const float MAX_SCORE = 1010000.0f;
    //�Q�[���J�n���Ɍ��肷��1�m�[�c������̃X�R�A
    private static float upScore = -1;

    private static int HP = 1000;

    private static int damege = 50;

    private static int speed = 1;

    private static int[][] judgments = new int[5][];

    public InGameStatus()
    {
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

    //����ɂ��㏸�X�R�A�̕ϓ�����̊֐�
    public static void AddScore(float rete) { score += upScore * rete; }

    public static void SetUpScore(int notesCount)
    {
        float upscore = MAX_SCORE / notesCount;

        upScore = upscore;
    }

    public static void HPDamege()
    {
        HP -= damege;
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

    //����̌v�Z������֐�
    public static void SetJudgments(int index, int index2)
    {
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

}
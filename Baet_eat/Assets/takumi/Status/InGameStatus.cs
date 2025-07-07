using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStatus
{
    //後で可変にする
    private static int NotesID = 0;
    private static int SoundSEID = 0;

    public static void AddNotesID()
    {
        NotesID++;
        if (NotesID >= (int)NotesMaterialTypeEnum.NotesMaterialType.MAX)
            NotesID = (int)NotesMaterialTypeEnum.NotesMaterialType.MAX - 1;
    }
    public static void SbuNotesID()
    {
        NotesID--;
        if (NotesID < 0)
            NotesID = 0;
    }

    public static int GetNotesID() {  return NotesID; }

    public static void AddSoundSEID()
    {
        SoundSEID++;
        if (SoundSEID >= (int)SoundSEEnum.SoundSEType.MAX)
            SoundSEID = (int)SoundSEEnum.SoundSEType.MAX - 1;
    }

    public static void SbuSoundSEID()
    {
        SoundSEID--;
        if (SoundSEID < 0)
            SoundSEID = 0;
    }
    public static int GetSoundSEID() {  return SoundSEID; }



    private static float score = 0;
    private const float MAX_SCORE = 1010000.0f;
    public static float GetMAXScore() { return MAX_SCORE; }
    //ゲーム開始時に決定する1ノーツ当たりのスコア
    private static float upScore = -1;

    private static float ScoreRate = 0;
    public static float GetScoreRate() { return ScoreRate; }

    private static int HP = 1000;

    private static int MAX_HP = 1000;

    private static AudioClip nowMusic;
    public static void SetNowMusic(AudioClip clip) { nowMusic = clip; }
    public static AudioClip GetNowMusic() { return nowMusic; }
    private static MusicData Data;
    public static void SetMusicData(MusicData music) { Data = music; }
    public static MusicData GetMusicData() { return Data; }
    public static int GetMAXHP() { return MAX_HP; }
    private static int damege = 50;


    private static int[][] judgments = new int[5][];
    private static int[] NoesTypeSuccess = { 0, 0, 0, 0 };
    private static int[] NoesTypeMIss = { 0, 0, 0, 0 };

    private static int combo = 0;
    private static int MaxCombo = 0;

    private static int NotesCount = 0;
    public static int GetNotesCount() { return NotesCount; }

    private static System.Action ChengeHpAction;
    public static void SetChengeHPUIAction(System.Action damegeUIAction) { ChengeHpAction = damegeUIAction; }

    private static bool Auto = false;
    public static bool GetAuto() { return Auto; }
    public static void AutoMode(bool flag = true) { Auto = flag; }
    public InGameStatus()
    {
        HP = 1000;
        score = 0;
        combo = 0;
        MaxCombo = 0;
        for (int i = 0; i < judgments.Length; i++)
        {
            judgments[i] = new int[2];
            for (int j = 0; j < judgments[i].Length; j++)
            {
                judgments[i][j] = 0;


            }
        }
        for(int i = 0; i < 4; i++) 
        {
            NoesTypeMIss[i] = NoesTypeSuccess[i] = 0;
        }
        ScoreRate = (float)MAX_SCORE / (float)(publicEnum.ClearRank.MAX + 2);


    }

    public static float GetScore() { return score; }
    public static int GetHP() { return HP; }

    //判定による上昇スコアの変動ありの関数
    public static void AddScore(float rete)
    {
        score += upScore * rete; LineUtility.SetScoreGage();
    }

    public static void SetUpScore(int notesCount)
    {
        float upscore = MAX_SCORE / (float)notesCount;
        NotesCount = notesCount;

        upScore = upscore;
        Debug.Log(upScore + ":" + notesCount);
    }
    public static void AddNoesTypeSuccess(int notesType) { NoesTypeSuccess[notesType]++; }
    public static void AddNoesTypeMIss(int notesType) { NoesTypeMIss[notesType]++; }
    public static int GetNoesTypeSuccess(int notesType) { return NoesTypeSuccess[notesType]; }
    public static int GetNoesTypeMIss(int notesType) { return NoesTypeMIss[notesType]; }
    static bool HeelFlag = false;
    public static void HPDamege()
    {
        HP -= damege;
        combo = 0;
        if (ChengeHpAction != null) ChengeHpAction();
        if (HP <= 150 && !HeelFlag)
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

    public static int GetMAXCombo() { return MaxCombo; }
    public static int GetCombo() { return combo; }

    public static publicEnum.ClearRank GetScoreClearRank(int score)
    {

        int scoreRank = (int)((float)score / ScoreRate);


        switch (scoreRank)
        {
            case 0:
            case 1: return publicEnum.ClearRank.D;
            case 2: return publicEnum.ClearRank.C;
            case 3: return publicEnum.ClearRank.B;

            case 4: return publicEnum.ClearRank.A;

            case 5: return publicEnum.ClearRank.S;

            case 6: return publicEnum.ClearRank.SPlus;
            case 7: return publicEnum.ClearRank.SPlus;
            case 8: return publicEnum.ClearRank.SPlus;
            case 9: return publicEnum.ClearRank.SPlus;
        }

        return publicEnum.ClearRank.None;

    }

    public static publicEnum.ClearStates CheckEnd()
    {
        if (NotesCount == judgments[0][0]) return publicEnum.ClearStates.ALLDC;
        else if (combo >= NotesCount) return publicEnum.ClearStates.FullCombo;
        else return publicEnum.ClearStates.Clear;
    }

}
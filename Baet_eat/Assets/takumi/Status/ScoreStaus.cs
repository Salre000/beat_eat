using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreStaus
{

    private static List<List<int>> DessertScore;
    public static void SetDessertScore(int ID, publicEnum.Difficulty difficulty, int score) { DessertScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertScore(int ID, publicEnum.Difficulty difficulty) { return DessertScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> MainDeshScore;
    public static void SetMainDeshScore(int ID, publicEnum.Difficulty difficulty, int score) { MainDeshScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshScore(int ID, publicEnum.Difficulty difficulty) { return MainDeshScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> DessertOverScore;
    public static void SetDessertOverScore(int ID, publicEnum.Difficulty difficulty, int score) { DessertOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertOverScore(int ID, publicEnum.Difficulty difficulty) { return DessertOverScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> MainDeshOverScore;
    public static void SetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty, int score) { MainDeshOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty) { return MainDeshOverScore[difficulty.ChengeInt()][ID]; }


    private static List<List<publicEnum.ClearRank>> DessertClearRanks;
    public static void SetDessertClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { DessertClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetDessertClearRanks(int ID, publicEnum.Difficulty difficulty) { return DessertClearRanks[difficulty.ChengeInt()][ID]; }

    private static List<List<publicEnum.ClearRank>> MainDeshClearRanks;
    public static void SetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { MainDeshClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty) { return MainDeshClearRanks[difficulty.ChengeInt()][ID]; }



    private static List<List<publicEnum.ClearStates>> DessertClearStates;
    public static void SetDessertDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { DessertClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetDessertDifficulty(int ID, publicEnum.Difficulty difficulty) { return DessertClearStates[difficulty.ChengeInt()][ID]; }

    private static List<List<publicEnum.ClearStates>> MainDeshClearStates;
    public static void SetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { MainDeshClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty) { return MainDeshClearStates[difficulty.ChengeInt()][ID]; }

    public static int nowMusi=-1;
    public static publicEnum.Difficulty nowDifficulty=publicEnum.Difficulty.None;



    private static bool OneFlag = false;
    public static void Initialize(int musicCount)
    {
        nowMusi = -1;
        nowDifficulty = publicEnum.Difficulty.None;

        if (OneFlag) return;
        DessertScore = new List<List<int>>();
        DessertOverScore = new List<List<int>>();
        DessertClearRanks = new List<List<publicEnum.ClearRank>>();
        DessertClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            DessertScore.Add(new List<int>(musicCount));
            DessertOverScore.Add(new List<int>(musicCount));
            DessertClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            DessertClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        MainDeshScore = new List<List<int>>();
        MainDeshOverScore = new List<List<int>>();
        MainDeshClearRanks = new List<List<publicEnum.ClearRank>>();
        MainDeshClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            MainDeshScore.Add(new List<int>(musicCount));
            MainDeshOverScore.Add(new List<int>(musicCount));
            MainDeshClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            MainDeshClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                MainDeshScore[i].Add(-1);
                MainDeshOverScore[i].Add(-1);
                MainDeshClearRanks[i].Add(publicEnum.ClearRank.None);
                MainDeshClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }
        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                DessertScore[i].Add(-1);
                DessertOverScore[i].Add(-1);
                DessertClearRanks[i].Add(publicEnum.ClearRank.None);
                DessertClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }

        //ƒXƒRƒA‚Ìó‹µ‚ðŠl“¾
        LoadData.LoadFoundation();

        OneFlag = true;

    }




}
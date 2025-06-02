using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreStatus
{

    private static List<List<int>> _dessertScore;
    public static void SetDessertScore(int ID, publicEnum.Difficulty difficulty, int score) { _dessertScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertScore(int ID, publicEnum.Difficulty difficulty) { return _dessertScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> _mainDeshScore;
    public static void SetMainDeshScore(int ID, publicEnum.Difficulty difficulty, int score) { _mainDeshScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshScore(int ID, publicEnum.Difficulty difficulty) { return _mainDeshScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> _dessertOverScore;
    public static void SetDessertOverScore(int ID, publicEnum.Difficulty difficulty, int score) { _dessertOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertOverScore(int ID, publicEnum.Difficulty difficulty) { return _dessertOverScore[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> _mainDeshOverScore;
    public static void SetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty, int score) { _mainDeshOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty) { return _mainDeshOverScore[difficulty.ChengeInt()][ID]; }


    private static List<List<publicEnum.ClearRank>> _dessertClearRanks;
    public static void SetDessertClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { _dessertClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetDessertClearRanks(int ID, publicEnum.Difficulty difficulty) { return _dessertClearRanks[difficulty.ChengeInt()][ID]; }

    private static List<List<publicEnum.ClearRank>> _mainDeshClearRanks;
    public static void SetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { _mainDeshClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty) { return _mainDeshClearRanks[difficulty.ChengeInt()][ID]; }



    private static List<List<publicEnum.ClearStates>> _dessertClearStates;
    public static void SetDessertDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { if ((int)_dessertClearStates[difficulty.ChengeInt()][ID] > (int)clearStates) return; _dessertClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetDessertDifficulty(int ID, publicEnum.Difficulty difficulty) { return _dessertClearStates[difficulty.ChengeInt()][ID]; }

    private static List<List<publicEnum.ClearStates>> _mainDeshClearStates;
    public static void SetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { _mainDeshClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty) { return _mainDeshClearStates[difficulty.ChengeInt()][ID]; }

    private static List<List<int>> _musicLevel = new List<List<int>>(MusicManager.CAPACITY);
    public static void AddSetMusicLevel(List<int> list) { _musicLevel.Add(list); }

    public  static int GetMusicLevel(int ID,int Difficulty) { return _musicLevel[ID][Difficulty]; }

    public static int nowMusic=0;
    public static publicEnum.Difficulty nowDifficulty=publicEnum.Difficulty.Drink;



    private static bool OneFlag = false;
    public static void Initialize(int musicCount)
    {

        if (OneFlag) return;
        _dessertScore = new List<List<int>>();
        _dessertOverScore = new List<List<int>>();
        _dessertClearRanks = new List<List<publicEnum.ClearRank>>();
        _dessertClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            _dessertScore.Add(new List<int>(musicCount));
            _dessertOverScore.Add(new List<int>(musicCount));
            _dessertClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            _dessertClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        _mainDeshScore = new List<List<int>>();
        _mainDeshOverScore = new List<List<int>>();
        _mainDeshClearRanks = new List<List<publicEnum.ClearRank>>();
        _mainDeshClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            _mainDeshScore.Add(new List<int>(musicCount));
            _mainDeshOverScore.Add(new List<int>(musicCount));
            _mainDeshClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            _mainDeshClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                _mainDeshScore[i].Add(-1);
                _mainDeshOverScore[i].Add(-1);
                _mainDeshClearRanks[i].Add(publicEnum.ClearRank.None);
                _mainDeshClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }
        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                _dessertScore[i].Add(-1);
                _dessertOverScore[i].Add(-1);
                _dessertClearRanks[i].Add(publicEnum.ClearRank.None);
                _dessertClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }

        //ƒXƒRƒA‚Ìó‹µ‚ðŠl“¾
        LoadData.LoadFoundation();
        LoadData.LoadMusicLevel();


        OneFlag = true;

    }




}
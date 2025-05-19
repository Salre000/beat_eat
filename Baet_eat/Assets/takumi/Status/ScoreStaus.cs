using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  ScoreStaus 
{

    private static List<List<int>> DessertScore;
    private static List<List<int>> MainDeshScore;

    private static List<List<int>> DessertOverScore;
    private static List<List<int>> MainDeshOverScore;

    private static List<List<publicEnum.ClearRank>> DessertClearRanks;
    private static List<List<publicEnum.ClearRank>> MainDeshClearRanks;


    private static List<List<publicEnum.Difficulty>> DessertDifficulty;
    private static List<List<publicEnum.Difficulty>> MainDeshDifficulty;

    public static void Initialize(int musicCount) 
    {

        for(int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++) 
        {
            DessertScore.Add(new List<int>(musicCount));
            DessertOverScore.Add(new List<int>(musicCount));
            DessertClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            DessertDifficulty.Add(new List<publicEnum.Difficulty>(musicCount));
        }

        for(int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++) 
        {
            MainDeshScore.Add(new List<int>(musicCount));
            MainDeshOverScore.Add(new List<int>(musicCount));
            MainDeshClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            MainDeshDifficulty.Add(new List<publicEnum.Difficulty>(musicCount));
        }

    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData 
{
    public  List<List<int>> dessertScore;
    public  List<List<int>> mainDeshScore;
    public  List<List<int>> dessertOverScore;
    public  List<List<int>> mainDeshOverScore;
    public  List<List<publicEnum.ClearRank>> dessertClearRanks;
    public  List<List<publicEnum.ClearRank>> mainDeshClearRanks;
    public  List<List<publicEnum.ClearStates>> dessertClearStates;
    public  List<List<publicEnum.ClearStates>> mainDeshClearStates;

    public ScoreData() { }
}

// JsonUtility は List<List<T>> を直接シリアライズできないため、保存時だけ1階層ラッパーに詰め替える
[Serializable]
public class IntListWrapper
{
    public List<int> values;
    public IntListWrapper() { }
    public IntListWrapper(List<int> values) { this.values = values; }
}

[Serializable]
public class ClearRankListWrapper
{
    public List<publicEnum.ClearRank> values;
    public ClearRankListWrapper() { }
    public ClearRankListWrapper(List<publicEnum.ClearRank> values) { this.values = values; }
}

[Serializable]
public class ClearStatesListWrapper
{
    public List<publicEnum.ClearStates> values;
    public ClearStatesListWrapper() { }
    public ClearStatesListWrapper(List<publicEnum.ClearStates> values) { this.values = values; }
}

[Serializable]
public class ScoreDataSaveModel
{
    public List<IntListWrapper> dessertScore;
    public List<IntListWrapper> mainDeshScore;
    public List<IntListWrapper> dessertOverScore;
    public List<IntListWrapper> mainDeshOverScore;
    public List<ClearRankListWrapper> dessertClearRanks;
    public List<ClearRankListWrapper> mainDeshClearRanks;
    public List<ClearStatesListWrapper> dessertClearStates;
    public List<ClearStatesListWrapper> mainDeshClearStates;

    public static ScoreDataSaveModel FromScoreData(ScoreData data)
    {
        return new ScoreDataSaveModel
        {
            dessertScore = data.dessertScore.ConvertAll(list => new IntListWrapper(list)),
            mainDeshScore = data.mainDeshScore.ConvertAll(list => new IntListWrapper(list)),
            dessertOverScore = data.dessertOverScore.ConvertAll(list => new IntListWrapper(list)),
            mainDeshOverScore = data.mainDeshOverScore.ConvertAll(list => new IntListWrapper(list)),
            dessertClearRanks = data.dessertClearRanks.ConvertAll(list => new ClearRankListWrapper(list)),
            mainDeshClearRanks = data.mainDeshClearRanks.ConvertAll(list => new ClearRankListWrapper(list)),
            dessertClearStates = data.dessertClearStates.ConvertAll(list => new ClearStatesListWrapper(list)),
            mainDeshClearStates = data.mainDeshClearStates.ConvertAll(list => new ClearStatesListWrapper(list)),
        };
    }

    public ScoreData ToScoreData()
    {
        return new ScoreData
        {
            dessertScore = dessertScore.ConvertAll(w => w.values),
            mainDeshScore = mainDeshScore.ConvertAll(w => w.values),
            dessertOverScore = dessertOverScore.ConvertAll(w => w.values),
            mainDeshOverScore = mainDeshOverScore.ConvertAll(w => w.values),
            dessertClearRanks = dessertClearRanks.ConvertAll(w => w.values),
            mainDeshClearRanks = mainDeshClearRanks.ConvertAll(w => w.values),
            dessertClearStates = dessertClearStates.ConvertAll(w => w.values),
            mainDeshClearStates = mainDeshClearStates.ConvertAll(w => w.values),
        };
    }
}

public static class ScoreStatus
{
    private static ScoreData Score;

    public static ScoreData GetScoreData() { return Score; }
    public static void SetScoreData(ScoreData score) { Score= score; }

    public static void SetDessertScore(int ID, publicEnum.Difficulty difficulty, int score) { Score.dessertScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertScore(int ID, publicEnum.Difficulty difficulty) { return Score.dessertScore[difficulty.ChengeInt()][ID]; }

    public static void SetMainDeshScore(int ID, publicEnum.Difficulty difficulty, int score) { Score.mainDeshScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshScore(int ID, publicEnum.Difficulty difficulty) { return Score.mainDeshScore[difficulty.ChengeInt()][ID]; }

    public static void SetDessertOverScore(int ID, publicEnum.Difficulty difficulty, int score) { Score.dessertOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetDessertOverScore(int ID, publicEnum.Difficulty difficulty) { return Score.dessertOverScore[difficulty.ChengeInt()][ID]; }

    public static void SetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty, int score) { Score.mainDeshOverScore[difficulty.ChengeInt()][ID] = score; }
    public static int GetMainDeshOverScore(int ID, publicEnum.Difficulty difficulty) { return Score.mainDeshOverScore[difficulty.ChengeInt()][ID]; }


    public static void SetDessertClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { Score.dessertClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetDessertClearRanks(int ID, publicEnum.Difficulty difficulty) { return Score.dessertClearRanks[difficulty.ChengeInt()][ID]; }

    public static void SetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearRank rank) { Score.mainDeshClearRanks[difficulty.ChengeInt()][ID] = rank; }
    public static publicEnum.ClearRank GetMainDeshClearRanks(int ID, publicEnum.Difficulty difficulty) { return Score.mainDeshClearRanks[difficulty.ChengeInt()][ID]; }



    public static void SetDessertDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { if ((int)Score.dessertClearStates[difficulty.ChengeInt()][ID] > (int)clearStates) return; Score.dessertClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetDessertDifficulty(int ID, publicEnum.Difficulty difficulty) { return Score.dessertClearStates[difficulty.ChengeInt()][ID]; }

    public static void SetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty, publicEnum.ClearStates clearStates) { Score.mainDeshClearStates[difficulty.ChengeInt()][ID] = clearStates; }
    public static publicEnum.ClearStates GetMainDeshDifficulty(int ID, publicEnum.Difficulty difficulty) { return Score.mainDeshClearStates[difficulty.ChengeInt()][ID]; }

    private static List<string[]> _musicLevel = new List<string[]>(MusicManager.CAPACITY);
    public static void AddSetMusicLevel(List<string[]> list) { _musicLevel=(list); }

    public  static string GetMusicLevel(int ID,int Difficulty) { return _musicLevel[ID][Difficulty]; }

    public static int nowMusic=0;
    public static publicEnum.Difficulty nowDifficulty=publicEnum.Difficulty.Drink;



    private static bool OneFlag = false;
    public static void Initialize(int musicCount)
    {

        if (OneFlag) return;

        //�X�R�A�̏󋵂��l��
        LoadData.LoadFoundation(()=>DataInitialize(musicCount));
        LoadData.LoadMusicLevel();


        OneFlag = true;

    }
    public static void DataInitialize(int musicCount) 
    {

        Score =new ScoreData();
        Debug.Log("�C�j�V�����C�Y");
        Score.dessertScore = new List<List<int>>();
        Score.dessertOverScore = new List<List<int>>();
        Score.dessertClearRanks = new List<List<publicEnum.ClearRank>>();
        Score.dessertClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            Score.dessertScore.Add(new List<int>(musicCount));
            Score.dessertOverScore.Add(new List<int>(musicCount));
            Score.dessertClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            Score.dessertClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        Score.mainDeshScore = new List<List<int>>();
        Score.mainDeshOverScore = new List<List<int>>();
        Score.mainDeshClearRanks = new List<List<publicEnum.ClearRank>>();
        Score.mainDeshClearStates = new List<List<publicEnum.ClearStates>>();

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            Score.mainDeshScore.Add(new List<int>(musicCount));
            Score.mainDeshOverScore.Add(new List<int>(musicCount));
            Score.mainDeshClearRanks.Add(new List<publicEnum.ClearRank>(musicCount));
            Score.mainDeshClearStates.Add(new List<publicEnum.ClearStates>(musicCount));
        }

        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                Score.mainDeshScore[i].Add(-1);
                Score.mainDeshOverScore[i].Add(-1);
                Score.mainDeshClearRanks[i].Add(publicEnum.ClearRank.None);
                Score.mainDeshClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }
        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            for (int music = 0; music < musicCount; music++)
            {

                Score.dessertScore[i].Add(-1);
                Score.dessertOverScore[i].Add(-1);
                Score.dessertClearRanks[i].Add(publicEnum.ClearRank.None);
                Score.dessertClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }

    }

    public static void AddMusic(int count) 
    {
        for (int i = 0; i < publicEnum.Difficulty.dessert.ChengeInt(); i++)
        {
            for (int music = 0; music < count; music++)
            {

                Score.mainDeshScore[i].Add(-1);
                Score.mainDeshOverScore[i].Add(-1);
                Score.mainDeshClearRanks[i].Add(publicEnum.ClearRank.None);
                Score.mainDeshClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }
        for (int i = 0; i < publicEnum.Difficulty.MAX.ChengeInt(); i++)
        {
            for (int music = 0; music < count; music++)
            {

                Score.dessertScore[i].Add(-1);
                Score.dessertOverScore[i].Add(-1);
                Score.dessertClearRanks[i].Add(publicEnum.ClearRank.None);
                Score.dessertClearStates[i].Add(publicEnum.ClearStates.None);

            }

        }
    }


}
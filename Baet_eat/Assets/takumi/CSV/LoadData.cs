using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static publicEnum;

public static class LoadData
{
    public static void LoadFoundation()
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(SaveData.FoundationFileName);

        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }

        int nowLine = 0;
        int musicMax = dataBase.musicData.Count;

        //デザート有のループ　スコアの記録
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);

                ScoreStatus.SetDessertScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();

            nowLine++;

        }
        //デザート無しのループ　スコアの記録
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);

                ScoreStatus.SetMainDeshScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();

            nowLine++;
        }
        //デザート有のループ　オーバースコアの記録
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertOverScore(musicID, (Difficulty)difficulty, score);


                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();

            nowLine++;
        }
        //デザート無しのループ　オーバースコアの記録
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshOverScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();
            nowLine++;
        }
        //デザート有のループ　クリアランクの記録
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<ClearRank> clearScore = new List<ClearRank>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearRank score = (ClearRank)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertClearRanks(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();
            nowLine++;
        }
        //デザート有のループ　クリアランクの記録
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<ClearRank> clearScore = new List<ClearRank>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearRank score = (ClearRank)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshClearRanks(musicID, (Difficulty)difficulty, score);


                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();
            nowLine++;
        }
        //デザート有のループ　クリア状況の記録
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<ClearStates> clearScore = new List<ClearStates>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearStates score = (ClearStates)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertDifficulty(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();
            nowLine++;
        }
        //デザート有のループ　クリア状況の記録
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<ClearStates> clearScore = new List<ClearStates>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearStates score = (ClearStates)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshDifficulty(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //インスタンスを経由して情報を渡す
            clearScore.Clear();
            nowLine++;
        }
    }

    public static void LoadOpsiton()
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(SaveData.OpstionFileName);

        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }
        /// 最後に選んでいたスキル　int 
        /// ノーツの速さ　float 
        /// ノーツの判定位置　float
        /// ノーツとノーツの間の線 bool
        /// BGMの音量　float
        /// SEの音量　float
        /// ノーツの見た目　int
        /// SEの音　int

        int LineCount = 0;
        LineCount++;
        OptioStatus.SetNotesSpeed(float.Parse(csvDatas[LineCount][0]));LineCount++;
        OptioStatus.SetNotesHitLinePos(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptioStatus.SetNotesToNotesLineFlag(bool.Parse(csvDatas[LineCount][0])); LineCount++;
        OptioStatus.SetBGM_Volume(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptioStatus.SetSE_Volume(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptioStatus.SetNotesID(int.Parse(csvDatas[LineCount][0])); LineCount++;
        OptioStatus.SetSEID(int.Parse(csvDatas[LineCount][0])); LineCount++; ;

    }

    public static void LoadAchievements()
    {



    }

    private static string MusicLevelPass = "MusicLevel";


    public static void LoadMusicLevel()
    {
        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //ファイルパスとファイルの名前を繋げる
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(MusicLevelPass);

        //繋げたファイルパスを使いファイルのロードを行う
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());

        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }
        Debug.Log(csvDatas[0][1]);

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);


        List<int> list = new List<int>();
        for (int i = 0; i < dataBase.musicData.Count; i++)
        {

            list.Clear();

            for (int j = 0; j < 5; j++)
            {
                list.Add(int.Parse(csvDatas[i][j+1]));

            }

            ScoreStatus.AddSetMusicLevel(list);

        }

    }

}
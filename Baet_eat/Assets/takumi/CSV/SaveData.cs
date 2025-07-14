using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;

//オプションデータをCSVに保存するクラス
public static class SaveData
{
    public static string MusicDataName = "MusicDataBase";

    public static string OpstionFileName = "OpsitonData";
    public static string FoundationFileName = "FoundationData";
    public static string ResultsFileName = "ResultsData";
    public static string AchiveMentFileName = "AchiveMent";
    public static string FILE_PASS = "/Resources/";
    public static string FILR_EXTENSION = ".csv";

    public static string Spece = ",";

    //順番基礎情報
    ///*
    ///
    /// スコア　曲の数*難易度(デザートあり)　int　
    /// スコア　曲の数*難易度(デザートなし)　int　
    /// オーバースコア　曲の数*難易度(デザートあり)　int　
    /// オーバースコア　曲の数*難易度(デザートなし)　int　
    /// スコアランク　曲の数*難易度(デザートあり)　enum　
    /// スコアランク　曲の数*難易度(デザートなし)　enum　
    /// クリア状況　曲の数＊難易度(デザートあり)　enum
    /// クリア状況　曲の数＊難易度(デザートなし)　enum
    ///*/

    //順番オプション
    ///
    /// *
    /// 最後に選んでいたスキル　int 
    /// ノーツの速さ　float 
    /// ノーツの判定位置　float
    /// 成功判定の位置
    /// 成功判定のoffset
    /// ノーツとノーツの間の線 bool
    /// BGMの音量　float
    /// SEの音量　float
    /// ノーツの見た目　int
    /// SEの音　int
    /// */
    /// 

    //基礎情報
    public static void SaveFoundation(int Startnumber = 0)
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(MusicDataName);

        string filePath = Path.Combine(Application.persistentDataPath, FoundationFileName + ".txt");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
        ScoreData scoreData;
        if (Startnumber == 1) ScoreStatus.DataInitialize(dataBase.musicData.Count);

        scoreData= ScoreStatus.GetScoreData();


        formatter.Serialize(stream, scoreData);
        stream.Close();


    }
    //アチーブメントの情報
    public static void SaveAchiveMent()
    {


        string filePath = Path.Combine(Application.persistentDataPath, AchiveMentFileName + ".txt");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);

        Achievements Data = AchievementStatus.achievements;


        formatter.Serialize(stream,Data);
        stream.Close();


    }
    //オプション情報
    public static void SaveOption(int Startnumber = 0)
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.persistentDataPath + "/" + OpstionFileName + FILR_EXTENSION, false);

        /// 最後に選んでいたスキル　int 
        /// ノーツの速さ　float 
        /// ノーツの判定位置　float
        /// ノーツとノーツの間の線
        /// BGMの音量　float
        /// SEの音量　float
        /// ノーツの見た目　int
        /// SEの音　int
        /// 選んでいたスキル　int

        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetSkillIndex().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesSpeed().ToString() : 1);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesHitLinePos().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesTouchPos().ToString() : "False");
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesTouchOffset().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesToNotesLineFlag().ToString() : "False");
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetBGM_Volume().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetSE_Volume().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetNotesID().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetSEID().ToString() : 0);
        sw.WriteLine(Startnumber == 0 ? OptionStatus.GetSkillIndex().ToString() : 0);

        sw.Flush();
        sw.Close();



    }
    public static void SaveResults()
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + OpstionFileName + FILR_EXTENSION, false);

        sw.Flush();
        sw.Close();



    }



}
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// ノーツとノーツの間の線
    /// BGMの音量　float
    /// SEの音量　float
    /// ノーツの見た目　int
    /// SEの音　int
    /// */
    /// 

    //基礎情報
    public static void SaveFoundation()
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(MusicDataName);
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FoundationFileName + FILR_EXTENSION, false);

        //今のlineの番号
        int LineCount = 0;

        int musicMax = dataBase.musicData.Count;

        StringBuilder sb = new StringBuilder();

        sb.Clear();

        //デザートありの難易度の曲のスコアを記録
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append(ScoreStatus.GetDessertScore(soundNumber,(publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //デザート無しの難易度の曲のスコアを記録
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append(ScoreStatus.GetMainDeshScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //デザートありの難易度の曲のオーバースコアを記録
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append(ScoreStatus.GetDessertOverScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //デザート無しの難易度の曲のオーバースコアを記録
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append(ScoreStatus.GetMainDeshOverScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //デザートありの難易度の曲のクリアランクを記録
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append((int)ScoreStatus.GetDessertClearRanks(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //デザート無しの難易度の曲のクリアランクを記録
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append((int)ScoreStatus.GetMainDeshClearRanks(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //デザートありの難易度の曲のクリア状況を記録
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append((int)ScoreStatus.GetDessertDifficulty(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //デザート無しの難易度の曲のクリア状況を記録
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //ここにそれぞれのスコアを入れる
                sb.Append((int)ScoreStatus.GetMainDeshDifficulty(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }





        sw.Flush();
        sw.Close();



    }
    //オプション情報
    public static void SaveOption()
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + OpstionFileName + FILR_EXTENSION, false);

        /// 最後に選んでいたスキル　int 
        /// ノーツの速さ　float 
        /// ノーツの判定位置　float
        /// ノーツとノーツの間の線
        /// BGMの音量　float
        /// SEの音量　float
        /// ノーツの見た目　int
        /// SEの音　int

        sw.WriteLine("saigoni eranda sukiru");
        sw.WriteLine("noutuno hayasa");
        sw.WriteLine("noutuno hannteinoiti");
        sw.WriteLine("noututo noutuno senn");
        sw.WriteLine("BGMno onnrixyou");
        sw.WriteLine("SEno onnrixyou");
        sw.WriteLine("noutuno mitameID");
        sw.WriteLine("SEno otonoID");





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
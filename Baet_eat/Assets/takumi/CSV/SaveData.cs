using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Timeline;

//オプションデータをCSVに保存するクラス
public static class SaveData 
{
    public static string OpstionFileName = "OpsitonData";
    public static string FoundationFileName = "FoundationData";
    public static string ResultsFileName = "ResultsData";
    public static string FILE_PASS = "/Resources/";
    public static string FILR_EXTENSION = ".csv";

    //順番基礎情報
    ///*
    ///
    /// スコア　曲の数*難易度(デザートあり)　int　
    /// スコア　曲の数*難易度(デザートなし)　int　
    /// オーバースコア　曲の数*難易度　int　
    /// スコアランク　曲の数*難易度　enum　
    /// クリア状況　曲の数＊難易度　enum
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
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FoundationFileName + FILR_EXTENSION, false);

        //今のlineの番号
        int LineCount = 0;

        //for(int soundNumber=0;soundNumber<;soundNumber++)
        //デザートありの難易度の曲のスコアを記録
        for(int i = 0; i < 5; i++) 
        {

        }



        sw.Flush();
        sw.Close();



    }
    public static void SaveOption() 
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + OpstionFileName + FILR_EXTENSION, false);





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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using static publicEnum;

public static class LoadData
{
    public static void LoadFoundation(System.Action Initialize)
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        string filePath = Path.Combine(Application.persistentDataPath, SaveData.FoundationFileName + ".txt");
        if (File.Exists(filePath))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            ScoreStatus.SetScoreData(data);

            stream.Close();

            if (dataBase.musicData.Count > ScoreStatus.GetScoreData().dessertScore[0].Count)
            {
                ScoreStatus.AddMusic(dataBase.musicData.Count - ScoreStatus.GetScoreData().dessertScore[0].Count);
            }

        }
        else
        {
            Initialize();


        }
    }
    public static void LoadAchiveMent(System.Action Initialize)
    {

        string filePath = Path.Combine(Application.persistentDataPath, SaveData.AchiveMentFileName + ".txt");
        if (File.Exists(filePath))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            Achievements data = formatter.Deserialize(stream) as Achievements;
            AchievementStatus.achievements = data;

            stream.Close();

            if (data.GetAChiveMentStatus().Count > (int)AchievementTypeEnum.AchievementType.MAX) return;

            int count = ((int)AchievementTypeEnum.AchievementType.MAX+1) - data.GetAChiveMentStatus().Count;
            for (int i = 0; i < count; i++)
            {
                AchievementStatus.achievements.AddAchiveMentStatus(false);
                AchievementStatus.achievements.AddAChiveMentCount(0);
            }


        }
        else
        {
            Initialize();


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
        string filePath = Path.Combine(Application.persistentDataPath, SaveData.OpstionFileName + SaveData.FILR_EXTENSION);

        string[] lines = File.ReadAllLines(filePath);

        //読み込んだテキストをString型にして格納
        //StringReader reader = new StringReader(textAsset.text);

        for (int i = 0; i < lines.Length; i++)
        {
            // ,で区切ってCSVに格納
            csvDatas.Add(lines[i].Split(','));
            height++; // 行数加算
        }

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

        int LineCount = 0;
        LineCount++;
        OptionStatus.SetNotesSpeed(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetNotesHitLinePos(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetNotesTouchPos(bool.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetNotesTouchOffset(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetNotesToNotesLineFlag(bool.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetBGM_Volume(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetSE_Volume(float.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetNotesID(int.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetSEID(int.Parse(csvDatas[LineCount][0])); LineCount++;
        OptionStatus.SetSkillIndex(int.Parse(csvDatas[LineCount][0])); LineCount++;



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
        for(int i = 0; i < csvDatas.Count; i++) 
        {

            for(int j = 0; j < csvDatas[i].Length-1; j++) 
            {

                csvDatas[i][j]=csvDatas[i][j+1];

            }


        }
        ScoreStatus.AddSetMusicLevel(csvDatas);



    }

}
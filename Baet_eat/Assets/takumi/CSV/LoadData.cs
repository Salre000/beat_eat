using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using static publicEnum;

public static class LoadData
{
    public static void LoadFoundation(System.Action Initialize)
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        string filePath = Path.Combine(Application.persistentDataPath, SaveData.FoundationFileName + ".json");
        if (File.Exists(filePath))
        {

            string json = File.ReadAllText(filePath);
            ScoreDataSaveModel saveModel = JsonUtility.FromJson<ScoreDataSaveModel>(json);
            ScoreStatus.SetScoreData(saveModel.ToScoreData());

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

        // NOTE: 実績ロードは既存仕様により無効化されたまま(現状維持・スコープ外)
        string filePath = Path.Combine(Application.persistentDataPath, SaveData.AchiveMentFileName + ".json");
        if (false)//File.Exists(filePath))
        {

            string json = File.ReadAllText(filePath);
            Achievements data = JsonUtility.FromJson<Achievements>(json);
            AchievementStatus.achievements = data;

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

        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(SaveData.OpstionFileName);

        //�q�����t�@�C���p�X���g���t�@�C���̃��[�h���s��
        string filePath = Path.Combine(Application.persistentDataPath, SaveData.OpstionFileName + SaveData.FILR_EXTENSION);

        string[] lines = File.ReadAllLines(filePath);

        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        //StringReader reader = new StringReader(textAsset.text);

        for (int i = 0; i < lines.Length; i++)
        {
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(lines[i].Split(','));
            height++; // �s�����Z
        }

        /// �Ō�ɑI��ł����X�L���@int 
        /// �m�[�c�̑����@float 
        /// �m�[�c�̔���ʒu�@float
        /// ��������̈ʒu
        /// ���������offset
        /// �m�[�c�ƃm�[�c�̊Ԃ̐� bool
        /// BGM�̉��ʁ@float
        /// SE�̉��ʁ@float
        /// �m�[�c�̌����ځ@int
        /// SE�̉��@int

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
        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(MusicLevelPass);

        //�q�����t�@�C���p�X���g���t�@�C���̃��[�h���s��
        TextAsset textAsset = Resources.Load<TextAsset>(builder.ToString());


        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(textAsset.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Timeline;

//�I�v�V�����f�[�^��CSV�ɕۑ�����N���X
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

    //���Ԋ�b���
    ///*
    ///
    /// �X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g����)�@int�@
    /// �X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g�Ȃ�)�@int�@
    /// �I�[�o�[�X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g����)�@int�@
    /// �I�[�o�[�X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g�Ȃ�)�@int�@
    /// �X�R�A�����N�@�Ȃ̐�*��Փx(�f�U�[�g����)�@enum�@
    /// �X�R�A�����N�@�Ȃ̐�*��Փx(�f�U�[�g�Ȃ�)�@enum�@
    /// �N���A�󋵁@�Ȃ̐�����Փx(�f�U�[�g����)�@enum
    /// �N���A�󋵁@�Ȃ̐�����Փx(�f�U�[�g�Ȃ�)�@enum
    ///*/

    //���ԃI�v�V����
    ///
    /// *
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
    /// */
    /// 

    //��b���
    public static void SaveFoundation(int Startnumber = 0)
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(MusicDataName);

        string filePath = Path.Combine(Application.persistentDataPath, FoundationFileName + ".json");

        ScoreData scoreData;
        if (Startnumber == 1) ScoreStatus.DataInitialize(dataBase.musicData.Count);

        scoreData= ScoreStatus.GetScoreData();

        string json = JsonUtility.ToJson(ScoreDataSaveModel.FromScoreData(scoreData));
        File.WriteAllText(filePath, json);


    }
    //�A�`�[�u�����g�̏��
    public static void SaveAchiveMent()
    {


        string filePath = Path.Combine(Application.persistentDataPath, AchiveMentFileName + ".json");

        Achievements Data = AchievementStatus.achievements;

        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(filePath, json);


    }
    //�I�v�V�������
    public static void SaveOption(int Startnumber = 0)
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.persistentDataPath + "/" + OpstionFileName + FILR_EXTENSION, false);

        /// �Ō�ɑI��ł����X�L���@int 
        /// �m�[�c�̑����@float 
        /// �m�[�c�̔���ʒu�@float
        /// �m�[�c�ƃm�[�c�̊Ԃ̐�
        /// BGM�̉��ʁ@float
        /// SE�̉��ʁ@float
        /// �m�[�c�̌����ځ@int
        /// SE�̉��@int
        /// �I��ł����X�L���@int

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
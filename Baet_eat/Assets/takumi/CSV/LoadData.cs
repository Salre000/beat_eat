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

        string filePath = Path.Combine(Application.persistentDataPath, SaveData.FoundationFileName+".txt");
        if (File.Exists(filePath))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            ScoreStatus.SetScoreData(data);

            stream.Close();

            if (dataBase.musicData.Count > ScoreStatus.GetScoreData().dessertScore[0].Count) 
            {
                ScoreStatus.AddMusic(dataBase.musicData.Count-ScoreStatus.GetScoreData().dessertScore[0].Count);
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

        Debug.Log(filePath);
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



    }

    public static void LoadAchievements()
    {



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
        Debug.Log(csvDatas[0][1]);

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(SaveData.MusicDataName);


        List<int> list = new List<int>();
        for (int i = 0; i < dataBase.musicData.Count; i++)
        {

            list.Clear();

            for (int j = 0; j < 5; j++)
            {
                list.Add(int.Parse(csvDatas[i][j + 1]));

            }

            ScoreStatus.AddSetMusicLevel(list);

        }

    }

}
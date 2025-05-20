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

        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //�t�@�C���p�X�ƃt�@�C���̖��O���q����
        StringBuilder builder = new StringBuilder();
        builder.Clear();
        builder.Append(SaveData.FoundationFileName);

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

        int nowLine = 0;
        int musicMax = dataBase.musicData.Count;

        //�f�U�[�g�L�̃��[�v�@�X�R�A�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);

                ScoreStatus.SetDessertScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();

            nowLine++;

        }
        //�f�U�[�g�����̃��[�v�@�X�R�A�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);

                ScoreStatus.SetMainDeshScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();

            nowLine++;
        }
        //�f�U�[�g�L�̃��[�v�@�I�[�o�[�X�R�A�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertOverScore(musicID, (Difficulty)difficulty, score);


                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();

            nowLine++;
        }
        //�f�U�[�g�����̃��[�v�@�I�[�o�[�X�R�A�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<int> clearScore = new List<int>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                int score = int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshOverScore(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();
            nowLine++;
        }
        //�f�U�[�g�L�̃��[�v�@�N���A�����N�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<ClearRank> clearScore = new List<ClearRank>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearRank score = (ClearRank)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertClearRanks(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();
            nowLine++;
        }
        //�f�U�[�g�L�̃��[�v�@�N���A�����N�̋L�^
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<ClearRank> clearScore = new List<ClearRank>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearRank score = (ClearRank)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshClearRanks(musicID, (Difficulty)difficulty, score);


                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();
            nowLine++;
        }
        //�f�U�[�g�L�̃��[�v�@�N���A�󋵂̋L�^
        for (int difficulty = 0; difficulty < Difficulty.MAX.ChengeInt(); difficulty++)
        {

            List<ClearStates> clearScore = new List<ClearStates>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearStates score = (ClearStates)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetDessertDifficulty(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();
            nowLine++;
        }
        //�f�U�[�g�L�̃��[�v�@�N���A�󋵂̋L�^
        for (int difficulty = 0; difficulty < Difficulty.dessert.ChengeInt(); difficulty++)
        {

            List<ClearStates> clearScore = new List<ClearStates>(musicMax);

            for (int musicID = 0; musicID < musicMax; musicID++)
            {
                ClearStates score = (ClearStates)int.Parse(csvDatas[nowLine][musicID]);
                ScoreStatus.SetMainDeshDifficulty(musicID, (Difficulty)difficulty, score);

                clearScore.Add(score);
            }
            //�C���X�^���X���o�R���ď���n��
            clearScore.Clear();
            nowLine++;
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
        /// �Ō�ɑI��ł����X�L���@int 
        /// �m�[�c�̑����@float 
        /// �m�[�c�̔���ʒu�@float
        /// �m�[�c�ƃm�[�c�̊Ԃ̐� bool
        /// BGM�̉��ʁ@float
        /// SE�̉��ʁ@float
        /// �m�[�c�̌����ځ@int
        /// SE�̉��@int

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
                list.Add(int.Parse(csvDatas[i][j+1]));

            }

            ScoreStatus.AddSetMusicLevel(list);

        }

    }

}
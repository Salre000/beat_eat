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
    /// �m�[�c�ƃm�[�c�̊Ԃ̐�
    /// BGM�̉��ʁ@float
    /// SE�̉��ʁ@float
    /// �m�[�c�̌����ځ@int
    /// SE�̉��@int
    /// */
    /// 

    //��b���
    public static void SaveFoundation()
    {

        MusicDataBase dataBase = Resources.Load<MusicDataBase>(MusicDataName);
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FoundationFileName + FILR_EXTENSION, false);

        //����line�̔ԍ�
        int LineCount = 0;

        int musicMax = dataBase.musicData.Count;

        StringBuilder sb = new StringBuilder();

        sb.Clear();

        //�f�U�[�g����̓�Փx�̋Ȃ̃X�R�A���L�^
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append(ScoreStatus.GetDessertScore(soundNumber,(publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //�f�U�[�g�����̓�Փx�̋Ȃ̃X�R�A���L�^
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append(ScoreStatus.GetMainDeshScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //�f�U�[�g����̓�Փx�̋Ȃ̃I�[�o�[�X�R�A���L�^
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append(ScoreStatus.GetDessertOverScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //�f�U�[�g�����̓�Փx�̋Ȃ̃I�[�o�[�X�R�A���L�^
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append(ScoreStatus.GetMainDeshOverScore(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //�f�U�[�g����̓�Փx�̋Ȃ̃N���A�����N���L�^
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append((int)ScoreStatus.GetDessertClearRanks(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //�f�U�[�g�����̓�Փx�̋Ȃ̃N���A�����N���L�^
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append((int)ScoreStatus.GetMainDeshClearRanks(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }

        //�f�U�[�g����̓�Փx�̋Ȃ̃N���A�󋵂��L�^
        for (int i = 0; i < 5; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append((int)ScoreStatus.GetDessertDifficulty(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }
        //�f�U�[�g�����̓�Փx�̋Ȃ̃N���A�󋵂��L�^
        for (int i = 0; i < 4; i++)
        {
            for (int soundNumber = 0; soundNumber < musicMax; soundNumber++)
            {
                //�����ɂ��ꂼ��̃X�R�A������
                sb.Append((int)ScoreStatus.GetMainDeshDifficulty(soundNumber, (publicEnum.Difficulty)i));
                sb.Append(Spece);

            }

            sw.WriteLine(sb.ToString());
            sb.Clear();


        }





        sw.Flush();
        sw.Close();



    }
    //�I�v�V�������
    public static void SaveOption()
    {
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + OpstionFileName + FILR_EXTENSION, false);

        /// �Ō�ɑI��ł����X�L���@int 
        /// �m�[�c�̑����@float 
        /// �m�[�c�̔���ʒu�@float
        /// �m�[�c�ƃm�[�c�̊Ԃ̐�
        /// BGM�̉��ʁ@float
        /// SE�̉��ʁ@float
        /// �m�[�c�̌����ځ@int
        /// SE�̉��@int

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
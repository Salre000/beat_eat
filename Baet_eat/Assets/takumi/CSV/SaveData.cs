using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Timeline;

//�I�v�V�����f�[�^��CSV�ɕۑ�����N���X
public static class SaveData 
{
    public static string OpstionFileName = "OpsitonData";
    public static string FoundationFileName = "FoundationData";
    public static string ResultsFileName = "ResultsData";
    public static string FILE_PASS = "/Resources/";
    public static string FILR_EXTENSION = ".csv";

    //���Ԋ�b���
    ///*
    ///
    /// �X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g����)�@int�@
    /// �X�R�A�@�Ȃ̐�*��Փx(�f�U�[�g�Ȃ�)�@int�@
    /// �I�[�o�[�X�R�A�@�Ȃ̐�*��Փx�@int�@
    /// �X�R�A�����N�@�Ȃ̐�*��Փx�@enum�@
    /// �N���A�󋵁@�Ȃ̐�����Փx�@enum
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
        StreamWriter sw;

        sw = new StreamWriter(Application.dataPath + FILE_PASS + FoundationFileName + FILR_EXTENSION, false);

        //����line�̔ԍ�
        int LineCount = 0;

        //for(int soundNumber=0;soundNumber<;soundNumber++)
        //�f�U�[�g����̓�Փx�̋Ȃ̃X�R�A���L�^
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
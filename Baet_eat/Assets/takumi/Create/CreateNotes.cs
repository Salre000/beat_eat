using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateNotes : MonoBehaviour
{
    private float StartPosition = 50;

    private int NotesCount = 0;

    [Serializable]
    public class Data
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public Note[] notes;

    }
    [Serializable]
    public class Note
    {
        public int type;
        public int num;
        public int block;
        public int LPB;
        public Note[] notes;

    }
    public int noteNum;
    private string songName = "Notes/�w���A�C�h���}�X�^�[_�����w��_EndressDance";

    [SerializeField, Header("�X�L���m�[�c�̃v���n�u�̃I���W�i��")] GameObject noteObjSkill;
    [SerializeField, Header("�ʏ�m�[�c�̃v���n�u�̃I���W�i��")] GameObject noteObj;
    [SerializeField, Header("�z�[���h�m�[�c�̃v���n�u�̃I���W�i��")] GameObject noteObjLong;
    [SerializeField, Header("�t���b�N�m�[�c�̃v���n�u�̃I���W�i��")] GameObject noteObjFlick;

    void Start()
    {
        noteNum = 0;
        Load(songName);
    }

    private void Load(string SongName)
    {
        GameObject NotesParent = new GameObject(SongName);

        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            NotesCount++;

            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //�v���n�u�𕡐����Č����Ȃ��悤�ɕύX
            GameObject notes = CreateTypeNotes(inputJson.notes[i].type);

            //���ԁ@ kankaku * inputJson.notes[i].num


            notes.transform.position = new Vector3((inputJson.notes[i].block*-1) + 4.5f, 0.03f, StartPosition);
            //�e�ɓZ�߂�
            notes.transform.parent = NotesParent.transform;

            NotesBase notesBase = notes.GetComponent<NotesBase>();

            notesBase.SetShowTime(kankaku * inputJson.notes[i].num);

            if (inputJson.notes[i].type == 2)
            {
                LongNotes longNotes = notes.GetComponent<LongNotes>();

                for (int j = 0; j < inputJson.notes[i].notes.Length; j++)
                {
                    NotesCount++;
                    if (j == 0)
                    {
                        longNotes.SetDistanceNum(inputJson.notes[i].notes[j].num - inputJson.notes[i].num);
                        longNotes.SetBlock(inputJson.notes[i].notes[j].block - inputJson.notes[i].block);
                    }
                    else
                    {
                        longNotes.SetDistanceNum(inputJson.notes[i].notes[j].num - inputJson.notes[i].notes[j - 1].num);
                        longNotes.SetBlock(inputJson.notes[i].notes[j].block - inputJson.notes[i].notes[j - 1].block);
                    }
                }
            }

            //�Q�}�X�O��̏�����

            notesBase.AddLaneIndex(inputJson.notes[i].block);
            notesBase.AddLaneIndex((inputJson.notes[i].block) + 1);

            NotesUtility.AddNotes(notesBase);

            notes.SetActive(false);
        }


        InGameStatus.SetUpScore(NotesCount);
    }

    GameObject CreateTypeNotes(int type)
    {
        switch (type)
        {

            case 0: return GameObject.Instantiate(noteObjSkill);
            case 1: return GameObject.Instantiate(noteObj);
            case 2: return GameObject.Instantiate(noteObjLong);
            case 3: return GameObject.Instantiate(noteObjFlick);

        }
        return null;
    }
}

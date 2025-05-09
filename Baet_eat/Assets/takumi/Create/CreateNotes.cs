using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNotes : MonoBehaviour
{
    private readonly float startPosition = 50;

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
    }
    public int noteNum;
    private string songName = "Notes/�w���A�C�h���}�X�^�[_�����w��_EndressDance";

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField,Header("�m�[�c�̃v���n�u�̃I���W�i��")] GameObject noteObj;

    void Start()
    {
        noteNum = 0;
        Load(songName);
    }

    private void Load(string SongName)
    {
        GameObject NotesParent=new GameObject(SongName);

        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //�m�[�c�̏���Z�߂�
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);
            //�v���n�u�𕡐����Č����Ȃ��悤�ɕύX
            NotesObj.Add(Instantiate(noteObj));
            //NotesObj[i].gameObject.SetActive(false);

            //���ԁ@ kankaku * inputJson.notes[i].num


            NotesObj[i].transform.position=new Vector3((inputJson.notes[i].block * -2)+4, 0.03f, kankaku * inputJson.notes[i].num*20);
            //�e�ɓZ�߂�
            NotesObj[i].transform.parent = NotesParent.transform;

            NotesBase notesBase= NotesObj[i].GetComponent<NotesBase>();

            //�Q�}�X�O��̏�����

            notesBase.AddLaneIndex(inputJson.notes[i].block*2);
            notesBase.AddLaneIndex((inputJson.notes[i].block*2)+1);

        }
    }
}

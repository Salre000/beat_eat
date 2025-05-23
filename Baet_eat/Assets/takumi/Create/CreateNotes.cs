using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static CreateTapArea;

public class CreateNotes : MonoBehaviour
{
    private float StartPosition = 50;

    private int NotesCount = 0;
    public int GetCount() {  return NotesCount; }
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
        public int renge;
        public Note[] notes;

    }
    public int noteNum;

    private readonly string FilePass = "Notes/";

    private readonly string[] Difficulty =
    {
        "Drink/",
        "Horsd'oeuvre/",
        "Soup/",
        "MainDish/",
        "Dessert/"
    };
    private string songName = "学園アイドルマスター_初星学園_EndressDance";

    [SerializeField, Header("スキルノーツのプレハブのオリジナル")] GameObject noteObjSkill;
    [SerializeField, Header("通常ノーツのプレハブのオリジナル")] GameObject noteObj;
    [SerializeField, Header("ホールドノーツのプレハブのオリジナル")] GameObject noteObjLong;
    [SerializeField, Header("フリックノーツのプレハブのオリジナル")] GameObject noteObjFlick;


    readonly float lineRenge = 1.1f;
    [SerializeField] private GameObject noteLineObject;

    void Start()
    {
        noteNum = 0;

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(FilePass);
        stringBuilder.Append(Difficulty[(int)ScoreStatus.nowDifficulty]);

        songName=Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic].musicName;
        stringBuilder.Append(songName);

        Load(stringBuilder.ToString());


    }

    int preNum = -1;
    int preID = -1;
    List<int> preNumList = new List<int>();
    List<int> preblockList = new List<int>();
    List<int> preRengeList = new List<int>();
    
    public static float Kankaku=0;
    private void Load(string SongName)
    {
        GameObject NotesParent = new GameObject(SongName);

        NotesParent.transform.position=Vector3.zero;

        NotesParent.AddComponent<NotesMove>();

        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        SoundUtility.SetObject(NotesParent);


        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            NotesCount++;

            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);

            Kankaku=kankaku;

            float beatSec = kankaku * (float)inputJson.notes[i].LPB;

            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //プレハブを複製して見えないように変更
            GameObject notes = CreateTypeNotes(inputJson.notes[i].type);

            // 時間　 kankaku * inputJson.notes[i].num 


            notes.transform.position = new Vector3((inputJson.notes[i].block * -1) + 4.5f - (float)inputJson.notes[i].renge / 2.0f, 0.03f, kankaku * inputJson.notes[i].num * InGameStatus.GetSpeed() * 20);
            //親に纏める
            notes.transform.parent = NotesParent.transform;

            NotesBase notesBase = notes.GetComponent<NotesBase>();

            notesBase.SetRemge(inputJson.notes[i].renge);

            notesBase.SetShowTime(kankaku * inputJson.notes[i].num);

            for (int j = 0; j < inputJson.notes[i].renge + 1; j++) notesBase.AddLaneIndex(inputJson.notes[i].block + j);

            NotesUtility.AddNotes(notesBase);



            if (inputJson.notes[i].renge != 0)
                notes.transform.localScale = new Vector3(0.1f * inputJson.notes[i].renge + 0.1f, 1, 0.075f);

            notes.SetActive(false);


            if (inputJson.notes[i].type == 2)
            {
                LongNotes longNotes = notes.GetComponent<LongNotes>();
                longNotes.Setblock(inputJson.notes[i].block);
                for (int j = 0; j < inputJson.notes[i].notes.Length; j++)
                {
                    NotesCount++;
                    longNotes.SetRenges(inputJson.notes[i].notes[j].renge);
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

                    if (j == inputJson.notes[i].notes.Length - 1)
                    {

                        preblockList.Add(inputJson.notes[i].notes[j].block);
                        preNumList.Add(inputJson.notes[i].notes[j].num);
                        preRengeList.Add(inputJson.notes[i].notes[j].renge);
                    }
                }

                longNotes.Initialize();

            }




            if (preNum == inputJson.notes[i].num)
            {
                //GameObject Line = Instantiate(noteLineObject);

                //Line.transform.parent = notes.transform;


                //float x = (Mathf.Max((9 - inputJson.notes[i].block), (9 - inputJson.notes[preID].block)) - Mathf.Min((9 - inputJson.notes[i].block), (9 - inputJson.notes[preID].block)))+1- inputJson.notes[i].renge;

                //Line.transform.localScale = new Vector3(x* lineRenge, 1, 1);
                //if (inputJson.notes[i].block > inputJson.notes[preID].block) x *= -1;

                //Line.transform.localPosition = new Vector3(((float)x/2.0f),0,0);

            }

            for (int j = 0; j < preNumList.Count; j++)
            {
                if (preNumList[j] == inputJson.notes[i].num)
                {







                }

            }






            preNum = inputJson.notes[i].num;
            preID = i;


            for (int j = 0; j < preNumList.Count; j++)
            {

                if (preNumList[j] > preNum) continue;

                preNumList.RemoveAt(j);
                preblockList.RemoveAt(j);
                preRengeList.RemoveAt(j);

                j--;


            }


        }


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

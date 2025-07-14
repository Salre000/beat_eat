using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CreateTapArea;

public class CreateNotes : MonoBehaviour
{
    [SerializeField] float offset;

    private readonly Vector3 LineOffset = new Vector3(0, 0, -6.25f);

    private float StartPosition = 50;

    public static GameObject parent;

    private int NotesCount = 0;

    private float offsetReta = 50000.0f;
    [SerializeField] GameObject _area;
    public int GetCount() { return NotesCount; }
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
    private string songName = "Kokage_De_Yuttari-2(Fast)";

    [SerializeField, Header("スキルノーツのプレハブのオリジナル")] GameObject noteObjSkill;
    [SerializeField, Header("通常ノーツのプレハブのオリジナル")] GameObject noteObj;
    [SerializeField, Header("ホールドノーツのプレハブのオリジナル")] GameObject noteObjLong;
    [SerializeField, Header("フリックノーツのプレハブのオリジナル")] GameObject noteObjFlick;

    [SerializeField] GameObject notesParent;

    readonly float lineRenge = 1.1f;
    [SerializeField] private GameObject noteLineObject;

    private void Awake()
    {


    }
    public void Start()
    {
        noteNum = 0;

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(FilePass);
        stringBuilder.Append(Difficulty[(int)ScoreStatus.nowDifficulty]);

        songName = Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic].musicName;
        stringBuilder.Append(songName);
        Load(stringBuilder.ToString());
        DessertNotes(stringBuilder.ToString());
    }
    void OnEnable()
    {

    }
    public static float Kankaku = 0;
    private void Load(string SongName)
    {
        GameObject NotesParent = new GameObject(SongName);

        NotesParent.transform.parent = notesParent.transform;


        parent = NotesParent;

        NotesParent.transform.position = Vector3.zero;

        notesParent.AddComponent<NotesMove>();

        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
        SoundUtility.SetObject(notesParent);



        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].block >= 10) continue;

            NotesCount++;

            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);

            Kankaku = kankaku;

            float beatSec = kankaku * (float)inputJson.notes[i].LPB;

            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //プレハブを複製して見えないように変更
            GameObject notes = CreateTypeNotes(inputJson.notes[i].type);

            // 時間　 kankaku * inputJson.notes[i].num 

            Debug.Log((inputJson.offset / 50000.0f) * OptionStatus.GetNotesSpeed() * 20 + "FFFF");
            notes.transform.position = new Vector3((inputJson.notes[i].block) - 4.5f + (float)inputJson.notes[i].renge / 2.0f, 0.03f,
                (kankaku * inputJson.notes[i].num * OptionStatus.GetNotesSpeed() * 20)
                + (inputJson.offset / offsetReta) * OptionStatus.GetNotesSpeed() * 20) + LineOffset;
            //親に纏める
            notes.transform.parent = NotesParent.transform;

            NotesBase notesBase = notes.GetComponent<NotesBase>();

            notesBase.SetRemge(inputJson.notes[i].renge);

            notesBase.SetShowTime(kankaku * inputJson.notes[i].num);

            for (int j = 0; j < inputJson.notes[i].renge + 3; j++)
                notesBase.AddLaneIndex(inputJson.notes[i].block + j);

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
                }

                longNotes.Initialize();

            }




            LineUtility.AddActiveObject(notesBase);
            if (ScoreStatus.nowDifficulty == publicEnum.Difficulty.dessert)
            {
                DessertNotes dessert = notes.AddComponent<DessertNotes>();


                dessert.SetNotesPos(global::DessertNotes.NotesPos.grand);
                dessert.Initialize();


            }


        }



    }

    private void DessertNotes(string SongName)
    {
        if (ScoreStatus.nowDifficulty != publicEnum.Difficulty.dessert) return;

        //本来はデザートオンリーの条件が必要だがデバッグ中は無視
        if (ScoreStatus.nowDifficulty == publicEnum.Difficulty.dessert)
            DessertManager.CreateTapAreaDessert(_area);


        GameObject NotesParent = new GameObject("Dessert"+SongName);

        DessertUtility.SetNotesParent(NotesParent);
        NotesParent.transform.parent = notesParent.transform;
        NotesParent.transform.position = new Vector3(0, 2, 0);


        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);


        noteNum += inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            if (inputJson.notes[i].block < 10) continue;

            NotesCount++;

            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);

            Kankaku = kankaku;

            float beatSec = kankaku * (float)inputJson.notes[i].LPB;

            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //プレハブを複製して見えないように変更
            GameObject notes = CreateTypeNotes(inputJson.notes[i].type);


            // 時間　 kankaku * inputJson.notes[i].num 

            notes.transform.position = new Vector3(
                (inputJson.notes[i].block) - 4.5f + (float)inputJson.notes[i].renge / 2.0f, 0.03f,
                (kankaku * inputJson.notes[i].num * OptionStatus.GetNotesSpeed() * 20)
                + (inputJson.offset / offsetReta) * OptionStatus.GetNotesSpeed() * 20) + LineOffset;
            //親に纏める
            notes.transform.parent = NotesParent.transform;

            NotesBase notesBase = notes.GetComponent<NotesBase>();
            if (notesBase is FlickNotes)
            {
                notesBase.transform.name = "横フリック";

                Destroy(notesBase);

                notesBase = notes.AddComponent<SpecifiedFlickNotes>();
            }
            notesBase.SetShowTime(kankaku * inputJson.notes[i].num);

            NotesUtility.AddNotes(notesBase);



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
                }

                longNotes.Initialize();

            }

            notes.transform.localScale = new Vector3(0.2f, 1, 0.075f);

            notes.SetActive(false);



            LineUtility.AddActiveObject(notesBase);
            DessertNotes dessert = notes.AddComponent<DessertNotes>();

            if (inputJson.notes[i].block==11)
            {
                dessert.SetNotesPos(global::DessertNotes.NotesPos.right);
                notes.transform.eulerAngles += new Vector3(0, 0, 90);
                notes.transform.position = new Vector3
                    (DessertManager.GetAreaList(0).transform.up.x / 10,
                    0,
                    notes.transform.position.z) + DessertManager.GetAreaList(0).transform.position;
                dessert.Initialize();

            }
            else
            {
                notes.transform.eulerAngles += new Vector3(0, 0, -90);
                dessert.SetNotesPos(global::DessertNotes.NotesPos.left);
                notes.transform.position = new Vector3
                    (DessertManager.GetAreaList(1).transform.up.x / 10,
                     0,
                     notes.transform.position.z) + DessertManager.GetAreaList(1).transform.position;
                dessert.Initialize();

            }

            SpecifiedFlickNotes notess = notesBase as SpecifiedFlickNotes;
            notess?.Initialize(inputJson.notes[i].renge);
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

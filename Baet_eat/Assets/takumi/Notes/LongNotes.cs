using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class LongNotes : NotesBase
{
    [SerializeField] Material material;

    GameObject endNotes;

    private List<LongLongNotes> LongLongNotesList=new List<LongLongNotes>();

    private List<int> _distanceNum = new List<int>();
    public void SetDistanceNum(int distanceNum) { _distanceNum.Add(distanceNum); }
    private List<int> _block = new List<int>();
    public void SetBlock(int num) { _block.Add(num); }

    private float Allrange(List<int> list) 
    {
        int allrange = 0;
        for(int i = 0; i < list.Count; i++) 
        {
            allrange += list[i];
        }
        return allrange;
    }
    private float Range(int count,List<int> list) 
    {

        int allrange = 0;
        if(count> list.Count)count = list.Count;
        for (int i = 0; i < count; i++)
        {
            allrange += list[i];
        }
        return allrange;
    }

    public void Start()
    {
        endNotes = transform.GetChild(0).gameObject;

        endNotes.transform.position = transform.position + new Vector3((Allrange(_block))*-2, 0, Allrange(_distanceNum) * InGameStatus.GetSpeed());
        for (int j = 0; j < _distanceNum.Count; j++)
        {

            for (int i = 0; i < _distanceNum[j]; i++)
            {
                float vec = ((float)_block[j] / (float)(_distanceNum[j]));

                GameObject longLongNotes = new GameObject("LongLongNotes");

               LongLongNotes longNotes= longLongNotes.AddComponent<LongLongNotes>();


                longLongNotes.transform.parent = transform;
                longLongNotes.transform.position = this.transform.position + new Vector3(Range(j,_block)*-2, 0, (i * InGameStatus.GetSpeed()) + (Range(j, _distanceNum) *InGameStatus.GetSpeed()));


                BoxArea boxarea = new BoxArea();
                //メッシュの座標を設定
                boxarea.leftTop = new Vector3(-1 + vec * (-i*2 - 2), 0.01f, InGameStatus.GetSpeed());
                boxarea.rightTop = new Vector3(1 + vec * (-i*2 - 2), 0.01f, InGameStatus.GetSpeed());
                boxarea.bottomLeft = new Vector3(-1 + vec * -i*2, 0.01f, 0);
                boxarea.bottomRight = new Vector3(1 + vec * -i*2, 0.01f, 0);

                longNotes.SetBoxArea(boxarea);
                longNotes.Set_SetTouchID(SetTouchIDs);
                LongLongNotesList.Add(longNotes);

                //メッシュの基本設定
                Mesh mesh = new Mesh();
                mesh.vertices = VerticePosition(boxarea);

                mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };

                // 領域と法線を自動で再計算する
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                // MeshFilterに設定
                longLongNotes.AddComponent<MeshFilter>().mesh = mesh;
                longLongNotes.AddComponent<MeshRenderer>().material = material;
            }
        }
    }

    protected override void Action()
    {
        base.Action();



    }

    private bool count = false;



    public override bool CheckHitlane(int index)
    {
        if (base.CheckHitlane(index))
        {
            if (!count)
            {

                LineUtility.ShowText("start");
                //ここが一度だけの場所

                HandUtility.AddEndAction(() =>
                {
                    LineUtility.ShowText("end1");

                    count = false;

                    int endAreaID = LineUtility.GetTapArea().GetClickPositionID(HandUtility.handPosition(touchID));

                    //ノーツ用のIDに変更
                    endAreaID = 9 - endAreaID;

                    if (endAreaID < 0) return;

                    LineUtility.ShowText("end2");


                    List<int> list = new List<int>();

                    list = laneIndex;
                    for (int i = 0; i < list.Count; i++)
                    {
                        //２マス前提
                        list[i] += (int)Allrange(_block)-2;


                    }
                    JudgmentType judgmentType = (JudgmentType)(int)LineUtility.RangeToDecision(endNotes.transform.position);



                    bool flag = list.Exists(number => number == endAreaID) && judgmentType <= JudgmentType.Miss && (int)judgmentType >= -(int)JudgmentType.Miss;

                    LineUtility.ShowText(list[0].ToString() + ":" + list[1].ToString() + " :" + endAreaID.ToString());

                    if (!flag) return;

                    Hit(endNotes);
                    LineUtility.ShowText("end");

                }, touchID);



            }
            count = true;
        }

        return false;
    }

    protected override double GetDestryDecision()
    {
        return base.GetDestryDecision() - Allrange(_distanceNum);
    }

    private void SetTouchIDs(int ID) 
    {
        touchID = ID;
        for(int i = 0; i < LongLongNotesList.Count; i++) LongLongNotesList[i].SetTouchID(ID);

        HandUtility.AddEndAction(() =>
        {
            touchID = -1;
            for (int i = 0; i < LongLongNotesList.Count; i++) LongLongNotesList[i].SetTouchID(-1);

        }
        , ID);


    }

}

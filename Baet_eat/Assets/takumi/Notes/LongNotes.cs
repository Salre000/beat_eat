using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class LongNotes : NotesBase
{
    [SerializeField] Material material;

    GameObject endNotes;

    private int block;
    public void Setblock(int num) { block = num; }

    private List<LongLongNotes> LongLongNotesList = new List<LongLongNotes>();

    private List<int> _distanceNum = new List<int>();
    public void SetDistanceNum(int distanceNum) { _distanceNum.Add(distanceNum); }
    private List<int> _block = new List<int>();
    public void SetBlock(int num) { _block.Add(num); }

    private List<int> _renge = new List<int>();
    public void SetRenges(int num) { _renge.Add(num); }

    private float Allrange(List<int> list)
    {
        int allrange = 0;
        for (int i = 0; i < list.Count; i++)
        {
            allrange += list[i];
        }
        return allrange;
    }
    private float Range(int count, List<int> list)
    {

        int allrange = 0;
        if (count > list.Count) count = list.Count;
        for (int i = 0; i < count; i++)
        {
            allrange += list[i];
        }
        return allrange;
    }

    public void Start()
    {
        endNotes = transform.GetChild(0).gameObject;

        endNotes.transform.position = transform.position + new Vector3((Allrange(_block)) * -1, 0, Allrange(_distanceNum) * InGameStatus.GetSpeed());
        float sizeX = Mathf.Min(renge, _renge[_renge.Count - 1])- Mathf.Max(renge, _renge[_renge.Count - 1]);

        if (sizeX < 0) sizeX = 1.0f / (float)Mathf.Abs(sizeX);
        else sizeX += 1.0f;
        endNotes.transform.localScale = new Vector3(sizeX, 1, 1);

        float num = (Range(1, _block) + block + _renge[0]) - ((float)block + (float)renge);
        float vecRight = num / (float)_distanceNum[0];
        vecRight *= -1;
        float vecLeft = ((float)_block[0]) * -1 / (float)_distanceNum[0];

        float renges = renge + 1;
        for (int j = 0; j < _distanceNum.Count; j++)
        {

            for (int i = 0; i < _distanceNum[j]; i++)
            {

                GameObject longLongNotes = new GameObject("LongLongNotes");

                LongLongNotes longNotes = longLongNotes.AddComponent<LongLongNotes>();


                longLongNotes.transform.parent = transform;
                longLongNotes.transform.position = this.transform.position + new Vector3(0, 0, (i * InGameStatus.GetSpeed()) + (Range(j, _distanceNum) * InGameStatus.GetSpeed()));


                BoxArea boxarea = new BoxArea();
                //メッシュの座標を設定
                boxarea.leftTop = new Vector3((Range(j, _block)- 5) + vecLeft * (-i - 1), 0.01f, InGameStatus.GetSpeed());
                boxarea.bottomLeft = new Vector3((Range(j, _block) - 5) + vecLeft * -i, 0.01f, 0);

                boxarea.rightTop = new Vector3((Range(j, _block) - 5) + renges + vecRight * (-i - 1), 0.01f, InGameStatus.GetSpeed());
                boxarea.bottomRight = new Vector3((Range(j, _block) - 5) + renges + vecRight * -i, 0.01f, 0);

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

            if (j >= _distanceNum.Count - 1)
            {
                return;
            }
            num = (Range(j + 2, _block) + _renge[j + 1]) - (Range(j + 1, _block) + (float)_renge[j]);
            vecRight = num / (float)_distanceNum[j + 1];
            vecRight *= -1;
            vecLeft = ((float)_block[j + 1]) * -1 / (float)_distanceNum[j + 1];
            renges = _renge[j] + 1;

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

                HandUtility.AddEndAction(EndNotes, touchID);


                Hit(false);

            }
            count = true;
        }

        return false;
    }

    private void EndNotes()
    {
        LineUtility.ShowText("end1");

        count = false;

        int endAreaID = LineUtility.GetTapArea().GetClickPositionID(HandUtility.handPosition(touchID));

        //ノーツ用のIDに変更
        endAreaID = 9 - endAreaID;

        if (endAreaID < 0) return;

        LineUtility.ShowText("end2");


        List<int> list = new List<int>();

        for (int i = 0; i < _renge[_renge.Count - 1]; i++)
        {
            list[i] = (int)Allrange(_block) + i;


        }
        JudgmentType judgmentType = (JudgmentType)(int)LineUtility.RangeToDecision(endNotes.transform.position);



        bool flag = list.Exists(number => number == endAreaID) && judgmentType <= JudgmentType.Miss && (int)judgmentType >= -(int)JudgmentType.Miss;

        LineUtility.ShowText(list[0].ToString() + ":" + list[1].ToString() + " :" + endAreaID.ToString());

        if (!flag) return;

        Hit(endNotes);
        LineUtility.ShowText("end");

    }

    protected override double GetDestryDecision()
    {
        return base.GetDestryDecision() - Allrange(_distanceNum);
    }

    private void SetTouchIDs(int ID)
    {
        touchID = ID;
        for (int i = 0; i < LongLongNotesList.Count; i++) LongLongNotesList[i].SetTouchID(ID);
        HandUtility.AddEndAction(EndNotes, touchID);


        HandUtility.AddEndAction(() =>
        {
            touchID = -1;
            for (int i = 0; i < LongLongNotesList.Count; i++) LongLongNotesList[i].SetTouchID(-1);

        }
        , ID);


    }

}

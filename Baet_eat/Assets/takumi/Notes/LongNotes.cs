using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class LongNotes : NotesBase
{
    [SerializeField] Material material;

    GameObject endNotes;

    private int _distanceNum = 5;
    public void SetDistanceNum(int distanceNum) { _distanceNum = distanceNum; }
    private int _block = 1;
    public void SetBlock(int num) { _block = num; }



    public void Start()
    {
        endNotes = transform.GetChild(0).gameObject;

        endNotes.transform.position = transform.position + new Vector3((_block *-1), 0, _distanceNum * InGameStatus.GetSpeed());

        for (int i = 0; i < _distanceNum; i++)
        {
            float vec = ((float)_block / (float)(_distanceNum));

            GameObject longLongNotes = new GameObject("LongLongNotes");


            longLongNotes.transform.parent = transform;
            longLongNotes.transform.position = this.transform.position + new Vector3(0, 0, i * InGameStatus.GetSpeed());


            BoxArea boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(-1 + vec * (-i - 1), 0.01f, InGameStatus.GetSpeed());
            boxarea.rightTop = new Vector3(1 + vec * (-i - 1), 0.01f, InGameStatus.GetSpeed());
            boxarea.bottomLeft = new Vector3(-1 + vec * -i, 0.01f, 0);
            boxarea.bottomRight = new Vector3(1 + vec * -i, 0.01f, 0);

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
                    endAreaID = 9-endAreaID;

                    if (endAreaID < 0) return;

                    LineUtility.ShowText("end2");


                    List<int> list = new List<int>();

                    list = laneIndex;
                    for (int i = 0; i < list.Count; i++)
                    {
                        //２マス前提
                        list[i] += _block;


                    }
                    JudgmentType judgmentType = (JudgmentType)(int)LineUtility.RangeToDecision(endNotes.transform.position);



                    bool flag = list.Exists(number => number == endAreaID) && judgmentType <= JudgmentType.Miss && (int)judgmentType >= -(int)JudgmentType.Miss;

                    LineUtility.ShowText(list[0].ToString()+":"+ list[1].ToString()+" :" + endAreaID.ToString());

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
        return base.GetDestryDecision() - _distanceNum;
    }

}

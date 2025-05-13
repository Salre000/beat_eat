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
            //���b�V���̍��W��ݒ�
            boxarea.leftTop = new Vector3(-1 + vec * (-i - 1), 0.01f, InGameStatus.GetSpeed());
            boxarea.rightTop = new Vector3(1 + vec * (-i - 1), 0.01f, InGameStatus.GetSpeed());
            boxarea.bottomLeft = new Vector3(-1 + vec * -i, 0.01f, 0);
            boxarea.bottomRight = new Vector3(1 + vec * -i, 0.01f, 0);

            //���b�V���̊�{�ݒ�
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);

            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };

            // �̈�Ɩ@���������ōČv�Z����
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            // MeshFilter�ɐݒ�
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
                //��������x�����̏ꏊ

                HandUtility.AddEndAction(() =>
                {
                    LineUtility.ShowText("end1");

                    count = false;

                    int endAreaID = LineUtility.GetTapArea().GetClickPositionID(HandUtility.handPosition(touchID));

                    //�m�[�c�p��ID�ɕύX
                    endAreaID = 9-endAreaID;

                    if (endAreaID < 0) return;

                    LineUtility.ShowText("end2");


                    List<int> list = new List<int>();

                    list = laneIndex;
                    for (int i = 0; i < list.Count; i++)
                    {
                        //�Q�}�X�O��
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

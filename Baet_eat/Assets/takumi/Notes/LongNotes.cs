using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class LongNotes : NotesBase 
{
    [SerializeField]Material material;

    GameObject endNotes;

    private int _distanceNum=5;
    public void SetDistanceNum(int distanceNum) { _distanceNum = distanceNum; }
    private int _block=-1;
    public void SetBlock(int num) { _block = num; }
    
    

    public void Start()
    {
        endNotes=transform.GetChild(0).gameObject;

        endNotes.transform.position = transform.position +new Vector3((_block * -2), 0, _distanceNum* InGameStatus.GetSpeed());

        for(int i = 0; i < _distanceNum; i++) 
        {
            float vec = ((float)_block / (float)(_distanceNum));

            GameObject longLongNotes=new GameObject("LongLongNotes");


            longLongNotes.transform.parent = transform;
            longLongNotes.transform.position=this.transform.position+new Vector3(0, 0, i*InGameStatus.GetSpeed());


            BoxArea boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(-1+ vec*(-i * 2 - 2), 0.01f, InGameStatus.GetSpeed());
            boxarea.rightTop = new Vector3(1+ vec*(-i * 2 - 2), 0.01f, InGameStatus.GetSpeed());
            boxarea.bottomLeft = new Vector3(-1+ vec*-i * 2, 0.01f, 0);
            boxarea.bottomRight = new Vector3(1 + vec*-i*2, 0.01f,0);

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

    public override bool CheckHitlane(int index)
    {
        return base.CheckHitlane(index);
    }

    protected override double GetDestryDecision()
    {
        return base.GetDestryDecision()- _distanceNum;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class CreateTapArea
{
    private float offset = -7;
    private float areaRange = 0.075f*10;
    private float wide = 10;

    private List<MeshRenderer> tapPoint = new List<MeshRenderer>();
    private List<BoxArea> tapPosition = new List<BoxArea>();
    private List<float> timeCount = new List<float>();

    private readonly float MaxTime = 1.7f;
    private Material normal;
    private Material click;

    public struct BoxArea
    {
        public Vector3 leftTop, rightTop, bottomRight, bottomLeft;
    }
    public static Vector3[] VerticePosition(BoxArea boxarea)
    {
        Vector3[] vector3s = new Vector3[]
        { boxarea.leftTop, boxarea.rightTop,
           boxarea.bottomRight,boxarea.bottomLeft };


        return vector3s;
    }

    public void GetClickPoint(Vector2 clickPoint, System.Action<int> action)
    {
        for (int i = 0; i < tapPoint.Count; i++)
        {
            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //周りの方向ベクトルを取得
                vecs[j] = (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[(j + 1) % 4]) - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
            }

            bool flag = false;
            for (int j = 0; j < 4; j++)
            {
                //クリックした方向ベクトルを取得
                Vector2 vec = clickPoint - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
                //外積を取得
                Vector3 dont = Vector3.Cross(vecs[j], vec);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue;

            //範囲内をクリックしたと認める

            action(i);
            timeCount[i] = 1;
            tapPoint[i].material = click;

            return;
        }


    }

    //クリックされた後に時間が経ったら色を戻る関数
    public void CheckTime()
    {
        for (int i = 0; i < timeCount.Count; i++)
        {
            if (timeCount[i] < 1) continue;
            timeCount[i] += Time.deltaTime;
            if (timeCount[i] < MaxTime) continue;

            timeCount[i] = 0;
            tapPoint[i].material = normal;
        }

    }

    public void CreateMesh(int divisionCount)
    {
        float wideDivision = wide / (divisionCount + 1);
        GameObject tapParent = new GameObject("TapObject");

        for (int i = 1; i < divisionCount + 2; i++)
        {


            BoxArea boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset + areaRange);
            boxarea.rightTop = new Vector3(wideDivision * i - wide / 2, 0.01f, offset + areaRange);
            boxarea.bottomLeft = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset);
            boxarea.bottomRight = new Vector3(wideDivision * i - wide / 2, 0.01f, offset);

            //メッシュの基本設定
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };


            // 領域と法線を自動で再計算する
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GameObject go = new GameObject();

            // MeshFilterに設定
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = normal;

            tapPosition.Add(boxarea);
            tapPoint.Add(go.GetComponent<MeshRenderer>());
            timeCount.Add(0.0f);

            go.transform.parent=tapParent.transform;
        }


        //デバッグ用
        GameObject judgmentLine = new GameObject("judgment");
        LineRenderer line = judgmentLine.AddComponent<LineRenderer>();
        line.positionCount = 2;

        line.material = new Material(normal);
        line.material.color = Color.red;

        line.SetPosition(0, new Vector3(wide / 2, 0.02f, offset + areaRange / 2));
        line.SetPosition(1, new Vector3(-wide/2, 0.02f, offset+areaRange/2));
        line.startWidth = line.endWidth = 0.1f;



    }

    public void SetMaterial(Material normal, Material click)
    {
        this.normal = normal;
        this.click = click;
    }

}
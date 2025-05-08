using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class CreateTapArea
{
    private float offset = -7;
    private float areaRange = 1.5f;
    private float wide = 10;

    private List<MeshRenderer> tapPoint = new List<MeshRenderer>();
    private List<BoxArea> tapPosition = new List<BoxArea>();

    private Material normal;
    private Material click;

    public struct BoxArea
    {
        public Vector3 leftTop, rightTop, bottomRight, bottomLeft;



    }

    private Vector3[] VerticePosition(BoxArea boxarea)
    {
        Vector3[] vector3s = new Vector3[]
        { boxarea.leftTop, boxarea.rightTop,
           boxarea.bottomRight,boxarea.bottomLeft };


        return vector3s;
    }
    public void GetClickPoint(Vector2 clickPoint)
    {
        for (int i = 0; i < tapPoint.Count; i++)
        {
            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //����̕����x�N�g�����擾
                vecs[j] = (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[(j+1)%4]) - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
            }

            bool flag = false;  
            for (int j = 0; j < 4; j++)
            {
                //�N���b�N���������x�N�g�����擾
                Vector2 vec = clickPoint - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
                //�O�ς��擾
                Vector3 dont = Vector3.Cross(vecs[j],vec);
                Debug.Log(dont+"Count"+i);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue; 

            //�͈͓����N���b�N�����ƔF�߂�


            tapPoint[i].material = click;

            return;
        }


    }

    public void CreateMesh(int divisionCount)
    {
        float wideDivision = wide / (divisionCount+1);

        for (int i = 1; i < divisionCount + 2; i++)
        {


            BoxArea boxarea = new BoxArea();
            //���b�V���̍��W��ݒ�
            boxarea.leftTop = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset + areaRange);
            boxarea.rightTop = new Vector3(wideDivision * i - wide / 2, 0.01f, offset + areaRange);
            boxarea.bottomLeft = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset);
            boxarea.bottomRight = new Vector3(wideDivision * i - wide / 2, 0.01f, offset);

            //���b�V���̊�{�ݒ�
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };


            // �̈�Ɩ@���������ōČv�Z����
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GameObject go = new GameObject();

            // MeshFilter�ɐݒ�
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = normal;

            tapPosition.Add(boxarea);
            tapPoint.Add(go.GetComponent<MeshRenderer>());
        }
    }

    public void SetMaterial(Material normal, Material click)
    {
        this.normal = normal;
        this.click = click;
    }

}
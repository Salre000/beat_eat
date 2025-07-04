using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CreateTapArea;
using static UnityEngine.GUILayout;

public class CreateTapArea
{
    public const float offset = -6.25f;
    public const float areaRange = 0.4f;
    public const float wide = 10;

    private List<MeshRenderer> tapPoint = new List<MeshRenderer>();
    private List<BoxArea> tapPosition = new List<BoxArea>();
    private List<float> timeCount = new List<float>();

    public const float MaxTime = 1.7f;
    private Material normal;
    private Material click;

    public static TextMeshProUGUI textMeshProUGUI;

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

    public int GetClickPositionID(Vector2 clickPosition)
    {
        for (int i = 0; i < tapPoint.Count; i++)
        {
            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //����̕����x�N�g�����擾
                vecs[j] = (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[(j + 1) % 4]) - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
            }

            bool flag = false;
            for (int j = 0; j < 4; j++)
            {
                //�N���b�N���������x�N�g�����擾
                Vector2 vec = clickPosition - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
                //�O�ς��擾
                Vector3 dont = Vector3.Cross(vecs[j], vec);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue;

            //�͈͓����N���b�N�����ƔF�߂�
            return i;
        }

        return -1;

    }

    public void GetClickPoint(Vector2 clickPoint, System.Action<int, int> action, int id)
    {

        for (int i = 0; i < tapPoint.Count; i++)
        {

            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //����̕����x�N�g�����擾
                vecs[j] = TapAreaPoint(i,j);
            }
            bool flag = false;
            for (int j = 0; j < 4; j++)
            {
                //�N���b�N���������x�N�g�����擾
                Vector2 vec = clickPoint - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);
                //�O�ς��擾
                Vector3 dont = Vector3.Cross(vecs[j], vec);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue;
            tapPoint[i].material = click;
            timeCount[i] = 1;

            //�͈͓����N���b�N�����ƔF�߂�
            action(i, id);

            return;
        }

    }

    private Vector2 TapAreaPoint(int i,int j) 
    {
        return (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[(j + 1) % 4]) - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(tapPosition[i])[j]);

    }

    //�N���b�N���ꂽ��Ɏ��Ԃ��o������F��߂�֐�
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
            //���b�V���̍��W��ݒ�
            boxarea.leftTop = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset + areaRange);
            boxarea.rightTop = new Vector3(wideDivision * i - wide / 2, 0.01f, offset + areaRange);
            boxarea.bottomLeft = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset - areaRange);
            boxarea.bottomRight = new Vector3(wideDivision * i - wide / 2, 0.01f, offset - areaRange);

            //���b�V���̊�{�ݒ�
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            boxarea.bottomLeft.z -= 2;
            boxarea.bottomRight.z -= 2;
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };

            boxarea.leftTop += new Vector3(0, 1.5f, 0);
            boxarea.rightTop += new Vector3(0, 1.5f, 0);

            // �̈�Ɩ@���������ōČv�Z����
            // �̈�Ɩ@���������ōČv�Z����
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            GameObject go = new GameObject();

            // MeshFilter�ɐݒ�
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = normal;

            tapPosition.Add(boxarea);
            tapPoint.Add(go.GetComponent<MeshRenderer>());
            timeCount.Add(0.0f);

            go.transform.parent = tapParent.transform;
        }

        DessertManager.click = click;
        DessertManager.normal = normal;


        //�f�o�b�O�p
        GameObject judgmentLine = new GameObject("judgment");
        LineRenderer line = judgmentLine.AddComponent<LineRenderer>();
        line.positionCount = 2;

        line.material = new Material(normal);
        line.material.color = Color.red;

        line.SetPosition(0, new Vector3(wide / 2, 0.02f, offset + (OptionStatus.GetNotesHitLinePos() * 0.5f)));
        line.SetPosition(1, new Vector3(-wide / 2, 0.02f, offset + (OptionStatus.GetNotesHitLinePos() * 0.5f)));
        line.startWidth = line.endWidth = 0.1f;



    }

    public void SetMaterial(Material normal, Material click)
    {
        this.normal = normal;
        this.click = click;
    }

}
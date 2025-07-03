using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static CreateTapArea;
public class CreateDessertGame : MonoBehaviour
{
    public const float TAP_AREA_DESSERT = -4.0f;

    private const float dessertOffSet = 0;

    private static List<float> timeCount = new List<float>();
    private static List<MeshRenderer> tapPoint = new List<MeshRenderer>();
    [SerializeField] private static List<BoxArea> box = new List<BoxArea>();

    private static List<GameObject> areaList = new List<GameObject>();

    public static Material normal;
    public static Material click;
    public static void CreateTapAreaDessert()
    {
        areaList.Clear();
        box.Clear();
        timeCount.Clear();
        tapPoint.Clear();


        GameObject area = GameObject.Find("Area");

        for (int i = -1; i < 2; i += 2)
        {
            timeCount.Add(0);

            GameObject areaCopy = GameObject.Instantiate(area);

            areaList.Add(areaCopy);
            //����Ȃ�������j��
            Destroy(areaCopy.transform.GetChild(0).gameObject);


            areaCopy.transform.position = new Vector3(6 * i, 2, 15);
            areaCopy.transform.eulerAngles = new Vector3(0, 0, 50 * i);
            areaCopy.transform.localScale = new Vector3(0.2f, 1, 8);


            BoxArea boxarea = new BoxArea();
            //���b�V���̍��W��ݒ�
            boxarea.leftTop = new Vector3(-3, 0, areaRange);
            boxarea.rightTop = new Vector3(3, 0, areaRange);
            boxarea.bottomLeft = new Vector3(-3, 0, -areaRange);
            boxarea.bottomRight = new Vector3(3, 0, -areaRange);

            GameObject go = new GameObject("�f�U�[�g�^�b�v�G���A");
            //�����̒l������̈ʒu

            go.transform.parent = (areaCopy.transform);
            go.transform.Rotate(0, 0, i * 50);
            go.transform.localPosition = Vector3.zero;
            go.transform.position += (go.transform.up / 100f) + new Vector3(0, 0, -go.transform.position.z + TAP_AREA_DESSERT);

            //���b�V���̊�{�ݒ�
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };


            // �̈�Ɩ@���������ōČv�Z����
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();


            // MeshFilter�ɐݒ�
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = normal;

            tapPoint.Add(go.GetComponent<MeshRenderer>());

            MeshFilter meshFilter = go.GetComponent<MeshFilter>();


            BoxArea box1 = new BoxArea();

            box1.leftTop = -go.transform.right + go.transform.position + go.transform.forward;
            box1.rightTop = go.transform.right + go.transform.position + go.transform.forward;
            box1.bottomLeft = -go.transform.right + go.transform.position - go.transform.forward;
            box1.bottomRight = go.transform.right + go.transform.position - go.transform.forward;


            box.Add(box1);


        }


        area.AddComponent<CreateDessertGame>();

    }

    public void GetClickPoint(Vector2 clickPoint,int id)
    {

        for (int i = 0; i < tapPoint.Count; i++)
        {

            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //����̕����x�N�g�����擾
                vecs[j] = TapAreaPoint(i, j);
            }
            bool flag = false;
            for (int j = 0; j < 4; j++)
            {
                //�N���b�N���������x�N�g�����擾
                Vector2 vec = clickPoint - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[i])[j]);
                //�O�ς��擾
                Vector3 dont = Vector3.Cross(vecs[j], vec);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue;
            tapPoint[i].material = click;
            timeCount[i] = 1;
            Debug.Log("number" + i);
            //�͈͓����N���b�N�����ƔF�߂�
            DessertUtility.Click(0,id);

            return;

        }

    }
    private Vector2 TapAreaPoint(int i, int j)
    {
        return (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[i])[(j + 1) % 4])
            - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[i])[j]);

    }


    public void Start()
    {
        DessertUtility.dessertGame = this;
    }

    public void CheckClick()
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
    private void FixedUpdate()
    {
        CheckClick();
    }

}

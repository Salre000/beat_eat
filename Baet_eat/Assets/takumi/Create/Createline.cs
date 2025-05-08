using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createline : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    [SerializeField, Header("ラインの分割数")] int _divisionCount = 12;

    private readonly int length = 40;

    private readonly int wide = 10;

    //タップをする位置を生成するクラス
    private CreateTapArea _tapArea;

    void Awake()
    {
        _tapArea=new CreateTapArea();

        float wideDivision = wide / (float)(_divisionCount + 1);
        _tapArea.CreateMesh(_divisionCount,lineMaterial);
        for (int i = 0; i < _divisionCount + 2; i++)
        {
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.parent = transform;

            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.startWidth = line.endWidth = 0.1f;

            //���_����ύX
            line.positionCount = 2;

            line.material = lineMaterial;

            Vector3 pos = this.transform.position;

            pos.x = wide / 2 - wideDivision * i;


            pos.z -= length;
            line.SetPosition(0, pos);
            pos.z += length * 2;
            line.SetPosition(1, pos);



        }
    }
}

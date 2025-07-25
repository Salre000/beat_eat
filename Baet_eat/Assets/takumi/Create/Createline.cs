﻿using UnityEngine;
using static CreateTapArea;
public class Createline
{
    private readonly int length = 40;


    public void SetLine(int _divisionCount,GameObject gameObject,Material material )
    {

        float wideDivision = wide / (float)(_divisionCount + 1);
        for (int i = 0; i < _divisionCount + 2; i++)
        {
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.parent = gameObject.transform;

            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.startWidth =0.05f;
            line.endWidth = 0.1f;
           //���_����ύX
           line.positionCount = 2;

            line.material = material;

            Vector3 pos = gameObject.transform.position+new Vector3(0,0.02f,0.05f);

            pos.x = wide / 2 - wideDivision * i;


            pos.z -= length;
            line.SetPosition(0, pos);
            pos.z += length * 2;
            line.SetPosition(1, pos);



        }
    }
}

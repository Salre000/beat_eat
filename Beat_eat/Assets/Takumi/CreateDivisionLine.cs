using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDivisionLine : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
    //ü‚Ì”
    [SerializeField,Header("•ªŠ„”")]int _divisionCount = 12;

    //  ƒ‰ƒCƒ“‚Ì’·‚³‚Ì’è”
    private readonly int length = 40;

    private readonly int wide = 10;



    void Start()
    {

        float wideDivision = wide / (float)(_divisionCount + 1);

        for (int i =0; i < _divisionCount + 2; i++) 
        {
            GameObject lineObject = new GameObject("Line");
            lineObject.transform.parent = transform;

            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.startWidth = line.endWidth = 0.1f;

            //’¸“_”‚ğ•ÏX
            line.positionCount = 2;

            line.material = lineMaterial;

            Vector3 pos=this.transform.position;

            pos.x =wide/2- wideDivision*i;


            pos.z -= length;
            line.SetPosition(0, pos);
            pos.z += length * 2;
            line.SetPosition(1, pos);



        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTapArea
{
    public struct BoxArea
    {
        public Vector3 leftTop, rightTop, bottomLeft, bottomRight;



    }

    private Vector3[] VerticePosition(BoxArea boxarea)
    {
        Vector3[] vector3s = new Vector3[]
        { boxarea.leftTop, boxarea.rightTop, 
          boxarea.bottomLeft, boxarea.bottomRight };


        return vector3s;
    }

    public void CreateMesh(int divisionCount,Material material  )
    {
        BoxArea boxarea = new BoxArea();
        boxarea.leftTop = new Vector3(100, 100, 0);
        boxarea.rightTop=new Vector3(100,0,0);
        boxarea.bottomLeft=new Vector3(0,100,0);
        boxarea.bottomRight=Vector3.zero;
        Mesh mesh = new Mesh();
        mesh.vertices = VerticePosition(boxarea);
        mesh.triangles = new[] { 0, 1, 2, 2, 1, 3 };


        // óÃàÊÇ∆ñ@ê¸Çé©ìÆÇ≈çƒåvéZÇ∑ÇÈ
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GameObject go = new GameObject();

        // MeshFilterÇ…ê›íË
        go.AddComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>().material=material;

    }

}
using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;

public class CreateLineFlash
{
    private List<MeshRenderer> flashLine = new List<MeshRenderer>();
    private List<Material> flashMaterial = new List<Material>();

    private Material material;
    private readonly float offset = -7.5f;
    private readonly float range = 60.0f;
    private float wide = 10;
    public void SetMaterial(Material material) { this.material = material; }
    public void SetFlashLine(int divisionCount)
    {
        float wideDivision = wide / (divisionCount + 1);
        GameObject ParentObject = new GameObject("FlashLine");
        for (int i = 1; i < divisionCount + 2; i++)
        {

            BoxArea boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(wideDivision * (i - 1) - wide / 2, 0.01f, offset+ range);
            boxarea.rightTop = new Vector3(wideDivision * i - wide / 2, 0.01f, offset+ range);
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
            flashMaterial.Add(new Material(material));
            // MeshFilterに設定
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = flashMaterial[i-1];

            go.transform.parent = ParentObject.transform;
            flashLine.Add(go.GetComponent<MeshRenderer>());
        }


    }
    public void SbuAlpha() 
    {
        for(int i=0;i< flashLine.Count; i++) 
        {
            Color color= flashMaterial[i].color;

            if (color.a <= 0) continue;

            color.a -= 0.01f;
            flashMaterial[i].color = color;
        }
    }
    public void AddAlpha(int index) 
    {
        Color color = flashMaterial[index].color;

        color.a = 0.2f;
        flashMaterial[index].color = color;
    }
}
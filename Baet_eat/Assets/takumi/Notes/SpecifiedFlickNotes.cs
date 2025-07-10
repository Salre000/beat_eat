using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecifiedFlickNotes : FlickNotes
{
    GameObject[] FlickUps = new GameObject[3];
    Vector3[] StartPoss = new Vector3[3];
    MeshRenderer[] meshs = new MeshRenderer[3];
    float []alphas= {0.5f,0.5f,0.5f };
    bool leftFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        renge = 15;
    }
    protected override void Action()
    {
        FlickImageMove();
        FlickDecision();
    }


    public void Initialize() 
    {
        GameObject.Instantiate(this.transform.GetChild(0).gameObject, transform);
        GameObject.Instantiate(this.transform.GetChild(0).gameObject, transform);


        int _i = 1;
        if (GetComponent<DessertNotes>().GetNotesPos() == DessertNotes.NotesPos.left)
        {
            _i = -1;
            leftFlag = true;

        }

        for (int i = 0; i < 3; i++)
        {
            FlickUps[i] = this.transform.GetChild(i == 0 ? i : i + 1).gameObject;
            FlickUps[i].transform.eulerAngles += new Vector3(90 * _i, 90, 90 * _i);
            FlickUps[i].transform.localScale = new Vector3(0.05f, 1, 0.3f);
            FlickUps[i].transform.localPosition -= (-FlickUps[i].transform.right) * (i * 2*_i);

            StartPoss[i] = FlickUps[i].transform.localPosition;
            meshs[i] = FlickUps[i].GetComponent<MeshRenderer>();
            meshs[i].material = new Material(meshs[i].material);
        }

        Debug.Log(-FlickUps[0].transform.right+"進行方向");

        NotesType = 3;

    }
    public override void FlickDecision()
    {

        if (!count) return;


        if (Vector2.Distance(flickStartPos, HandUtility.handPosition(touchID)) < renge) return;
        //追加で方向指定

        //方向ベクトルを取得
        Vector2 Vec = HandUtility.handPosition(touchID) - flickStartPos;

        Vec.Normalize();

        Vector2 targetAngle = new Vector2(0, -FlickUps[0].transform.right.x);

        //角度の比較をする

        if (Vector2.Angle(Vec, targetAngle) > 90) return;

        Debug.Log("角度" + Vector2.Angle(Vec, targetAngle));

        base.Hit();


    }
    public override void FlickImageMove()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = FlickUps[i].transform.localPosition;

            pos += (-FlickUps[i].transform.right*(leftFlag?-1:1))/10.0f;

            FlickUps[i].transform.localPosition = pos;

            Color color = meshs[i].material.color;

            color.a += alphas[i];

            meshs[i].material.color = color;

            if (color.a >= 1)
            {
                alphas[i] = -0.05f;
            }
            else if (color.a <= 0)
            {

                alphas[i] = 0.05f;

                FlickUps[i].transform.localPosition = StartPoss[i];

            }

        }
    }
    public override void Hit()
    {
        if (count) return;
        flickStartPos = HandUtility.handPosition(touchID);
        count = true;

    }
}

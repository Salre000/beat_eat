using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickNotes : NotesBase
{
    GameObject FilickUp;

    MeshRenderer MeshRenderer;

    private float startPos = 0;
    private float alpha = 0.01f;
    private float speed = 0.01f;
    public void Start()
    {
        FilickUp = this.transform.GetChild(0).gameObject;
        startPos = FilickUp.transform.position.y;
        MeshRenderer = FilickUp.GetComponent<MeshRenderer>();

        MeshRenderer.material = new Material(MeshRenderer.material);
    }

    protected override void Action()
    {
        base.Action();

        Vector3 pos = FilickUp.transform.position;

        pos.y += speed;

        FilickUp.transform.position = pos;

        Color color = MeshRenderer.material.color;

        color.a += alpha;

        MeshRenderer.material.color = color;

        if (color.a >= 1)
        {
            alpha = -0.05f;
        }
        else if (color.a <= 0)
        {

            alpha = 0.05f;

            pos = FilickUp.transform.position;

            pos.y = startPos;

            FilickUp.transform.position = pos;


        }


    }
}

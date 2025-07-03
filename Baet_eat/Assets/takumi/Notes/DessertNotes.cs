using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DessertNotes : MonoBehaviour
{
    public enum NotesPos 
    {
        grand,
        left,
        right,
        max
    }

    private NotesPos notesPos;
    public void SetNotesPos(NotesPos notes) {  notesPos = notes; }
    public NotesPos GetNotesPos() { return notesPos; }

    public static float t = 1;

    private Vector3 StartPos=Vector3.zero;
    private Vector3 EndPos=Vector3.zero;
    public void Awake()
    {
        StartPos=EndPos = transform.position;
        DessertUtility.AddAllNotes(this);
    }

    public void OnEnable()
    {
        DessertUtility.AddActiveNotes(this);

    }
    void OnDisable()
    {
        DessertUtility.SbuActiveNotes(this);
    }
    public void FixedUpdate()
    {
        t += Time.deltaTime;
        if (t > 1) return;
        transform.position = Vector3.Slerp(StartPos,EndPos,t);
    }

    public void Chenge()
    {
        //現在位置をスタートポイントに決定
        StartPos = transform.position;

        //移動を決定
        EndPos =new Vector3(DessertUtility.GetSideVec(this).x, transform.position.y,transform.position.z );

    }
    public void Chenge(Vector3 vector)
    {
        //瞬間的に移動させる
        transform.position= vector;

    }


}

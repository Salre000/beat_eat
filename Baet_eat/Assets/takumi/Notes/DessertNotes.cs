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
    }
}

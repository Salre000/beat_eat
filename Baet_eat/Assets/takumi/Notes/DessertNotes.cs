using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static NotesBase;
public class DessertNotes : MonoBehaviour
{
    public enum NotesPos 
    {
        right,
        left,
        grand,
        max
    }

    private NotesPos notesPos;
    public void SetNotesPos(NotesPos notes) {  notesPos = notes; }
    public NotesPos GetNotesPos() { return notesPos; }

    private NotesBase notesBase;
    public static float t = 1;

    private Vector3 StartPos=Vector3.zero;
    private Vector3 EndPos=Vector3.zero;
    public void Awake()
    {
        DessertUtility.AddAllNotes(this);
        notesBase=GetComponent<NotesBase>();

        notesBase.SetEndPos(-4.0f);
    }

    public void OnEnable()
    {
        DessertUtility.AddActiveNotes(this);

    }
    void OnDisable()
    {
        DessertUtility.SbuActiveNotes(this);
    }

    public void Hit() 
    {
        notesBase.Hit();
    }
    public  void SetTouchID(int id) 
    {
        notesBase.SetTouchID(id);
    }
    public  bool CheckHitlane(int index)
    {
        JudgmentType judgmentType = (JudgmentType)((int)LineUtility.RangeToDecision(this.transform.position,-4f));
        Debug.Log("‹——£" + judgmentType);
        bool flag = ((int)notesPos == index) && judgmentType <= JudgmentType.Good && (int)judgmentType >= -(int)JudgmentType.Good;
        return flag;
    }
}

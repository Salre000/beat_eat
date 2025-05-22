using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{



    private List<NotesBase> AllNotes=new List<NotesBase>();
    public void AddNotes(NotesBase notesBase) {  AllNotes.Add(notesBase); }


    public void Awake()
    {
        NotesUtility.notesManager = this;
    }

    public void FixedUpdate()
    {
        ShowNotes();

        List<HandManager.Hands> hands = HandUtility.GetHands();

        for(int i = 0; i < hands.Count; i++) 
        {
            if (!hands[i].flag) continue;

            LineUtility.GetTapArea().GetClickPoint(hands[i].HandPosition, Click,i);


        }

    }

    public void ShowNotes() 
    {
        for(int i = 0; i < AllNotes.Count; i++) 
        {
            if (AllNotes[i].GetShowTime() > SoundUtility.GetNowTime() || AllNotes[i].gameObject.activeSelf || AllNotes[i].GetShowTime()<0) continue;

            //ƒm[ƒc‚Ì•`‰æ‚ð‚·‚é
            AllNotes[i].gameObject.SetActive(true);


        }


    }

    public void Click(int index, int id) 
    {
        LineUtility.Click(index, id);
    }


}

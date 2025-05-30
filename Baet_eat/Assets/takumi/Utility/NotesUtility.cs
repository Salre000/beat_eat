using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesUtility
{
    public static NotesManager notesManager { set; private get; }

    public static void AddNotes(NotesBase notesBase) { notesManager.AddNotes(notesBase); }
    public static List<NotesBase> GetNotes() { return notesManager.GetALLNotes(); }


}
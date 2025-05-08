using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineUtility
{
    
    public static InGameManager gameManager{ set; private get; } 

    public static float RangeToDecision(Vector3 pos) { return gameManager.RangeToDecision(pos); }

    public static void AddActiveObject(NotesBase gameObject) { gameManager.AddActiveObject(gameObject); }
    public static void SbuActiveObject(NotesBase gameObject) { gameManager.SbuActiveObject(gameObject); }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LineUtility
{
    
    public static InGameManager gameManager{ set; private get; } 

    //‚±‚±‚Ì‚©‚¯‚½’l‚ª¬‚³‚¢’ö@”»’è‚Ì‘å‚«‚³‚ª‘å‚«‚­‚È‚é
    public static float RangeToDecision(Vector3 pos) { return gameManager.RangeToDecision(pos)*2f; }

    public static void AddActiveObject(NotesBase gameObject) { gameManager.AddActiveObject(gameObject); }
    public static void SbuActiveObject(NotesBase gameObject) { gameManager.SbuActiveObject(gameObject); }

    public static List<NotesBase> GetNotesBases() { return gameManager.GetActiveObject(); }
    public static CreateTapArea GetTapArea() { return gameManager.GetTapArea(); }

    public static void Click(int index, int id) {  gameManager.Click(index,id); }

    public static void ShowText(string text) {  gameManager.ShowText(text); }

    public static void AddCount() {  gameManager.AddCount(); }

    public static void SetScoreGage() {  gameManager.SetScore(); }
    public static Material GetInbisible() {return  gameManager.Getinvisible(); }
    public static Material GetNOtInbisible() { return gameManager.GetNoeInvisible(); }

}
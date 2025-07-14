using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DessertUtility
{

    public static DessertManager dessertGame;

    public static void ReSet() 
    {
    }

    public static void CheckSideTap(Vector2 vector2, int id)
    {
        if (dessertGame == null) return;

        dessertGame.GetClickPoint(vector2, id);
    }
    //indexは左右どっちをクリックしたのか
    //IDはユビ
    public static void Click(int index, int id)
    {
        //ラインを光らせる
        dessertGame.AddAlpha(index);

        //ノーツを消す処理
        for (int i = 0; i < dessertGame.GetActiveNotes().Count; i++)
        {
            DessertNotes notes = dessertGame.GetActiveNotes()[i];

            if (!notes.gameObject.activeSelf) continue;

            notes.SetTouchID(id);

            //同じレーンなのかどうか
            if (!notes.CheckHitlane(index)) continue;

            notes.Hit(id);

            return;


        }

    }

    private const float sidePos = 6.0f;
    public static Vector3 GetSideVec(DessertNotes notes)
    {
        if (notes.GetNotesPos() == DessertNotes.NotesPos.grand) return Vector3.zero;

        switch (notes.GetNotesPos())
        {
            case DessertNotes.NotesPos.left:

                notes.SetNotesPos(DessertNotes.NotesPos.right);
                return new Vector3(sidePos, 0, 0);

            case DessertNotes.NotesPos.right:

                notes.SetNotesPos(DessertNotes.NotesPos.left);
                return new Vector3(sidePos * -1, 0, 0);
        }

        return Vector3.zero;

    }


    public static void AddAllNotes(DessertNotes notes) { dessertGame.AddAllNotes(notes); }
    public static void SbuAllNotes(DessertNotes notes) { dessertGame.SbuAllNotes(notes); }
    public static void AddActiveNotes(DessertNotes notes) { dessertGame.AddActiveNotes(notes); }
    public static void SbuActiveNotes(DessertNotes notes) { dessertGame.SbuActiveNotes(notes); }

    public static void ALLChenge()
    {

        //デバッグよう
        if (!Input.GetKeyUp(KeyCode.P)) return;

        List<DessertNotes> notes = dessertGame.GetAllNotes();

        DessertNotes.t = 0;
        for (int i = 0; i < notes.Count; i++)
        {
        }

    }

    public static int RotetoRate = 1;
    public static void StartRoteto(int reta) {dessertGame.StartRoteto(reta); RotetoRate *= -1; }
    public static void SetNotesParent(GameObject gameObject) {dessertGame.SetNotesParent(gameObject);}

    public static int GetRotetoCount() {return dessertGame.GetRotetoCount();}
}
using NoteEditor.Notes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChengeNotes : MonoBehaviour
{
    public static NoteTypes NoteTypes = NoteTypes.Flick;
    public static bool flag = false;
    public static bool DessertSide = false;
    public TextMeshProUGUI text;
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            flag = true;
            return;
        }

        if (Input.GetKey(KeyCode.F)) NoteTypes = NoteTypes.Flick;
        if (Input.GetKey(KeyCode.S)) NoteTypes = NoteTypes.Skill;
        if (Input.GetKey(KeyCode.N)) NoteTypes = NoteTypes.Single;
        if (Input.GetKey(KeyCode.L)) NoteTypes = NoteTypes.Long;
        if (Input.GetKeyDown(KeyCode.M)) DessertSide = !DessertSide;

        text.text = NoteTypes.ToString() + "  デザートなのか" + DessertSide;

    }

    public static Color SetColor(NoteTypes noteTypes)
    {
        switch (noteTypes)
        {
            case NoteTypes.Skill:

                return Color.yellow;

            case NoteTypes.Flick:
                return Color.red;

            case NoteTypes.Single:
                return Color.blue;
            case NoteTypes.Long:
                return Color.magenta;
        }

        return Color.white;

    }


}

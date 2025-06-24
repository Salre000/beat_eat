using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateNotesMaterialAll", menuName = "ノーツのマテリアルを纏めるオブジェクトを生成")]
public class NotesMaterialAll : ScriptableObject
{
    public List<NotesMaterial> notesMaterials = new List<NotesMaterial>();

}
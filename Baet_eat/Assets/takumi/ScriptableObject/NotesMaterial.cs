using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CreateNotesMaterial", menuName = "ノーツのマテリアルオブジェクトを生成")]
public class NotesMaterial : ScriptableObject
{

    public Material normal;
    public Material skill;
    public Material flick;
    public Material flickup;
    public Material _long;
    public Material _longlong;


    public string typeName = "";
    public string typeNameExplanation = "";


}

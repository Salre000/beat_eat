using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSEObject", menuName = "ノーツのタップ音のオブジェクト")]
public class SoundSEObject : ScriptableObject
{

    public AudioClip normal;
    public AudioClip skill;
    public AudioClip flick;
    public AudioClip _long;

    public string typeName = "";
    public string typeNameExplanation = "";


}

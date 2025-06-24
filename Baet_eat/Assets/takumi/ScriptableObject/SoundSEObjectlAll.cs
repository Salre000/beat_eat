using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoundSEObjectAll", menuName = "ノーツのタップ音のオブジェクトを纏めるオブジェクトを生成")]
public class SoundSEObjectlAll : ScriptableObject
{
    public List<SoundSEObject> notesMaterials = new List<SoundSEObject>();

}
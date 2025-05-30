using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CreateStringBox", menuName = "文字列の配列オブジェクト")]

public class StringObject : ScriptableObject
{
    public List<string> strings = new List<string>();
    public List<string> Explanation = new List<string>();
}

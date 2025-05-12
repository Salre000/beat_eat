using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MusicDataBase", menuName = "楽曲データベースを作成")]

public class MusicDataBase : ScriptableObject
{
    // MusicDataを配列で保存
    [SerializeField] public MusicData[] musicData;
}

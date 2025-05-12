using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "�y�ȃf�[�^���쐬")]

public class MusicData : ScriptableObject
{
    [SerializeField] public string musicID;
    [SerializeField] public string musicName;
    [SerializeField] public int musicLevel;
    [SerializeField] public Sprite jacket;
}

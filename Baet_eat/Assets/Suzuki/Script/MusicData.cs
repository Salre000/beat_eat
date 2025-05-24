using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "�y�ȃf�[�^���쐬")]

public class MusicData : ScriptableObject
{
    [SerializeField] public string musicID;
    [SerializeField] public string musicName;
    [SerializeField] public float BPM;
    [SerializeField] public int musicLevel;
    [SerializeField] public Sprite jacket;
    [SerializeField] public string musicAuthorName;
    [SerializeField] public string musicComposerName;
    [SerializeField] public string musicArrangerName;

    public int difficulty=4;
}

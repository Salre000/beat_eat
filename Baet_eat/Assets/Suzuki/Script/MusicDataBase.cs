using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MusicDataBase", menuName = "�y�ȃf�[�^�x�[�X���쐬")]

public class MusicDataBase : ScriptableObject
{
    // MusicData��z��ŕۑ�
    [SerializeField] public MusicData[] musicData;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSEObject", menuName = "�m�[�c�̃^�b�v���̃I�u�W�F�N�g")]
public class SoundSEObject : ScriptableObject
{

    public AudioClip normal;
    public AudioClip skill;
    public AudioClip flick;
    public AudioClip _long;

    public string typeName = "";
    public string typeNameExplanation = "";


}

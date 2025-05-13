using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSoundManager 
{
    GameObject _soundGameObject;

    AudioClip _notesHitSound;

    AudioSource _soundSource;

    public NotesSoundManager(GameObject soundGameObject, AudioClip notesHitSound) 
    {
        _soundGameObject = soundGameObject;
        _notesHitSound = notesHitSound;
        _soundSource = soundGameObject.GetComponent<AudioSource>();
    }

    public void StartNotesHitSound() { _soundSource.PlayOneShot(_notesHitSound); }



}
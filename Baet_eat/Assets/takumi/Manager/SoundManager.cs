using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip _notesHitSound;

    [SerializeField] AudioClip mainBGM;

    [SerializeField]private float time = 0;

    AudioSource _soundSource;

    public void Awake()
    {
        SoundUtility.soundManager = this;
    }

    public void Start()
    {
        _soundSource = gameObject.GetComponent<AudioSource>();

        _soundSource.clip = mainBGM;
        _soundSource.time = 0;
        _soundSource.Play();
    }

    public void StartNotesHitSound() { _soundSource.PlayOneShot(_notesHitSound); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
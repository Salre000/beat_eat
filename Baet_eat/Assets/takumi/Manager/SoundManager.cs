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
        _soundSource.time = 180;//終わりかけが195
        _soundSource.Play();
    }

    float afterTime = 0;
    public void FixedUpdate()
    {
        if (_soundSource.isPlaying || time != 0) return;

        //音楽が終了したときに回る
        afterTime += Time.deltaTime;

        if (afterTime <= 1) return;

        Debug.Log("シーン移行");



    }

    public void StartNotesHitSound() { _soundSource.PlayOneShot(_notesHitSound); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
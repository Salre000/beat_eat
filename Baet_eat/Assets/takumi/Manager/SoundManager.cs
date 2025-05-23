using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip _notesHitSound;


    [SerializeField]private float time = 0;

    AudioSource _soundSource;
    [SerializeField]AudioSource _BGMSoundSource;

    [SerializeField] public GameObject _sound;
    public void Awake()
    {
        SoundUtility.soundManager = this;
    }
    private readonly string FliePass = "Music/";

    public void Start()
    {
        _soundSource = gameObject.GetComponent<AudioSource>();



        _soundSource.clip = Resources.Load<AudioClip>(FliePass+Resources.Load<MusicDataBase>(SaveData.MusicDataName).musicData[ScoreStatus.nowMusic].musicName); ;
        _soundSource.time = 0;//終わりかけが195
        _sound.transform.position+=new Vector3(0,0, 0*20);
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
        GameSceneManager.LoadScene(GameSceneManager.resultScene);
    }

    public void SetNotesHitSound(AudioClip clip) { _notesHitSound = clip; }
    public void StartNotesHitSound() { _BGMSoundSource.PlayOneShot(_notesHitSound); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
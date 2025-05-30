using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip _notesNormalHitSound;
    [SerializeField] AudioClip _notesLongHitSound;
    [SerializeField] AudioClip _notesFlickHitSound;
    [SerializeField] AudioClip _notesSkilllHitSound;


    [SerializeField] private float time = 0;

    AudioSource _soundSource;
    [SerializeField] AudioSource _BGMSoundSource;

    public GameObject _sound;

    [SerializeField] CreateNotes createNotes;

    void OnEnable()
    {


    }

    public void Awake()
    {
        SoundUtility.soundManager = this;
    }
    private readonly string FliePass = "Musics/";
    private float StartOffset = OptionStatus.GetNotesSpeed() * 20 * 6f;

    public void Start()
    {
        MusicDataBase musicData = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        _soundSource = gameObject.GetComponent<AudioSource>();

        _soundSource.clip = Resources.Load<AudioClip>(FliePass + musicData.musicData[ScoreStatus.nowMusic].musicName); ;
        _soundSource.time = 0;//終わりかけが195

        //ジャケット分だけ後ろに戻す
        _sound.transform.position += new Vector3(0, 0, StartOffset);


    }

    float afterTime = 0;
    bool oneFlag = false;
    public void FixedUpdate()
    {

        if (DelayStart()) return; ;

        if (_soundSource.isPlaying || time != 0) return;

        //音楽が終了したときに回る
        afterTime += Time.deltaTime;

        if (afterTime <= 1) return;
        if (oneFlag) return;

        Debug.Log("シーン移行");
        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);
        oneFlag = true;
    }

    private bool OneFlag = false;
    private float DelayTime = 0;
    private bool DelayStart()
    {
        if (OneFlag) return false;
        DelayTime += Time.deltaTime;

        if (DelayTime < 6f) return true;

        _soundSource.Play();
        OneFlag = true;

        return false;

    }

    public static void DebagClear() { GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive); }

    public void SetNotesHitSound(AudioClip clip) { _notesNormalHitSound = clip; }
    public void StartNotesNormalHitSound() { _BGMSoundSource.PlayOneShot(_notesNormalHitSound); }
    public void StartNotesFlickHitSound() { _BGMSoundSource.PlayOneShot(_notesFlickHitSound); }
    public void StartNotesLongHitSound() { _BGMSoundSource.PlayOneShot(_notesLongHitSound); }
    public void StartNotesSkillHitSound() { _BGMSoundSource.PlayOneShot(_notesSkilllHitSound); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
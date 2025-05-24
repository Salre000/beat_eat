using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClip _notesHitSound;


    [SerializeField] private float time = 0;

    AudioSource _soundSource;
    [SerializeField] AudioSource _BGMSoundSource;

    [SerializeField] Canvas jacketCanvas;

    public GameObject _sound;
    public void Awake()
    {
        SoundUtility.soundManager = this;
    }
    private readonly string FliePass = "Musics/";
    private float StartOffset = OptionStatus.GetNotesSpeed() * 20 * 2.5f;

    public void Start()
    {
        MusicDataBase musicData = Resources.Load<MusicDataBase>(SaveData.MusicDataName);

        _soundSource = gameObject.GetComponent<AudioSource>();

        jacketCanvas.gameObject.transform.GetChild(2).GetComponent<Image>().sprite =
           musicData.musicData[ScoreStatus.nowMusic].jacket;

        jacketCanvas.gameObject.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            musicData.musicData[ScoreStatus.nowMusic].name;

        _soundSource.clip = Resources.Load<AudioClip>(FliePass + musicData.musicData[ScoreStatus.nowMusic].musicName); ;
        _soundSource.time = 0;//終わりかけが195

        //ジャケット分だけ後ろに戻す
        _sound.transform.position += new Vector3(0, 0, StartOffset);


    }

    float afterTime = 0;
    public void FixedUpdate()
    {

        if (DelayStart()) return; ;

        if (_soundSource.isPlaying || time != 0) return;

        //音楽が終了したときに回る
        afterTime += Time.deltaTime;

        if (afterTime <= 1) return;

        Debug.Log("シーン移行");
        GameSceneManager.LoadScene(GameSceneManager.resultScene);
    }

    private bool OneFlag = false;
    private float DelayTime = 0;
    private bool DelayStart()
    {
        if (OneFlag) return false;
        DelayTime += Time.deltaTime;

        if (DelayTime < 2.5f) return true;

        _soundSource.Play();
        OneFlag = true;

        //ここでジャケットを消す
        jacketCanvas.gameObject.SetActive(false);

        return false;

    }

    public void SetNotesHitSound(AudioClip clip) { _notesHitSound = clip; }
    public void StartNotesHitSound() { _BGMSoundSource.PlayOneShot(_notesHitSound); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundSEObject nowSound;

    [SerializeField] private float time = 0;

    AudioSource _soundSource;
    [SerializeField] AudioSource _BGMSoundSource;

    public bool GetPlayFlag() { return _soundSource.isPlaying; }

    public GameObject _sound;

    [SerializeField] CreateNotes createNotes;

    [SerializeField] GameObject poseImage;
    [SerializeField] TextMeshProUGUI poseCount;

    [SerializeField] private float pitch = 1;

    private readonly int poolCount = 50;
    [SerializeField] private List<AudioSource> souresPool = new List<AudioSource>();

    [SerializeField] AudioMixerGroup mixerGroup;


    public void Awake()
    {
        SoundUtility.soundManager = this;
        nowSound = Resources.Load<SoundSEObjectlAll>("InGame/SoundSEObjectAll").notesMaterials[OptionStatus.GetSEID()];
        for (int i = 0; i < poolCount; i++)
        {

            GameObject gameObject = new GameObject("SoundPool" + i.ToString());
            souresPool.Add(gameObject.AddComponent<AudioSource>());
            souresPool[i].outputAudioMixerGroup = mixerGroup;

        }

        pitch = ((SoundSEEnum.SoundSEType)OptionStatus.GetSEID()).GetPitch();

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

        poseImage.SetActive(false);
        poseCount.gameObject.SetActive(false);

    }

    float afterTime = 0;
    bool oneFlag = false;
    public void FixedUpdate()
    {
        SbuPoseCount();

        if (DelayStart()) return; ;

        if (_soundSource.isPlaying || time != 0) return;

        //音楽が終了したときに回る
        afterTime += Time.deltaTime;

        if (afterTime <= 1) return;
        if (oneFlag) return;
        AchievementStatus.Achievement(AchievementTypeEnum.AchievementType._NewEed);
        Debug.Log("シーン移行");
        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);
        oneFlag = true;
    }

    private bool OneFlag = false;
    private float DelayTime = 0;
    private bool DelayStart()
    {
        if (NotesMove.Instance.stopFlag) return true;
        if (OneFlag) return false;
        DelayTime += Time.deltaTime;

        if (DelayTime < 6f) return true;


        OneFlag = true;
        _soundSource.Play();

        return false;

    }

    public void OpenPose()
    {
        poseCount.gameObject.SetActive(false);

        poseImage.SetActive(true);
        NotesMove.Instance.stopFlag = true;
        MainBGMStop();
    }

    private float poseCountDown = 0;
    public void ClosePose()
    {
        poseImage.SetActive(false);
        poseCount.gameObject.SetActive(true);
        poseCountDown = 3;
        poseCount.text = poseCountDown.ToString("F2");

    }

    private void SbuPoseCount()
    {
        if (!poseCount.gameObject.activeSelf) return;


        poseCountDown -= Time.deltaTime;

        poseCount.text = poseCountDown.ToString("F2");
        if (poseCountDown > 0) return;



        MainBGMStart();
        NotesMove.Instance.stopFlag = false;
        poseCount.gameObject.SetActive(false);

    }

    public static void DebagClear() { GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive); }

    public void Restart()
    {
        TransitionEffect.nextSceneNameSystem = GameSceneManager.mainScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);

    }

    public void ChangeSelect()
    {
        TransitionEffect.nextSceneNameSystem = GameSceneManager.selectScene;

        GameSceneManager.LoadScene(GameSceneManager.changeScene, LoadSceneMode.Additive);


    }

    private AudioSource GetAudio()
    {
        for (int i = 0; i < souresPool.Count; i++)
        {

            if (souresPool[i].isPlaying) continue;


            return souresPool[i];
        }

        return null;

    }
    private void SetSound(AudioClip clip)
    {
        AudioSource sound = GetAudio();

        sound.clip = clip;

        sound.pitch = pitch;

        sound.Play();

    }

    public void StartNotesNormalHitSound() { SetSound(nowSound.normal); }
    public void StartNotesFlickHitSound() { SetSound(nowSound.flick); }
    public void StartNotesLongHitSound() { SetSound(nowSound._long); }
    public void StartNotesSkillHitSound() { SetSound(nowSound.skill); }

    public void MainBGMStop() { if (time != 0) return; time = _soundSource.time; _soundSource.Stop(); }
    public void MainBGMStart() { if (time == 0) return; _soundSource.time = time; _soundSource.Play(); time = 0; }

    public float GetNowTime() { return _soundSource.time; }



}
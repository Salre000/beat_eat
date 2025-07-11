using NoteEditor.Model;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewSelectMusic : MonoBehaviour
{
    // �I�΂�Ă���Ȃ��W���P�b�g�≹�y�𗬂�

    [SerializeField] private MusicDataBase dataBase;
    [SerializeField, Header("�E�̑傫�ȃW���P�b�g�\������")] private Image _jacket;
    [SerializeField, Header("�W���P�b�g�̔w�i�\������")] private Image _backJacket;
    [SerializeField, Header("�ȓ�Փx�e�L�X�g")] private TextMeshProUGUI _musicLevel;

    private AudioSource _audioSource;
    private List<AudioClip> _musicList=new(MusicManager.CAPACITY);
    private StringBuilder _stringBuilder;
    private int _selectNumber;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _selectNumber = MusicManager.instance.GetSelectMusicNumber();
        _stringBuilder = new StringBuilder();
        _stringBuilder.Clear();
        _audioSource = GetComponent<AudioSource>();
        // 
        LoadMusic();
        SelectedMusic();
    }

    private void Update()
    {

        if (_selectNumber != MusicManager.instance.GetSelectMusicNumber())
        {
            _selectNumber = MusicManager.instance.GetSelectMusicNumber();
            SelectedMusic();
        }
        if (!_audioSource.isPlaying) _audioSource.PlayOneShot(_musicList[_selectNumber]);
    }

    // �I���J�[�h���؂�ւ�邽�тɌĂяo��
    private void SelectedMusic()
    {
        ChangeJacket();
        _audioSource.Stop();
        _audioSource.PlayOneShot(_musicList[_selectNumber]);
    }

    private void ChangeJacket()
    {
        _jacket.sprite = dataBase.musicData[_selectNumber+MusicManager.NOTMUSICNUMBER].jacket;
        _backJacket.sprite = _jacket.sprite;
    }

    private void BuildingString(string toString, bool isFileName = false)
    {
        _stringBuilder.Clear();
        if (isFileName != false)
            _stringBuilder.Append("Musics/");

        _stringBuilder.Append(toString);
    }

    private void LoadMusic()
    {
        for(int i = 1;i<dataBase.musicData.Count;i++)
        {
            BuildingString(dataBase.musicData[i].musicName, true);
            string musicName = _stringBuilder.ToString();
            _musicList.Add(Resources.Load<AudioClip>(musicName));
        }
    }
}

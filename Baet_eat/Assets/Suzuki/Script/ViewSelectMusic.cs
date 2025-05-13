using NoteEditor.Model;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewSelectMusic : MonoBehaviour
{
    // 選ばれている曲をジャケットや音楽を流す

    [SerializeField] private MusicDataBase dataBase;
    [SerializeField, Header("右の大きなジャケット表示部分")] private Image _jacket;
    [SerializeField, Header("ジャケットの背景表示部分")] private Image _backJacket;
    [SerializeField, Header("曲難易度テキスト")] private TextMeshProUGUI _musicLevel;

    private AudioSource _audioSource;
    private AudioClip _music;
    private StringBuilder _stringBuilder;
    private string _musicName;
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
        BuildingString(dataBase.musicData[_selectNumber].musicName, true);
        _musicName = _stringBuilder.ToString();
        _music = (AudioClip)Resources.Load(_musicName);
        SelectedMusic();
    }

    private void Update()
    {

        if (_selectNumber != MusicManager.instance.GetSelectMusicNumber())
        {
            _selectNumber = MusicManager.instance.GetSelectMusicNumber();
            SelectedMusic();
        }
    }

    // 選択カードが切り替わるたびに呼び出し
    private void SelectedMusic()
    {
        BuildingString(dataBase.musicData[_selectNumber].musicName, true);
        ChangeJacket();
        _musicName = _stringBuilder.ToString();
        _music = (AudioClip)Resources.Load(_musicName);
        _audioSource.Stop();
        _audioSource.PlayOneShot(_music);
    }

    private void ChangeJacket()
    {
        _jacket.sprite = dataBase.musicData[_selectNumber].jacket;
        _backJacket.sprite = _jacket.sprite;
    }

    private void BuildingString(string toString, bool isFileName = false)
    {
        _stringBuilder.Clear();
        if (isFileName != false)
            _stringBuilder.Append("Music/");

        _stringBuilder.Append(toString);
    }
}

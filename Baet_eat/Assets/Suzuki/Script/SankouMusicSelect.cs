using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text;

public class SankouMusicSelect : MonoBehaviour
{
    [SerializeField] private MusicDataBase dataBase;
    [SerializeField] private TextMeshProUGUI[] _musicNameText;
    [SerializeField] private TextMeshProUGUI[] _musicLevelText;
    [SerializeField] private Image[] _musicJacket;
    [SerializeField] private Image _jacket;

    private AudioSource _audio;
    private AudioClip _music;
    private string _musicName;
    private StringBuilder _stringBuilder;

    private int _select;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _select = 0;
        _stringBuilder = new StringBuilder();
        _stringBuilder.Clear();
        _audio = GetComponent<AudioSource>();
        BuildingString(dataBase.musicData[_select].musicName, true);
        _musicName = _stringBuilder.ToString();
        _music = (AudioClip)Resources.Load(_musicName);
        MusicUpdateALL();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_select < dataBase.musicData.Count)
            {
                _select++;
                MusicUpdateALL();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_select > 0)
            {
                _select--;
                MusicUpdateALL();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }
    private void MusicUpdateALL()
    {
        BuildingString(dataBase.musicData[_select].musicName, true);
        _musicName = _stringBuilder.ToString();
        Debug.Log(_musicName);
        _music = (AudioClip)Resources.Load(_musicName);
        _audio.Stop();
        _audio.PlayOneShot(_music);
        for (int i = 0; i < 5; i++)
        {
            MusicUpdate(i - 2);
        }
    }
    private void MusicUpdate(int id)
    {
        try
        {
            BuildingString(dataBase.musicData[_select + id].musicName);
            _musicNameText[id + 2].text = _stringBuilder.ToString();
            _stringBuilder.Clear();
            _stringBuilder.Append(dataBase.musicData[_select + id].musicLevel);
            _musicLevelText[id + 2].text = _stringBuilder.ToString();
            _musicJacket[id+2].sprite=dataBase.musicData[_select+id].jacket;
        }
        catch
        {
            // •\Ž¦ŠO
            BuildingString("");
            _musicNameText[id + 2].text = _stringBuilder.ToString();
            _musicLevelText[id + 2].text = _stringBuilder.ToString();
            _musicJacket[id + 2].sprite =null;
        }
        if (id == 0)
        {
            _jacket.sprite = dataBase.musicData[_select + id].jacket;
        }
    }
    private void BuildingString(string toString, bool isFileName = false)
    {
        _stringBuilder.Clear();
        if (isFileName != false)
            _stringBuilder.Append("Music/");

        _stringBuilder.Append(toString);
    }

}
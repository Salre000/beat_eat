using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    // �S�ȕ��K�v�A�Ȃ𑝂₵����CAPACITY���X�V���邱��
    public const int CAPACITY = 7;
    // �S�Ă̋ȃJ�[�h��}��
    [SerializeField] private List<GameObject> _musicCards = new(CAPACITY);
    private const float _DISTANCE = 125.0f;
    // ���ݑI�𒆂̃J�[�h
    private int _selectMusicNumber = 0;
    // �I�𒆂̓�Փx 0~4
    private int _difficultyNumber = 0;
    // ��Փx�̕ύX�����m
    private bool _isChangeDifficulty = false;
    // �Ȃ��^�b�v�őI���������𔻒�
    private bool _isSelected = false;
    private RectTransform _closest = null;

    private void Awake()
    {
        instance = this;



        // �ȃJ�[�h�𓙊Ԋu�ɕ��ׂ�(y����)
        for(int i=0; i < _musicCards.Count; i++)
        {
            if (i==0) continue;
            Vector2 vector2 = _musicCards[i-1].transform.localPosition;
            vector2.y-= _DISTANCE;
            _musicCards[i].transform.localPosition = vector2;
        }
    }

    private void Start()
    {
        _difficultyNumber = (int)ScoreStatus.nowDifficulty;
    }

    public List<GameObject> GetMusicCards()
    {
        return _musicCards;
    }

    // �I�΂�Ă���Ȃ�Ԃ�
    public int GetSelectMusicNumber() { return _selectMusicNumber; }
    // �I�΂ꂽ��ID�̃Z�b�g
    public void SetSelectMusicNumer(int selectMusicNumber) { _selectMusicNumber = selectMusicNumber; }
    // ���݂̓�Փx��Ԃ�
    public int GetDifficultyNumber() {  return _difficultyNumber; }
    // ��Փx�̕ύX������΍��킹�ĕύX����
    public void SetDifficultyNumber(int setDifficulty) {  _difficultyNumber = setDifficulty; }
    // ��Փx�̕ύX���m
    public bool IsChangeDifficulty() { return _isChangeDifficulty; }
    // ��Փx�̕ύX�����m�点
    public void SetChangeDifficulty() { _isChangeDifficulty = !_isChangeDifficulty; }
    // �^�b�v�Ō��m
    public bool IsSelected() { return _isSelected; }
    // �^�b�v�����������炤
    public void SetSelected(bool isSelected) {  _isSelected = isSelected; }
    // �������{�^���𓮂������߂ɕԂ�
    public RectTransform GetClosest() { return _closest; }
    // �Ώۂ��擾
    public void SetClosest(RectTransform closest) { _closest = closest; }
}

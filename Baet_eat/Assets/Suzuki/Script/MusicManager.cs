using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public const int _CAPACITY = 20;
    // 全ての曲カードを挿入
    [SerializeField] private List<GameObject> _musicCards = new(_CAPACITY);
    private const float _DISTANCE = 125.0f;
    // 現在選択中のカード
    private int _selectMusicNumber = 0;
    // 選択中の難易度 0~4
    private int _difficultyNumber = 0;
    // 難易度の変更を感知
    private bool _isChangeDifficulty = false;

    private void Awake()
    {
        instance = this;
        // 曲カードを等間隔に並べる(y軸に)
        for(int i=0; i < _musicCards.Count; i++)
        {
            if (i==0) continue;
            Vector2 vector2 = _musicCards[i-1].transform.localPosition;
            vector2.y-= _DISTANCE;
            _musicCards[i].transform.localPosition = vector2;
        }
    }

    public List<GameObject> GetMusicCards()
    {
        return _musicCards;
    }
    public int GetSelectMusicNumber() { return _selectMusicNumber; }

    public void SetSelectMusicNumer(int selectMusicNumber) { _selectMusicNumber = selectMusicNumber; }

    public int GetDifficultyNumber() {  return _difficultyNumber; }
    public void SetDifficultyNumber(int setDifficulty) {  _difficultyNumber = setDifficulty; }

    public bool IsChangeDifficulty() { return _isChangeDifficulty; }
    public void SetChangeDifficulty() { _isChangeDifficulty = !_isChangeDifficulty; }
}

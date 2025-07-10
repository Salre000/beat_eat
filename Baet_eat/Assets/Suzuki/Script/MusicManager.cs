using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    // 全曲分必要、曲を増やしたらCAPACITYを更新すること
    public const int CAPACITY = 7;
    // 全ての曲カードを挿入
    [SerializeField] private List<GameObject> _musicCards = new(CAPACITY);
    private const float _DISTANCE = 125.0f;
    // 現在選択中のカード
    private int _selectMusicNumber = 0;
    // 選択中の難易度 0~4
    private int _difficultyNumber = 0;
    // 難易度の変更を感知
    private bool _isChangeDifficulty = false;
    // 曲をタップで選択したかを判定
    private bool _isSelected = false;
    private RectTransform _closest = null;

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

    private void Start()
    {
        _difficultyNumber = (int)ScoreStatus.nowDifficulty;
    }

    public List<GameObject> GetMusicCards()
    {
        return _musicCards;
    }

    // 選ばれている曲を返す
    public int GetSelectMusicNumber() { return _selectMusicNumber; }
    // 選ばれた曲IDのセット
    public void SetSelectMusicNumer(int selectMusicNumber) { _selectMusicNumber = selectMusicNumber; }
    // 現在の難易度を返す
    public int GetDifficultyNumber() {  return _difficultyNumber; }
    // 難易度の変更があれば合わせて変更する
    public void SetDifficultyNumber(int setDifficulty) {  _difficultyNumber = setDifficulty; }
    // 難易度の変更検知
    public bool IsChangeDifficulty() { return _isChangeDifficulty; }
    // 難易度の変更をお知らせ
    public void SetChangeDifficulty() { _isChangeDifficulty = !_isChangeDifficulty; }
    // タップで検知
    public bool IsSelected() { return _isSelected; }
    // タップしたかをもらう
    public void SetSelected(bool isSelected) {  _isSelected = isSelected; }
    // 押したボタンを動かすために返す
    public RectTransform GetClosest() { return _closest; }
    // 対象を取得
    public void SetClosest(RectTransform closest) { _closest = closest; }
}

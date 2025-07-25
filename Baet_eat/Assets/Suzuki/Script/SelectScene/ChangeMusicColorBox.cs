using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicColorBox : MonoBehaviour
{
    private List<GameObject> _musicSelects = new(MusicManager.CAPACITY);
    private List<Outline> _musicSelectOutLine = new(MusicManager.CAPACITY);
    private List<GameObject> _colorBoxs = new(MusicManager.CAPACITY);
    private const int _BOX_MAX = 4;
    private Image _boxImage;
    private Image _backJacketImage;
    // 難易度別カラー


    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        _musicSelects = MusicManager.instance.GetMusicCards();
        // ジャケット裏のイメージカラーも変更する
        _backJacketImage=GameObject.Find("JacketBackImage").GetComponent<Image>();
        // 選択されている難易度で曲カードの色を変更
        for (int i = 0; i < _musicSelects.Count; i++)
        {
            _colorBoxs.Add(_musicSelects[i].transform.GetChild(0).gameObject);
            _musicSelectOutLine.Add(_musicSelects[i].GetComponent<Outline>());
            for (int n = 0; n < _BOX_MAX; n++)
            {
                _boxImage = _colorBoxs[i].transform.GetChild(n).GetComponent<Image>();

                switch (MusicManager.instance.GetDifficultyNumber())
                {
                    case 0:
                        _boxImage.color = ColorManager.DRINK_COLOR;
                        break;
                    case 1:
                        _boxImage.color = ColorManager.HORSDOEUVRE_COLOR;
                        break;
                    case 2:
                        _boxImage.color = ColorManager.SOUP_COLOR;
                        break;
                    case 3:
                        _boxImage.color = ColorManager.MAINDISH_COLOR;
                        break;
                    case 4:
                        _boxImage.color = ColorManager.DESSERT_COLOR;
                        break;
                }
            }
            _musicSelectOutLine[i].effectColor = _boxImage.color;
            _backJacketImage.color=_boxImage.color;
            _boxImage = null;
        }
        // ガベコレ行き
        _musicSelects = null;
    }

    private void Update()
    {
        // 難易度変更ボタンが実行され、難易度変更が行われたとき
        if (MusicManager.instance.IsChangeDifficulty())
        {
            ChangeColor();
            MusicManager.instance.SetChangeDifficulty();
        }
    }

    private void ChangeColor()
    {
        for (int i = 0; i < _colorBoxs.Count; i++)
        {
            for (int n = 0; n < _BOX_MAX; n++)
            {
                _boxImage = _colorBoxs[i].transform.GetChild(n).GetComponent<Image>();

                switch (MusicManager.instance.GetDifficultyNumber())
                {
                    case 0:
                        _boxImage.color = ColorManager.DRINK_COLOR;
                        break;
                    case 1:
                        _boxImage.color = ColorManager.HORSDOEUVRE_COLOR;
                        break;
                    case 2:
                        _boxImage.color = ColorManager.SOUP_COLOR;
                        break;
                    case 3:
                        _boxImage.color = ColorManager.MAINDISH_COLOR;
                        break;
                    case 4:
                        _boxImage.color = ColorManager.DESSERT_COLOR;
                        break;
                }

            }
            _musicSelectOutLine[i].effectColor = _boxImage.color;
            _backJacketImage.color=_boxImage.color;
            _boxImage = null;

        }
    }
}

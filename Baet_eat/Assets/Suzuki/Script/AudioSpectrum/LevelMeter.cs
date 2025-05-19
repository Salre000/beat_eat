using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMeter : MonoBehaviour
{
    [SerializeField] private AudioSpectrum spectrum;
    [SerializeField] private List<Transform> objects;
    [SerializeField] private float scale;
    private List<Image> images;
    private Transform _transform=null;
    private Vector3 _localScale = new Vector3(0,0,0);
    private const float _UPDATE_INTERVAL = 0.05f;
    private float _time = 0f;
    Vector3 _currentScale = Vector3.one;

    // サンプリングで得たHzで配列に入っているオブジェクトを動かす

    private void Awake()
    {
        images = new(objects.Count);
        for(int i = 0; i < objects.Count; i++)
        {
            images.Add(objects[i].GetComponent<Image>());
        }
    }

    private void Update()
    {
        ChangeColor();
        _time += Time.deltaTime;
        if (_time <= _UPDATE_INTERVAL)
            return;

        _time = 0f;
        for (int i = 0; i < objects.Count; i++)
        {
            _transform = objects[i];
            _localScale = _transform.localScale;
            _localScale.y = spectrum.Levels[i] * scale;
            if(_localScale.y <= 0.1f) _localScale.y = 0.1f;
            _currentScale = _transform.localScale;
            _currentScale.y = Mathf.Lerp(_currentScale.y, _localScale.y, Time.deltaTime * 100);
            _transform.localScale = _currentScale;
        }
    }

    // ついでにカラーを難易度に寄せてみる
    private void ChangeColor()
    {
        if(MusicManager.instance.IsChangeDifficulty()) return;
        for (int i = 0;i < objects.Count;i++)
        {
            switch (MusicManager.instance.GetDifficultyNumber())
            {
                case 0:
                    images[i].color = ColorManager.DRINK_COLOR_TRANSLUCENT;
                    break;
                case 1:
                    images[i].color = ColorManager.HORSDOEUVRE_COLOR_TRANSLUCENT;
                    break;
                case 2:
                    images[i].color = ColorManager.SOUP_COLOR_TRANSLUCENT;
                    break;
                case 3:
                    images[i].color = ColorManager.MAINDISH_COLOR_TRANSLUCENT;
                    break;
                case 4:
                    images[i].color = ColorManager.DESSERT_COLOR_TRANSLUCENT;
                    break;
            }
        }
    }
}
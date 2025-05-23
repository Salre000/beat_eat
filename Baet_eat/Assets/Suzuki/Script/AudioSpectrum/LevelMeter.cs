using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMeter : MonoBehaviour
{
    [SerializeField] private AudioSpectrum spectrum;
    [SerializeField] private List<Transform> objects;
    [SerializeField] private float scale;
    private List<Image> images;
    private Transform _transform = null;
    private Vector3 _localScale = new Vector3(0, 0, 0);
    Vector3 _currentScale = Vector3.one;
    private WaitForSeconds _wait = new WaitForSeconds(0.05f);

    // サンプリングで得たHzで配列に入っているオブジェクトを動かす

    private void Awake()
    {
        images = new(objects.Count);
        for (int i = 0; i < objects.Count; i++)
        {
            images.Add(objects[i].GetComponent<Image>());
        }
        StartCoroutine(WaitFrame());
    }

    IEnumerator WaitFrame()
    {
        while (true)
        {
            ChangeColor();
            yield return _wait;
            Meter();
        }

    }

    private void Update()
    {

    }

    private void Meter()
    {
        for (int i = 0; i<objects.Count; i++)
        {
            _transform = objects[i];
            _localScale = _transform.localScale;
            _localScale.y = spectrum.Levels[i]* scale;
            if (_localScale.y <= 0.1f) _localScale.y = 0.1f;
            _currentScale = _transform.localScale;
            _currentScale.y = Mathf.Lerp(_currentScale.y, _localScale.y, Time.deltaTime * 150);
            _transform.localScale = _currentScale;
        }
    }


    // ついでにカラーを難易度に寄せてみる
    private void ChangeColor()
    {
        if (MusicManager.instance.IsChangeDifficulty()) return;
        for (int i = 0; i < objects.Count; i++)
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLevelMeter : MonoBehaviour
{
    // タイトル画面用
    [SerializeField] private AudioSpectrum spectrum;
    [SerializeField] private List<Transform> objects;
    [SerializeField] private float scale;
    private List<Image> images;
    private Transform _transform = null;
    private Vector3 _localScale = new Vector3(0, 0, 0);
    Vector3 _currentScale = Vector3.one;
    private WaitForSeconds _wait = new WaitForSeconds(0.05f);

    // サンプリングで得たHzで配列に入っているオブジェクトを動かす

    // 虹に色を回す
    private const int _UP_REMIT = 255;
    private const int _DOWN_REMIT = 0;
    private byte _colorR = 255;
    private byte _colorG = 0;
    private byte _colorB = 0;
    private const int _num = 5;
    private Color32 _color = new();
    private int _valueR = 0;
    private int _valueG = 0;
    private int _valueB = 0;

    private void Awake()
    {
        _color.a=_UP_REMIT;
        _color.r = _colorR;
        _color.g = _colorG;
        _color.b = _colorB;
        _valueR=_colorR;
        _valueG=_colorG;
        _valueB=_colorB;
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
            yield return _wait;
            Meter();
        }

    }

    private void FixedUpdate()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        // Rが255ならGを下げる
        if (_valueR >= _UP_REMIT)
        {
            _valueG -= _num;
            if(_valueG<_DOWN_REMIT)
                _valueG = _DOWN_REMIT;
            _colorG = (byte)_valueG;
        }
        // Gが0ならBを上げる
         if (_valueG <= _DOWN_REMIT)
        {
            _valueB += _num;
            if (_valueB > _UP_REMIT)
                _valueB = _UP_REMIT;
            _colorB = (byte)_valueB;
        }
        // Bが255ならRを下げる
         if(_valueB >= _UP_REMIT)
        {
            _valueR -= _num;
            if (_valueR < _DOWN_REMIT)
                _valueR = _DOWN_REMIT;
            _colorR = (byte)_valueR;
        }
        // Rが0ならGを上げる
         if(_valueR <= _DOWN_REMIT)
        {
            _valueG += _num;
            if (_valueG > _UP_REMIT)
                _valueG = _UP_REMIT;
            _colorG = (byte)_valueG;
        }
        // Gが255ならBを下げる
         if(_valueG >= _UP_REMIT)
        {
            _valueB -= _num;
            if (_valueB < _DOWN_REMIT)
                _valueB = _DOWN_REMIT;
            _colorB = (byte)_valueB;
        }
        // Bが0ならRを上げる
         if(_valueB <= _DOWN_REMIT)
        {
            _valueR += _num;
            if(_valueR>_UP_REMIT)
                _valueR = _UP_REMIT;
            _colorR = (byte)_valueR;
        }

        _color.r = _colorR;
        _color.g = _colorG;
        _color.b = _colorB;

        for (int i = 0; i < objects.Count; i++)
        {
            images[i].color = _color;
        }
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
}
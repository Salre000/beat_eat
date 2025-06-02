using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _lineTransforms = new List<RectTransform>(5);
    [SerializeField] private List<RectTransform> _maskTransforms = new List<RectTransform>(5);
    [SerializeField] private List<RectTransform> _targetTransforms = new List<RectTransform>(5);
    private List<Vector3> _resetTransforms = new List<Vector3>(5);
    private List<Vector3> line = new List<Vector3>(5);
    private List<Vector3> mask = new List<Vector3>(5);
    private List<bool> _isTrans = new (5);
    private float _coolTime = 0;
    private const float _MOVE_TIME= 3.5f;
    private float _speed = 15;
    private float startNum = 100f;
    private float _startTime = 0.0f;

    private void Awake()
    {
        
        for(int i=0;i< _maskTransforms.Count;i++)
        {
            _isTrans.Add(false);
            mask.Add(Vector3.zero);
            _resetTransforms.Add(Vector3.zero);
            line.Add(Vector3.zero);
            _resetTransforms[i]=_maskTransforms[i].localPosition;
            line[i]=_lineTransforms[i].position;
        }
    }

    private void Update()
    {
        if (_startTime < 1.0f)
        {
            _startTime += Time.deltaTime;
            return;
        }
        LineMove();
    }
    private void LineMove()
    {
        // 最後まで移動したならクールタイムを挟む
        if (_isTrans[4])
        {
            _coolTime += Time.deltaTime;
            if( _coolTime > _MOVE_TIME )
            {
                for(int i=0;i< _maskTransforms.Count;i++)
                {
                    _isTrans[i] = false;
                    mask[i]=Vector3.zero;
                }
                _coolTime = 0;
            }
        }

        // maskを動かしつつlineはそのままの位置に
        // line1
        if (!_isTrans[0])
        {
            mask[0] = _maskTransforms[0].localPosition;
            Vector3 value = mask[0];
            // maskの移動
            value.x = Mathf.Lerp(mask[0].x, _targetTransforms[0].localPosition.x, Time.deltaTime * _speed);
            value.y = Mathf.Lerp(mask[0].y, _targetTransforms[0].localPosition.y, Time.deltaTime * _speed);
            mask[0] = value;
            _maskTransforms[0].localPosition = mask[0];
            // 次のlineへ
            if ((mask[0] - _targetTransforms[0].localPosition).sqrMagnitude < 0.01f)
            {
                _maskTransforms[0].localPosition = _resetTransforms[0];
                _isTrans[0] = true;
            }
            // lineは動かさないように
            _lineTransforms[0].position = line[0];
        }
        // line2
        if (!_isTrans[1]&& (mask[0] - _targetTransforms[0].localPosition).sqrMagnitude < startNum)
        {
            mask[1] = _maskTransforms[1].localPosition;
            Vector3 value = mask[1];
            // maskの移動
            value.x = Mathf.Lerp(mask[1].x, _targetTransforms[1].localPosition.x, Time.deltaTime * _speed);
            value.y = Mathf.Lerp(mask[1].y, _targetTransforms[1].localPosition.y, Time.deltaTime * _speed);
            mask[1] = value;
            _maskTransforms[1].localPosition = mask[1];
            // 次のlineへ
            if ((mask[1] - _targetTransforms[1].localPosition).sqrMagnitude < 0.01f)
            {
                _maskTransforms[1].localPosition = _resetTransforms[1];
                _isTrans[1] = true;
            }
            // lineは動かさないように
            _lineTransforms[1].position = line[1];
        }
        // line3
        if (!_isTrans[2] && (mask[1] - _targetTransforms[1].localPosition).sqrMagnitude < startNum)
        {
            mask[2] = _maskTransforms[2].localPosition;
            Vector3 value = mask[2];
            // maskの移動
            value.x = Mathf.Lerp(mask[2].x, _targetTransforms[2].localPosition.x, Time.deltaTime * _speed);
            value.y = Mathf.Lerp(mask[2].y, _targetTransforms[2].localPosition.y, Time.deltaTime * _speed);
            mask[2] = value;
            _maskTransforms[2].localPosition = mask[2];
            // 次のlineへ
            if ((mask[2] - _targetTransforms[2].localPosition).sqrMagnitude < 0.01f)
            {
                _maskTransforms[2].localPosition = _resetTransforms[2];
                _isTrans[2] = true;
            }
            // lineは動かさないように
            _lineTransforms[2].position = line[2];
        }
        // line4
        if (!_isTrans[3] && (mask[2] - _targetTransforms[2].localPosition).sqrMagnitude < startNum)
        {
            mask[3] = _maskTransforms[3].localPosition;
            Vector3 value = mask[3];
            // maskの移動
            value.x = Mathf.Lerp(mask[3].x, _targetTransforms[3].localPosition.x, Time.deltaTime * _speed);
            value.y = Mathf.Lerp(mask[3].y, _targetTransforms[3].localPosition.y, Time.deltaTime * _speed);
            mask[3] = value;
            _maskTransforms[3].localPosition = mask[3];
            // 次のlineへ
            if ((mask[3] - _targetTransforms[3].localPosition).sqrMagnitude < 0.01f)
            {
                _maskTransforms[3].localPosition = _resetTransforms[3];
                _isTrans[3] = true;
            }
            // lineは動かさないように
            _lineTransforms[3].position = line[3];
        }
        // line5
        if (!_isTrans[4] && (mask[3] - _targetTransforms[3].localPosition).sqrMagnitude < startNum)
        {
            mask[4] = _maskTransforms[4].localPosition;
            Vector3 value = mask[4];
            // maskの移動
            value.x = Mathf.Lerp(mask[4].x, _targetTransforms[4].localPosition.x, Time.deltaTime * _speed);
            value.y = Mathf.Lerp(mask[4].y, _targetTransforms[4].localPosition.y, Time.deltaTime * _speed);
            mask[4] = value;
            _maskTransforms[4].localPosition = mask[4];
            // 次のlineへ
            if ((mask[4] - _targetTransforms[4].localPosition).sqrMagnitude < 0.01f)
            {
                _maskTransforms[4].localPosition = _resetTransforms[4];
                _isTrans[4] = true;
            }
            // lineは動かさないように
            _lineTransforms[4].position = line[4];
        }
    }

}

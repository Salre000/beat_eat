using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AnimationTitle : MonoBehaviour
{
    [SerializeField] private RectTransform _target; // 背景たちが目標地点に設定する位置
    [SerializeField] private RectTransform _leftBack;
    [SerializeField] private RectTransform _de;
    [SerializeField] private RectTransform _rightBack;
    [SerializeField] private RectTransform _dish;
    [SerializeField] private RectTransform _specTarget;
    [SerializeField] private RectTransform _spec;
    [SerializeField] private RectTransform _dishTarget;
    [SerializeField] private RectTransform _dishPos;
    private bool _isLeftAnimating = false;
    private bool _isCenterAnimating = false;
    private bool _isRightAnimating = false;
    private bool _isSpecAimating = false;
    private bool _isDishAimating = false;
    private Vector3 _rt=new();
    private const float _START_TIME = 1.0f;
    private float _time = 0.0f;
    private float _speed = 20;
    private const float _BPM = 132;
    private const float _BPM_START_TIME = 2.8f;
    private float _bpmTime = 0.0f;
    private float _rhythmTime = 0.0f;
    private float _rhythm = 0.0f;
    [SerializeField] private RectTransform _beat_eat;
    private Vector2 _beatScale;
    private Vector2 _beatReaetScale;
    private Vector2 _beatTargetScale=new(1.1f,1.1f);
    private bool _isScale = false;
    private const float _scaleTime = 15.0f;

    private void Start()
    {
        _rhythmTime = 60.0f/_BPM;
        _beatScale = _beatReaetScale = _beat_eat.localScale;
        _rt=_leftBack.localPosition;
    }

    private void Update()
    {
        BackImageAnimation();
        _bpmTime += Time.deltaTime;
        if (_bpmTime < _BPM_START_TIME) return;
        BpmScaleAnimation();
    }

    private void BpmScaleAnimation()
    {
        _rhythm += Time.deltaTime;

        if (_rhythm >= _rhythmTime / 4 && !_isScale)
        {
            _beatScale = _beatTargetScale;
            _isScale=true;
        }
        if (_rhythm >= _rhythmTime / 3)
        {
            _beatScale = _beatReaetScale;
        }
        if (_rhythm >= _rhythmTime)
        {
            _isScale=false;
            _rhythm = 0f;
        }

        _beat_eat.localScale = Vector3.Lerp(_beat_eat.localScale,_beatScale,Time.deltaTime* _scaleTime);
    }
            private void BackImageAnimation()
    {
        if(_isSpecAimating) return;

        _time += Time.deltaTime;
        if(_time < _START_TIME) return;

        if (!_isLeftAnimating)
            LeftAnim();
        else if(!_isRightAnimating)
            RightAnim();
        else if(!_isCenterAnimating)
            CenterAnim();
        else
        {
            SpecAnim();
            DishAnim();
        }
    }

    private void LeftAnim()
    {
        _rt.x= Mathf.Lerp(_leftBack.localPosition.x,_target.localPosition.x,Time.deltaTime*_speed);
        _leftBack.localPosition= _rt;
        if ((_leftBack.localPosition - _target.localPosition).sqrMagnitude < 0.01f)
        {
            _rt=_rightBack.localPosition;
            _isLeftAnimating=true;
        }
    }

    private void RightAnim()
    {
        _rt.x = Mathf.Lerp(_rightBack.localPosition.x, _target.localPosition.x, Time.deltaTime * _speed);
        _rightBack.localPosition = _rt;
        if ((_rightBack.localPosition - _target.localPosition).sqrMagnitude < 0.01f)
        {
            _rt = _de.localPosition;
            _isRightAnimating = true;
        }
    }
    private void CenterAnim()
    {
        _rt.y = Mathf.Lerp(_de.localPosition.y, _target.localPosition.y, Time.deltaTime * _speed);
        _de.localPosition = _rt;
        if ((_de.localPosition - _target.localPosition).sqrMagnitude < 0.01f)
        {
            _rt = _spec.localPosition;
            _isCenterAnimating = true;
            _speed = 10f;
        }
    }

    private void SpecAnim()
    {
        _rt.x = Mathf.Lerp(_spec.localPosition.x, _specTarget.localPosition.x, Time.deltaTime * _speed);
        _spec.localPosition = _rt;
        if ((_spec.localPosition - _specTarget.localPosition).sqrMagnitude < 0.01f&&_isDishAimating)
        {

            _isSpecAimating = true;
        }
    }
    private void DishAnim()
    {
        _rt.x = Mathf.Lerp(_dish.localPosition.x, _dishTarget.localPosition.x, Time.deltaTime * _speed);
        _dish.localPosition = _rt;
        if ((_dish.localPosition - _dishTarget.localPosition).sqrMagnitude < 0.01f)
        {

            _isDishAimating = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStartMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _titleMusic;

    private float _time = 0f;
    private const float _startTime = 2.8f;

    private void Update()
    {
        if(_titleMusic.isPlaying) return;
        _time += Time.deltaTime;
        if (_time < _startTime) return;
        _titleMusic.Play();
    }
}

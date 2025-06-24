using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JacketRotation : MonoBehaviour
{
    private RectTransform _jacket;
    private float _speed = 10.0f;
    private int _musicNumber = 0;
    [SerializeField] private MusicDataBase _musicDataBase;
    // Start is called before the first frame update
    void Start()
    {
        _jacket = GetComponent<RectTransform>();
        _musicNumber=MusicManager.instance.GetSelectMusicNumber();
    }

    // Update is called once per frame
    void Update()
    {
        _musicNumber=MusicManager.instance.GetSelectMusicNumber();
        _jacket.Rotate(0f, 0f, Time.deltaTime * (_speed * ( _musicDataBase.musicData[_musicNumber].BPM/60)));
    }
}

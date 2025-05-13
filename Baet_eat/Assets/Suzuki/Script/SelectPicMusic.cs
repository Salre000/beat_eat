using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPicMusic : MonoBehaviour
{
    // ���ݑI�΂�Ă���J�[�h�����

    [SerializeField] private Transform _selectBox;
    private List<GameObject> _musicCard = new(MusicManager._CAPACITY);

    private void Start()
    {
        _musicCard=MusicManager.instance.GetMusicCards();
    }

    private void Update()
    {
        PicUpCard();
    }

    private void PicUpCard()
    {
        int selectNumber = 0;
        // �I�΂�Ă�����̂����
        foreach(GameObject musicCard in _musicCard)
        {
            if ((musicCard.transform.position - _selectBox.position).sqrMagnitude < 10)
            {
                MusicManager.instance.SetSelectMusicNumer(selectNumber);
            }
            selectNumber++;
        }
    }
}

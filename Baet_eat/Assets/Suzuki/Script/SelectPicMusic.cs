using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPicMusic : MonoBehaviour
{
    // ���ݑI�΂�Ă���J�[�h�����

    // �ȃJ�[�h
    [SerializeField] private Transform _musicSelectBox;
    private List<GameObject> _musicCard = new(MusicManager.CAPACITY);

    // �X�L���J�[�h
    [SerializeField] private Transform _skillSelectBox;
    private List<GameObject> _skillCard = new(SkillManager.SKILLLIST_CAPACITY);

    private void Start()
    {
        _musicCard = MusicManager.instance.GetMusicCards();
        _skillCard = SkillManager.instance.GetSkillCards();
    }

    private void Update()
    {
        PicUpMusicCard();
        PicUpSkillCard();
    }

    private void PicUpMusicCard()
    {
        int selectNumber = 0;
        // �ǂ̋Ȃ��I�΂�Ă�����̂����
        foreach (GameObject musicCard in _musicCard)
        {
            if ((musicCard.transform.position - _musicSelectBox.position).sqrMagnitude < 10)
            {
                MusicManager.instance.SetSelectMusicNumer(selectNumber);
            }
            selectNumber++;
        }
    }
    private void PicUpSkillCard()
    {
        int selectNumber = 0;
        // �ǂ̃X�L�����I�΂�Ă�����̂����
        foreach (GameObject skillCard in _skillCard)
        {
            if ((skillCard.transform.position - _skillSelectBox.position).sqrMagnitude < 10)
            {
                SkillManager.instance.SetSelectedSkillID(selectNumber);
            }
            selectNumber++;
        }
    }
}

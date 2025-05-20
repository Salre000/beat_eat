using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPicMusic : MonoBehaviour
{
    // 現在選ばれているカードを特定

    // 曲カード
    [SerializeField] private Transform _musicSelectBox;
    private List<GameObject> _musicCard = new(MusicManager.CAPACITY);

    // スキルカード
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
        // どの曲か選ばれているものを特定
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
        // どのスキルか選ばれているものを特定
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public static class SelectPicSnap
{
    #region MusicCard
    public static void MusicPicMuve(float centerY, RectTransform content,
        float snapSpeed,bool isDragging)
    {
        // �������Ȃ𒆉��ɃX�i�b�v������
        // ���S�Ƃ̍��������߂Ă��̕������ړ�
        RectTransform closest = MusicManager.instance.GetClosest();
        float delta = centerY - closest.position.y;
                // Content�S�̂̈ʒu�𒲐����ăX�i�b�v
        Vector3 newPos = content.localPosition + new Vector3(0, delta, 0);
        content.localPosition = Vector3.Lerp(content.localPosition, newPos, Time.deltaTime * snapSpeed);
        if(!MusicManager.instance.IsSelected())return;
        float value = (content.localPosition - newPos).sqrMagnitude;
        if (value <= 5f && value >= 5f || isDragging)
            MusicManager.instance.SetSelected(false);
    }

    public static void MusicSelectCard(RectTransform targetPic, RectTransform content)
    {
        List<Button> _buttonCards = new (MusicManager.CAPACITY);
        int index = 0;
        foreach (GameObject gameObject in MusicManager.instance.GetMusicCards())
        {
            int buttonIndex = index;
            _buttonCards.Add(gameObject.GetComponent<Button>());
            _buttonCards[index].onClick.AddListener(() => OnMusicButton(buttonIndex, targetPic,content));
            index++;
        }
    }

    // index�ɂ͂ǂ̋Ȃ������ꂽ���̐��l�������Ă���
    public static void OnMusicButton(int index, RectTransform targetPic, RectTransform content)
    {
        MusicManager.instance.SetClosest(null);
        float minDist = float.MaxValue;
        // ���S�ƂȂ�w�W��Y���W���擾
        float centerY = targetPic.position.y;

        int count = 0;
        foreach (RectTransform child in content)
        {
            if (count != index)
            {
                count++;
                continue;
            }
            float dist = Mathf.Abs(centerY - child.position.y);
            if (dist < minDist)
            {
                minDist = dist;
                MusicManager.instance.SetClosest(child);
            }
            break;
        }

        MusicManager.instance.SetSelected(true);
    }
    #endregion

    #region SkillCard
    public static void SkillPicMuve(float centerY, RectTransform content,
    float snapSpeed, bool isDragging)
    {
        // �������Ȃ𒆉��ɃX�i�b�v������
        // ���S�Ƃ̍��������߂Ă��̕������ړ�
        RectTransform closest = SkillManager.instance.GetClosest();
        float delta = centerY - closest.position.y;
        // Content�S�̂̈ʒu�𒲐����ăX�i�b�v
        Vector3 newPos = content.localPosition + new Vector3(0, delta, 0);
        content.localPosition = Vector3.Lerp(content.localPosition, newPos, /*Time.deltaTime * */snapSpeed);
        if (!SkillManager.instance.IsSelected()) return;
        float value = (content.localPosition - newPos).sqrMagnitude;
        if (value <= 5f && value >= 5f || isDragging)
            SkillManager.instance.SetSelected(false);
    }

    public static void SkillSelectCard(RectTransform targetPic, RectTransform content)
    {
        List<Button> _buttonCards = new(SkillManager.SKILLLIST_CAPACITY);

        int index = 0;
        foreach (GameObject gameObject in SkillManager.instance.GetSkillCards())
        {
            int buttonIndex = index;
            _buttonCards.Add(gameObject.GetComponent<Button>());
            _buttonCards[index].onClick.AddListener(() => OnSkillButton(buttonIndex, targetPic, content));
            index++;
        }
    }

    // index�ɂ͂ǂ̋Ȃ������ꂽ���̐��l�������Ă���
    public static void OnSkillButton(int index, RectTransform targetPic, RectTransform content)
    {
        SkillManager.instance.SetClosest(null);
        float minDist = float.MaxValue;
        // ���S�ƂȂ�w�W��Y���W���擾
        float centerY = targetPic.position.y;

        int count = 0;
        foreach (RectTransform child in content)
        {
            if (count != index)
            {
                count++;
                continue;
            }
            float dist = Mathf.Abs(centerY - child.position.y);
            if (dist < minDist)
            {
                minDist = dist;
                SkillManager.instance.SetClosest(child);
            }
            break;
        }
        SkillManager.instance.SetSelected(true);
    }
    #endregion
}

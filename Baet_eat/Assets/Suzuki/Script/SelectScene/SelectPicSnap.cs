using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public static class SelectPicSnap
{
    #region MusicCard
    public static void MusicPicMuve(float centerY, RectTransform content,
        float snapSpeed,bool isDragging)
    {
        // 見つけた曲を中央にスナップさせる
        // 中心との差分を求めてその分だけ移動
        RectTransform closest = MusicManager.instance.GetClosest();
        float delta = centerY - closest.position.y;
                // Content全体の位置を調整してスナップ
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

    // indexにはどの曲が押されたかの数値を持っている
    public static void OnMusicButton(int index, RectTransform targetPic, RectTransform content)
    {
        MusicManager.instance.SetClosest(null);
        float minDist = float.MaxValue;
        // 中心となる指標のY座標を取得
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
        // 見つけた曲を中央にスナップさせる
        // 中心との差分を求めてその分だけ移動
        RectTransform closest = SkillManager.instance.GetClosest();
        float delta = centerY - closest.position.y;
        // Content全体の位置を調整してスナップ
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

    // indexにはどの曲が押されたかの数値を持っている
    public static void OnSkillButton(int index, RectTransform targetPic, RectTransform content)
    {
        SkillManager.instance.SetClosest(null);
        float minDist = float.MaxValue;
        // 中心となる指標のY座標を取得
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPic : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 1f;      // Lerpにかける時間
    private bool isDragging = false;
    float minDist = float.MaxValue;
    // 中心となる指標のY座標を取得
    float centerY = 0;
    [SerializeField] RectTransform targetPic;
    private List<GameObject> _skillCards = new(SkillManager.SKILLLIST_CAPACITY);

    private void Start()
    {
        _skillCards = SkillManager.instance.GetSkillCards();
        SelectPicSnap.SkillSelectCard(targetPic, content);
    }
    void Update()
    {
        Debug.Log(isDragging);
        if (SkillManager.instance.IsSelected())
        {
            SelectPicSnap.SkillPicMuve(centerY, content, snapSpeed, isDragging);
            return;
        }
        // ドラッグしておらず、スクロール速度が遅くなったらスナップ開始
        if (!isDragging && scrollRect.velocity.magnitude < 1000f)
        {
            SkillManager.instance.SetClosest(null);
            minDist = float.MaxValue;
            // 中心となる指標のY座標を取得
            centerY = targetPic.position.y;
            // 一番中心に近い曲を探す
            foreach (RectTransform child in content)
            {
                float dist = Mathf.Abs(centerY - child.position.y);
                if (dist < minDist)
                {
                    minDist = dist;
                    SkillManager.instance.SetClosest(child);
                }
            }
            // 見つけた曲を中央にスナップさせる
            if (SkillManager.instance.GetClosest() != null)
            {
                SelectPicSnap.SkillPicMuve(centerY, content, snapSpeed, isDragging);
            }
        }
    }

    public void BegingEvent()
    {
        isDragging = true;
    }

    public void EndEvent()
    {
        isDragging = false;
    }

}

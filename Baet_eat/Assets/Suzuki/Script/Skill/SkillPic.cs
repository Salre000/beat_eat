using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPic : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 10f;      // Lerpにかける時間
    private bool isDragging = false;

    [SerializeField] RectTransform pic;
    void Update()
    {
        // ドラッグしておらず、スクロール速度が遅くなったらスナップ開始
        if (!isDragging && scrollRect.velocity.magnitude < 10f)
        {
            RectTransform closest = null;
            float minDist = float.MaxValue;
            // 中心となる指標のY座標を取得
            float centerY = pic.position.y;
            // 一番中心に近い曲を探す
            foreach (RectTransform child in content)
            {
                float dist = Mathf.Abs(centerY - child.position.y);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = child;
                }
            }
            // 見つけた曲を中央にスナップさせる
            if (closest != null)
            {
                // 中心との差分を求めてその分だけ移動
                float delta = centerY - closest.position.y;
                // Content全体の位置を調整してスナップ
                Vector2 newPos = content.localPosition + new Vector3(0, delta, 0);
                content.localPosition = Vector3.Lerp(content.localPosition, newPos, snapSpeed);
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

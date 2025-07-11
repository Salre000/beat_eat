using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SnapPic : MonoBehaviour
{
    // 斜めに並べつつ中央にあるカードをピックアップ

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 30f;      // Lerpにかける時間
    private bool isDragging = false;

    float minDist = float.MaxValue;
    // 中心となる指標のY座標を取得
    float centerY = 0;

    [SerializeField] RectTransform targetPic;

    private void Start()
    {
        SelectPicSnap.MusicSelectCard(targetPic, content);
    }


    void Update()
    {
        SnapCard();
    }

    private void SnapCard()
    {
        if (MusicManager.instance.IsSelected())
        {
            SelectPicSnap.MusicPicMuve(centerY, content, snapSpeed, isDragging);
            return;
        }
        // ドラッグしておらず、スクロール速度が遅くなったらスナップ開始
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
        {

            MusicManager.instance.SetClosest(null);
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
                    MusicManager.instance.SetClosest(child);
                }
            }
            // 見つけた曲を中央にスナップさせる
            if (MusicManager.instance.GetClosest() != null)
            {
                SelectPicSnap.MusicPicMuve(centerY, content, snapSpeed, isDragging);
            }
        }
    }

    // ドラッグ開始
    public void BegingEvent()
    {
        isDragging = true;
    }
    // ドラッグ終了
    public void EndEvent()
    {
        isDragging = false;
    }

    // 中央拡大
    void LateUpdate()
    {
        float centerY = targetPic.position.y;
        foreach (RectTransform card in content)
        {
            // 距離を正規化
            float dist = Mathf.Abs(centerY - card.position.y);
            float scale = Mathf.Clamp(1.0f - dist / 50f, 0.7f, 1.0f); // 距離に応じてスケールチェンジ
            card.localScale = Vector3.Lerp(card.localScale, new Vector3(scale, scale, scale), 10f);
            UpdateCardPosition(card, centerY);
        }
    }

    void UpdateCardPosition(RectTransform card, float centerY)
    {
        float diffY = card.position.y - centerY;
        // 縦スクロールに応じてX位置も変化（斜め移動）
        float xOffset = diffY * -3.0f; // どれくらい斜めにするか
        Vector2 targetPos = card.anchoredPosition;
        targetPos.x = xOffset;
        card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPos, 10f);
    }

}
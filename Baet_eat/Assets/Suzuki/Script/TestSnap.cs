using UnityEngine.UI;
using UnityEngine;

public class TestSnap : MonoBehaviour
{
    // 斜めに並べつつ中央にあるカードをピックアップ

    public ScrollRect scrollRect;       // 対象のScrollRect
    public RectTransform content;      // コンテンツ（カードの親）
    public float snapSpeed = 10f;      // スナップ速度
    private bool isDragging = false;   // ユーザーがドラッグ中かどうか

    [SerializeField] RectTransform pic;
    void Update()
    {
        // ドラッグしておらず、スクロール速度が遅くなったらスナップ開始
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
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
    // ドラッグ開始
    public void OnBeginDrag()
    {
        isDragging = true;
    }
    // ドラッグ終了
    public void OnEndDrag()
    {
        isDragging = false;
    }

    // 中央拡大
    void LateUpdate()
    {
        float centerY = pic.position.y;
        foreach (RectTransform card in content)
        {
            // 距離を正規化
            float dist = Mathf.Abs(centerY - card.position.y);
            float scale = Mathf.Clamp(1.0f - dist / 50f, 0.7f, 1.0f); // 距離に応じてスケール
            card.localScale = Vector3.Lerp(card.localScale, new Vector3(scale, scale, scale),  10f);
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
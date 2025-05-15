using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class SnapPic : MonoBehaviour
{
    // 斜めに並べつつ中央にあるカードをピックアップ

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 30f;      // Lerpにかける時間
    private bool isDragging = false;

    // ボタンが押されたら中央に持ってくる
    private bool _isSelected = false;

    private List<Button> _buttonCards = new(MusicManager._CAPACITY);

    RectTransform closest = null;
    float minDist = float.MaxValue;
    // 中心となる指標のY座標を取得
    float centerY = 0;

    [SerializeField] RectTransform pic;
    private void Start()
    {
        int index = 0;
        foreach (GameObject gameObject in MusicManager.instance.GetMusicCards())
        {
            int buttonIndex = index;
            _buttonCards.Add(gameObject.GetComponent<Button>());
            _buttonCards[index].onClick.AddListener(() => OnButton(buttonIndex));
            index++;
        }
    }


    void Update()
    {
        if (_isSelected)
            SnapMuve();   
        else
            SnapCard();
    }

    private void SnapCard()
    {
        // ドラッグしておらず、スクロール速度が遅くなったらスナップ開始
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
        {
            closest = null;
            minDist = float.MaxValue;
            // 中心となる指標のY座標を取得
            centerY = pic.position.y;
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
        float centerY = pic.position.y;
        foreach (RectTransform card in content)
        {
            // 距離を正規化
            float dist = Mathf.Abs(centerY - card.position.y);
            float scale = Mathf.Clamp(1.0f - dist / 50f, 0.7f, 1.0f); // 距離に応じてスケール
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

    private void SnapMuve()
    {
        // 見つけた曲を中央にスナップさせる
        if (closest != null)
        {
            float delta = centerY - closest.position.y;
            Vector3 newPos = content.localPosition + new Vector3(0, delta, 0);
            content.localPosition = Vector3.Lerp(content.localPosition, newPos, Time.deltaTime * snapSpeed);
            if((content.localPosition - newPos).sqrMagnitude <= 5f&& (content.localPosition - newPos).sqrMagnitude >= 5f || isDragging) _isSelected = false;
        }
    }

    // indexにはどの曲が押されたかの数値を持っている
    public void OnButton(int index)
    {
        closest = null;
        minDist = float.MaxValue;
        // 中心となる指標のY座標を取得
        centerY = pic.position.y;

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
                closest = child;
            }
            break;
        }

        _isSelected = true;
        Debug.Log(index);
    }
}
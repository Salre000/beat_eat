using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

public class SnapPic : MonoBehaviour
{
    // �΂߂ɕ��ׂ����ɂ���J�[�h���s�b�N�A�b�v

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 30f;      // Lerp�ɂ����鎞��
    private bool isDragging = false;

    // �{�^���������ꂽ�璆���Ɏ����Ă���
    private bool _isSelected = false;

    private List<Button> _buttonCards = new(MusicManager._CAPACITY);

    RectTransform closest = null;
    float minDist = float.MaxValue;
    // ���S�ƂȂ�w�W��Y���W���擾
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
        // �h���b�O���Ă��炸�A�X�N���[�����x���x���Ȃ�����X�i�b�v�J�n
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
        {
            closest = null;
            minDist = float.MaxValue;
            // ���S�ƂȂ�w�W��Y���W���擾
            centerY = pic.position.y;
            // ��Ԓ��S�ɋ߂��Ȃ�T��
            foreach (RectTransform child in content)
            {
                float dist = Mathf.Abs(centerY - child.position.y);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = child;
                }
            }
            // �������Ȃ𒆉��ɃX�i�b�v������
            if (closest != null)
            {
                // ���S�Ƃ̍��������߂Ă��̕������ړ�
                float delta = centerY - closest.position.y;
                // Content�S�̂̈ʒu�𒲐����ăX�i�b�v
                Vector2 newPos = content.localPosition + new Vector3(0, delta, 0);
                content.localPosition = Vector3.Lerp(content.localPosition, newPos, snapSpeed);
            }
        }
    }

    // �h���b�O�J�n
    public void BegingEvent()
    {
        isDragging = true;
    }
    // �h���b�O�I��
    public void EndEvent()
    {
        isDragging = false;
    }

    // �����g��
    void LateUpdate()
    {
        float centerY = pic.position.y;
        foreach (RectTransform card in content)
        {
            // �����𐳋K��
            float dist = Mathf.Abs(centerY - card.position.y);
            float scale = Mathf.Clamp(1.0f - dist / 50f, 0.7f, 1.0f); // �����ɉ����ăX�P�[��
            card.localScale = Vector3.Lerp(card.localScale, new Vector3(scale, scale, scale), 10f);
            UpdateCardPosition(card, centerY);
        }
    }

    void UpdateCardPosition(RectTransform card, float centerY)
    {
        float diffY = card.position.y - centerY;
        // �c�X�N���[���ɉ�����X�ʒu���ω��i�΂߈ړ��j
        float xOffset = diffY * -3.0f; // �ǂꂭ�炢�΂߂ɂ��邩
        Vector2 targetPos = card.anchoredPosition;
        targetPos.x = xOffset;
        card.anchoredPosition = Vector2.Lerp(card.anchoredPosition, targetPos, 10f);
    }

    private void SnapMuve()
    {
        // �������Ȃ𒆉��ɃX�i�b�v������
        if (closest != null)
        {
            float delta = centerY - closest.position.y;
            Vector3 newPos = content.localPosition + new Vector3(0, delta, 0);
            content.localPosition = Vector3.Lerp(content.localPosition, newPos, Time.deltaTime * snapSpeed);
            if((content.localPosition - newPos).sqrMagnitude <= 5f&& (content.localPosition - newPos).sqrMagnitude >= 5f || isDragging) _isSelected = false;
        }
    }

    // index�ɂ͂ǂ̋Ȃ������ꂽ���̐��l�������Ă���
    public void OnButton(int index)
    {
        closest = null;
        minDist = float.MaxValue;
        // ���S�ƂȂ�w�W��Y���W���擾
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
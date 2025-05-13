using UnityEngine.UI;
using UnityEngine;

public class TestSnap : MonoBehaviour
{
    // �΂߂ɕ��ׂ����ɂ���J�[�h���s�b�N�A�b�v

    public ScrollRect scrollRect;       // �Ώۂ�ScrollRect
    public RectTransform content;      // �R���e���c�i�J�[�h�̐e�j
    public float snapSpeed = 10f;      // �X�i�b�v���x
    private bool isDragging = false;   // ���[�U�[���h���b�O�����ǂ���

    [SerializeField] RectTransform pic;
    void Update()
    {
        // �h���b�O���Ă��炸�A�X�N���[�����x���x���Ȃ�����X�i�b�v�J�n
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
        {
            RectTransform closest = null;
            float minDist = float.MaxValue;
            // ���S�ƂȂ�w�W��Y���W���擾
            float centerY = pic.position.y;
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
    public void OnBeginDrag()
    {
        isDragging = true;
    }
    // �h���b�O�I��
    public void OnEndDrag()
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
            card.localScale = Vector3.Lerp(card.localScale, new Vector3(scale, scale, scale),  10f);
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
}
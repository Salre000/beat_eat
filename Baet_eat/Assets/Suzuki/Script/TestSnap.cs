using UnityEngine.UI;
using UnityEngine;

public class TestSnap : MonoBehaviour
{
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
            // �r���[�|�[�g�̒��SY���W���擾
            float centerY = pic.position.y;/*scrollRect.viewport.position.y;*/
            // ��Ԓ��S�ɋ߂��q�v�f�i�ȃJ�[�h�j��T��
            foreach (RectTransform child in content)
            {
                float dist = Mathf.Abs(centerY - child.position.y);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = child;
                }
            }
            // �������J�[�h�𒆉��ɃX�i�b�v������
            if (closest != null)
            {
                // ���S�Ƃ̍��������߂āA���̕������ړ�����
                float delta = centerY - closest.position.y;
                // Content�S�̂̈ʒu�𒲐����ăX�i�b�v
                Vector2 newPos = content.localPosition + new Vector3(0, delta, 0);
                content.localPosition = Vector3.Lerp(content.localPosition, newPos, /*Time.deltaTime * */snapSpeed);
            }
        }
    }
    // �h���b�O�J�n���ɌĂ΂��
    public void OnBeginDrag()
    {
        isDragging = true;
    }
    // �h���b�O�I�����ɌĂ΂��
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

    void UpdateCardPosition(RectTransform item, float centerY)
    {
        float diffY = item.position.y - centerY;
        // �c�X�N���[���ɉ�����X�ʒu���ω��i�΂߈ړ��j
        float xOffset = diffY * -3.0f; // �X���W��
        Vector2 targetPos = item.anchoredPosition;
        targetPos.x = xOffset;
        item.anchoredPosition = Vector2.Lerp(item.anchoredPosition, targetPos, /*Time.deltaTime * */10f);
    }
}
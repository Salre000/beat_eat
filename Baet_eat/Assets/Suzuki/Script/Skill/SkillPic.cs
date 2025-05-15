using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPic : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 10f;      // Lerp�ɂ����鎞��
    private bool isDragging = false;

    [SerializeField] RectTransform pic;
    void Update()
    {
        // �h���b�O���Ă��炸�A�X�N���[�����x���x���Ȃ�����X�i�b�v�J�n
        if (!isDragging && scrollRect.velocity.magnitude < 10f)
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

    public void BegingEvent()
    {
        isDragging = true;
    }

    public void EndEvent()
    {
        isDragging = false;
    }

}

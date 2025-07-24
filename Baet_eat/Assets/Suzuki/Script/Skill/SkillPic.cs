using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPic : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 120f;      // Lerp�ɂ����鎞��
    private bool isDragging = false;
    float minDist = float.MaxValue;
    // ���S�ƂȂ�w�W��Y���W���擾
    float centerY = 0;
    [SerializeField] RectTransform targetPic;

    private void Start()
    {
        SelectPicSnap.SkillSelectCard(targetPic, content);
    }
    void Update()
    {
        if (SkillManager.instance.IsSelected())
        {
            SelectPicSnap.SkillPicMuve(centerY, content, snapSpeed, isDragging);
            return;
        }
        // �h���b�O���Ă��炸�A�X�N���[�����x���x���Ȃ�����X�i�b�v�J�n
        if (!isDragging && scrollRect.velocity.magnitude < 10000f)
        {
            SkillManager.instance.SetClosest(null);
            minDist = float.MaxValue;
            // ���S�ƂȂ�w�W��Y���W���擾
            centerY = targetPic.position.y;
            // ��Ԓ��S�ɋ߂��Ȃ�T��
            foreach (RectTransform child in content)
            {
                float dist = Mathf.Abs(centerY - child.position.y);
                if (dist < minDist)
                {
                    minDist = dist;
                    SkillManager.instance.SetClosest(child);
                }
            }
            // �������Ȃ𒆉��ɃX�i�b�v������
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

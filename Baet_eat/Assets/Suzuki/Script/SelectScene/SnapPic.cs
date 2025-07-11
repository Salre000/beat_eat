using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SnapPic : MonoBehaviour
{
    // �΂߂ɕ��ׂ����ɂ���J�[�h���s�b�N�A�b�v

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    private float snapSpeed = 30f;      // Lerp�ɂ����鎞��
    private bool isDragging = false;

    float minDist = float.MaxValue;
    // ���S�ƂȂ�w�W��Y���W���擾
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
        // �h���b�O���Ă��炸�A�X�N���[�����x���x���Ȃ�����X�i�b�v�J�n
        if (!isDragging && scrollRect.velocity.magnitude < 100f)
        {

            MusicManager.instance.SetClosest(null);
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
                    MusicManager.instance.SetClosest(child);
                }
            }
            // �������Ȃ𒆉��ɃX�i�b�v������
            if (MusicManager.instance.GetClosest() != null)
            {
                SelectPicSnap.MusicPicMuve(centerY, content, snapSpeed, isDragging);
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
        float centerY = targetPic.position.y;
        foreach (RectTransform card in content)
        {
            // �����𐳋K��
            float dist = Mathf.Abs(centerY - card.position.y);
            float scale = Mathf.Clamp(1.0f - dist / 50f, 0.7f, 1.0f); // �����ɉ����ăX�P�[���`�F���W
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

}
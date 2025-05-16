using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckScrollDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // �����͕K�v�Ȃ��̂��������Α��v
    [SerializeField] private SkillPic skillPic;
    [SerializeField] private SnapPic snapPic;
    public void OnBeginDrag(PointerEventData eventData)
    {
        skillPic?.BegingEvent();
        snapPic?.BegingEvent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        skillPic?.EndEvent();
        snapPic?.EndEvent();
    }
}

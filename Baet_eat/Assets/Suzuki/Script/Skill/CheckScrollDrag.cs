using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckScrollDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // これらは必要なものだけ入れれば大丈夫
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

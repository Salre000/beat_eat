using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSkillDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private SkillPic pic;
    public void OnBeginDrag(PointerEventData eventData)
    {
        pic?.BegingEvent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pic?.EndEvent();
    }
}

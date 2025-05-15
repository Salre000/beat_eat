using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class MusicDragCheck : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private SnapPic pic;
    public void OnBeginDrag(PointerEventData eventData)
    {
        pic?.BegingEvent();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pic?.EndEvent();
    }
}

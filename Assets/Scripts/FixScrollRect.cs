using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixScrollRect: MonoBehaviour, IBeginDragHandler,  IDragHandler, IEndDragHandler, IScrollHandler
{
    public ScrollRect MainScrollRect;
 
 
    public void OnBeginDrag(PointerEventData eventData)
    {
        MainScrollRect.OnBeginDrag(eventData);
    }
 
 
    public void OnDrag(PointerEventData eventData)
    {
        MainScrollRect.OnDrag(eventData);
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        MainScrollRect.OnEndDrag(eventData);
    }
 
 
    public void OnScroll(PointerEventData data)
    {
        MainScrollRect.OnScroll(data);
    }
 
 
}

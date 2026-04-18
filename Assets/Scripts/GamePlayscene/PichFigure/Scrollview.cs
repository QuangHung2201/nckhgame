using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scrollview : MonoBehaviour , IEndDragHandler
{
    public void OnEndDrag(PointerEventData eventData)
    {
        PichFigure.instance.Minfigurenear();
    }    
}

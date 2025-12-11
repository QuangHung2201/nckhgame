using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInHelper : MonoBehaviour   // quan trọng : set theo anchor để scale theo các loại tỷ lệ màn hình  
                                           // chú ý : gameobject phải set anchor theo hướng bay ( điểm neo )
{
    public bool formLeft;
    private Vector2 targetPos;
    private RectTransform rectOjbect;

    private void Awake()
    {
        rectOjbect = GetComponent<RectTransform>();
        targetPos = rectOjbect.anchoredPosition;
        animationFly();
    }

    public void animationFly()
    {
        Vector2 startPos = targetPos + (formLeft ? Vector2.left * 500 : Vector2.right * 500);
        rectOjbect.anchoredPosition = startPos;
        rectOjbect.DOAnchorPos(targetPos, 0.5f).SetEase(Ease.OutBack);
    }    
}

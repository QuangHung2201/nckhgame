using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInHelper : MonoBehaviour     
                                           
{   RectTransform rectTransform;
    public bool leftform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        animationInFly();
    }
    public void animationInFly() // di chuyển "vào" trái hoặc phải
    {
        Vector2 originPos = rectTransform.anchoredPosition;
        Vector2 startPos = originPos + (leftform ? Vector2.left * 500 : Vector2.right * 500);
        rectTransform.anchoredPosition = startPos;
        rectTransform.DOAnchorPos(originPos, 0.5f).SetEase(Ease.OutBack);
    }    
}

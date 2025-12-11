using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseAni : MonoBehaviour
{
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

    }

    private void OnEnable()
    {
        aniOpen();
    }

    public void aniOpen()
    {
        Vector3 startScale = rect.localScale;
        rect.localScale = Vector3.zero;
        rect.DOScale(startScale, 0.35f).SetEase(Ease.OutBack);
    }    
    public void aniClose()
    {
        rect.DOScale(0f, 0.35f)
        .SetEase(Ease.InOutSine)
        .OnComplete(() => Destroy(gameObject));
    }    
}

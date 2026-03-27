using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignInhelper : MonoBehaviour
{
    Tween scaleTween;
    private RectTransform m_RectTransform;

    void Start()
    {
    }
    private void OnEnable()
    {
        m_RectTransform = GetComponent<RectTransform>();
        scaleSizeAni();
    }
    public void scaleSizeAni()
    {
        scaleTween?.Kill();
        Vector3 targetSign = m_RectTransform.localScale;
        m_RectTransform.localScale = targetSign * 1.4f;
        scaleTween = m_RectTransform.DOScale(targetSign, 0.5f).SetEase(Ease.InBack);
    }    
     
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFly : MonoBehaviour
{
    void Start()
    { 
    }

    public void Coinfly(Transform targetpos,int i) // hàm di chuyển
    {
        Sequence seq = DOTween.Sequence();
        Vector2 Posrandom = new Vector2(Random.Range(0,0.5f), Random.Range(0,0.5f));

        seq.Append(gameObject.transform.DOMove(targetpos.position * Posrandom,0.5f).SetEase(Ease.OutCubic));
        seq.AppendCallback(()=>
        {
            transform.DOScale(0.5f, 0.9f);
            CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
            cg.DOFade(0.6f, 1.1f);
            transform
            .DOMove(targetpos.position, 0.9f)
            .SetEase(Ease.OutQuad)
            .SetDelay(i * 0.1f);
        }
        );
    }    
}

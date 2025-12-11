using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
    }

    public void Coinfly(Transform targetpos,int i) // hàm di chuyển
    {
        transform.DOScale(0.5f, 0.9f);
        CanvasGroup cg = gameObject.GetComponent<CanvasGroup>();
        cg.DOFade(0.6f, 1.1f);
        transform
        .DOMove(targetpos.position, 0.9f)
        .SetEase(Ease.OutQuad)
        .SetDelay(i * 0.1f);
    }    
}

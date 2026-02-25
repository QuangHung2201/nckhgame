using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class HelperCoinAni  // xử dụng hiệu ứng cho coin khi trong màn hình chính 
{

    public static void AnimateCoinIncrease(int from, int to, float duration)  // hàm hiệu ứng tăng coin trên popup
    {
        if( PopupCoin.instance != null )
        {
        PopupCoin.instance.poppubPar.transform.SetAsLastSibling();
        DOTween.To(() => from, x => {
            from = x;
            PopupCoin.instance.textcoin.text = from.ToString(); 
        }, to, duration).SetEase(Ease.OutQuad);
        }    
    }

    public static void flyToPopupCoin(GameObject coin) // hiệu ứng bay coin lên popup
    {
        if( PopupCoin.instance != null )
        {
            Vector2 up = new Vector2(coin.transform.position.x, coin.transform.position.y + 0.5f);
            Sequence seq =  DOTween.Sequence();
            seq.Append(coin.transform.DOMove(up, 0.4f));
            seq.Append(coin.transform
                .DOMove(PopupCoin.instance.poppubcoinTranf.position, 0.6f)
                .SetEase(Ease.OutQuad));
            seq.Join(coin.transform.DOScale(0.3f, 0.6f));
            seq.OnComplete(() => Object.Destroy(coin));
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBasket : MonoBehaviour
{
    public static CoinBasket Instance;
    public int numBer_Coin; // biến lưu số lượng coin tạm thời
    public TextMeshProUGUI textNumberCoin;
    void Start()
    {
        Instance = this;
        numBer_Coin = 0; 
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void upDataCoinBasket()
    {
        int numberCoinbefore = numBer_Coin;
        numBer_Coin +=  5;
        animUpdate(numberCoinbefore, numBer_Coin, 1f);
    }    

    public void animUpdate(int form , int to, float duration)
    {
        DOTween.To(() => form, x =>
        {
            form = x;
            textNumberCoin.text = form.ToString();
        },
        to, duration).SetEase(Ease.OutQuad);
    }    

    public void resetCoin()
    {
        int nb = PrefManager.PrefMoney.getNumberCoin();
        PrefManager.PrefMoney.SetNumberCoin(numBer_Coin + nb);
        numBer_Coin = 0;
        textNumberCoin.text = "" + numBer_Coin;
    }    
}

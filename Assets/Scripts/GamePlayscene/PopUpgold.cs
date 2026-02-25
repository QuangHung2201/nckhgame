using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpgold : MonoBehaviour
{
    public static PopUpgold Instance;
    public TextMeshProUGUI textNumberCoin;
    private int LastnumbCoin;
    public GameObject buttonContinue;
    public Transform targetCoin;
    public List<GameObject> coinflycl;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        coinflycl = new List<GameObject>();
        LastnumbCoin = CoinBasket.Instance.numBer_Coin;
        setTextPopupCoin();
        StartCoroutine(delayContinue()); // sau start 4s thì hiển thị
        flycoin();
        animation();
    }
    private void OnDisable()
    {
        Instance = null;    
    }
    public void setTextPopupCoin()
    {
        int coinSave = PrefManager.PrefMoney.getNumberCoin();
        int sumCoin = coinSave + LastnumbCoin;
        AnimateCoinIncrease(coinSave, sumCoin, 1.4f);
        PrefManager.PrefMoney.SetNumberCoin(sumCoin);
    }
    void AnimateCoinIncrease(int from, int to, float duration)  // hàm hiệu ứng tăng coin
    {
        DOTween.To(() => from, x => {
            from = x;
            textNumberCoin.text = from.ToString(); // đọc text lên trong từng fame
        }, to, duration).SetEase(Ease.OutQuad);
    }

    IEnumerator delayContinue()
    {
        yield return new WaitForSeconds(1.4f);
        buttonContinue.SetActive(true);
    }    
    public void flycoin()
    {
        GameObject coinFlyFrab = Resources.Load<GameObject>("sceneGameplay/CionFly");
        if(coinFlyFrab != null )
        {
            for(int i = 0; i<5 ; i++)
            {
                GameObject CoinClone = Instantiate(coinFlyFrab);
                CoinClone.transform.SetParent(transform, false);
                CoinClone.transform.position = new Vector3(0, 0, 0);
                CoinClone.GetComponent<CoinFly>().Coinfly(targetCoin,i);
                coinflycl.Add(CoinClone);
            }    
        }
        StartCoroutine(destroycoin());
    }   
    IEnumerator destroycoin()
    {
        yield return new WaitForSeconds(1.3f);
        for(int i = 0; i < coinflycl.Count; i++)
        {
            Destroy(coinflycl[i]);
        }
    } 
    
    public void animation()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOScale(1.2f, 0.1f).SetEase(Ease.OutBack));
        sq.AppendInterval(1.2f);
        sq.Append(transform.DOScale(1f,0.1f).SetEase(Ease.InBack));
        sq.OnComplete(() => sq.Kill()) ;
    }
}

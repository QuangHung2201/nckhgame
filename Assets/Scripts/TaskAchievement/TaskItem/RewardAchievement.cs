using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardAchievement : MonoBehaviour
{
    public Transform parentTrF;
    public Button buttonConT;

    private GameObject coinObj;
    private int numbCoinReW;

    [SerializeField] private CanvasGroup bg;

    private void OnEnable()
    {
        CreateUI(); // chỉ tạo 1 lần
        buttonConT.onClick.AddListener(EvenButtonConT);
    }
    private void OnDisable()
    {
        bg.alpha = 1.0f;
        buttonConT.onClick.RemoveAllListeners();
    }

    void CreateUI()
    {
        if (coinObj == null)
        {
            GameObject prefab = Resources.Load<GameObject>("PrefabsAchievement/item_coin");
            coinObj = Instantiate(prefab, parentTrF, false);
        }
    }

    public void SetCoin(int value)
    {
        numbCoinReW = value;

        if (coinObj != null)
        {
            coinObj.GetComponent<ItemReward>().setdata("coin", "coin", numbCoinReW);
        }
    }

    public void EvenButtonConT()
    {
        coinObj.GetComponent<ItemReward>().openLight();

        HelperCoinAni.flyToPopupCoin(coinObj);
        StartCoroutine(delay2());
    }

    IEnumerator delay2()
    {
        yield return new WaitForSeconds(1f);

        int numberBef = PrefManager.PrefMoney.getNumberCoin();
        int numberAfTer = numberBef + numbCoinReW;

        PrefManager.PrefMoney.SetNumberCoin(numberAfTer);
        HelperCoinAni.AnimateCoinIncrease(numberBef, numberAfTer, 0.5f);

        bg.DOFade(0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        PopupCoin.instance.poppubPar.transform.SetAsFirstSibling();
    }
}

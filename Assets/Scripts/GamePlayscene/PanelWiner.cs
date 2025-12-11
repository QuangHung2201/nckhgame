using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelWiner : MonoBehaviour
{
    public Button button_close;
    public Button button_claimx2;
    public TextMeshProUGUI textCoinClaim;
    public GameObject panelClaim;
    // Start is called before the first frame update
    void Start()
    {
        setTextCoin();
    }
    private void OnEnable()
    {
        button_close.onClick.AddListener(backMain);
    }
    private void OnDestroy()
    {
        button_close.onClick.RemoveAllListeners();
    }
    public void backMain()
    {
        panelClaim.SetActive(true);
    }  
    public void setTextCoin()
    {
        int number = CoinBasket.Instance.numBer_Coin;
        textCoinClaim.text = number.ToString() + "Xu";
    }    
}

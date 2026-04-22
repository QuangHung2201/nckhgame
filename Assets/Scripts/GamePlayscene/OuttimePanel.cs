using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OuttimePanel : MonoBehaviour
{
    public Button buttonmain;
    public Button buttonreset;
    public TextMeshProUGUI textNumBerCoin;

    private int numb;
    private void OnEnable()
    {
        numb = CoinBasket.Instance.numBer_Coin;
        buttonmain.onClick.AddListener(backmain);
        buttonreset.onClick.AddListener(resetquestions);
        setTextCoin();
    }
    private void OnDestroy()
    {
        buttonmain.onClick.RemoveAllListeners();
        buttonreset.onClick.RemoveAllListeners();
    }
    private void backmain()
    {
        ManagerSceneGame.Instance.backMain();
    }   
    private void resetquestions()
    {
        ManagerSceneGame.Instance.resetquestion();
        ManagerSceneGame.Instance.panel_fail.SetActive(false);
    }    
    public void setTextCoin()
    {
        textNumBerCoin.text = numb.ToString();
    }    
}

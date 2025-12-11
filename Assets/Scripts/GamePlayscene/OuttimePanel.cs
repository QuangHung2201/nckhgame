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
        buttonreset.onClick.AddListener(resetquestion);
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
    private void resetquestion()
    {
        string idlocation =  PrefManager.PrefSaveUserMap.GetUserLocationchoose();
        PrefManager.PrefSaveUserMap.SetUserQuestionLocationID(idlocation, 0);
        ManagerSceneGame.Instance.SetdataGameplay();
        ManagerSceneGame.Instance.panel_fail.SetActive(false);
    }    
    public void setTextCoin()
    {
        textNumBerCoin.text = numb.ToString();
    }    
}

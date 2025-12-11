using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupCoin : MonoBehaviour
{
    public TextMeshProUGUI textcoin;
    // Start is called before the first frame update
    void Start()
    {
        setTextCoin();
    }

    public void setTextCoin()
    {
        int numbercoin = PrefManager.PrefMoney.getNumberCoin();
        textcoin.text = numbercoin.ToString(); 
    }    
  
}

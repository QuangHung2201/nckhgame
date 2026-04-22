using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class panelCoin : MonoBehaviour
{
    public static panelCoin Instance;
    public Button button_close;
    public Button button_claimx2;
    public TextMeshProUGUI textCoinClaim;
    public int number;
    public GameObject panelClaim;

    void Start()
    {
        Instance = this;
        setTextCoin();
    }
    private void OnEnable()
    {
        button_close.onClick.AddListener(backMain);
    }
    private void OnDisable()
    {
        Instance = null;
    }
    private void OnDestroy()
    {
        button_close.onClick.RemoveAllListeners();
    }
    public void backMain()
    {
        panelClaim.SetActive(true);
    }
    public void SetCoin(int value)
    {
        number = value;
        setTextCoin();
    }
    public void setTextCoin()
    {
        textCoinClaim.text = number.ToString() + "Xu";
    }
}

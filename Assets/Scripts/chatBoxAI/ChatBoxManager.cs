using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxManager : MonoBehaviour
{
    [SerializeField] private Button btnShowHello;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnOpenChatbos;

    [SerializeField] private GameObject PopupShow;
    [SerializeField] private GameObject FeaTureChatboss;
    [SerializeField] private TextMeshProUGUI txt_popup;
    void Start()
    {
        btnShowHello.onClick.AddListener(ShowPopupHello);
        btnClose.onClick.AddListener(ClosePopupHello);
        btnOpenChatbos.onClick.AddListener(OpenFeaTureChatboss);
        eventtxt_popup();
    }

    private void OnDestroy()
    {
        btnShowHello.onClick.RemoveAllListeners();
        btnClose.onClick.RemoveAllListeners();
        btnOpenChatbos.onClick.RemoveAllListeners();
    }

    public void eventtxt_popup()
    {
        txt_popup.text = "Xin chào " + PrefManager.PrefProfiles.GetUserName()
            + " bạn có muôn tôi giúp gì không ?";
    }    
    public void ShowPopupHello()
    {
        PopupShow.SetActive(true);
    }    
    public void ClosePopupHello()
    {
        PopupShow.SetActive(false);
    } 
    public void OpenFeaTureChatboss()
    {
        FeaTureChatboss.SetActive(true);
        gameObject.SetActive(false);
    }    
}

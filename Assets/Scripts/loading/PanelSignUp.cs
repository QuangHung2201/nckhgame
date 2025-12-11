using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelSignUp : MonoBehaviour
{
    public Button button_continue;
    // Start is called before the first frame update
    void Start()
    {
        
    }
       
    public void listen_loadMain(Action action)
    {
        button_continue.onClick.AddListener(() => action()); // chuyển từ systemAction => unity Action để unity hiểu được hàm
    }
    private void OnDisable()
    {
        button_continue.onClick.RemoveAllListeners(); // tắt lắng nghe button khi không hoạt động
    }
    public void listeninputname(string name)
    {
        PrefManager.PrefProfiles.SetUserName(name);
        Debug.Log(PrefManager.PrefProfiles.GetUserName());
    }    
    public void listeninputbirth(string birth)
    {
        PrefManager.PrefProfiles.SetUserdateofbirth(birth);
        Debug.Log(PrefManager.PrefProfiles.GetUserdateofbirth());
    }    
}

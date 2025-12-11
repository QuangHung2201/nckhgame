using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfilePanel : MonoBehaviour
{
    public static UserProfilePanel instance;
    public TextMeshProUGUI textID;
    public TextMeshProUGUI textName;
    public Button buttonPanelRename;
    public Button button_close;
    void Start()
    {
        instance = this;
        setID();
    }
    public void setID() // HÀM SET DATA
    {
        textID.text = "ID: " + PrefManager.PrefProfiles.GetUserID();
        textName.text = "Name: " + PrefManager.PrefProfiles.GetUserName();
    } 
    public void closepanel()
    {
        MainEvent.instance.ClosePanel();
        gameObject.GetComponent<OpenCloseAni>().aniClose();
    }
    private void OnEnable()
    {
        button_close.onClick.AddListener(closepanel);
        buttonPanelRename.onClick.AddListener(openPanelrename);
    }
    private void OnDestroy()
    {
        button_close.onClick.RemoveAllListeners();
        buttonPanelRename.onClick.RemoveAllListeners();
        instance = null;
    }

    private void openPanelrename()
    {
        GameObject panelrename = Resources.Load<GameObject>("UserProfile/PanelRename");
        GameObject panelrenameClone = Instantiate(panelrename);
        panelrenameClone.transform.SetParent(transform, false);
    }    
}

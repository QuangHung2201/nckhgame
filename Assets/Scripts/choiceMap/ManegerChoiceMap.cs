using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManegerChoiceMap : MonoBehaviour
{
    public static ManegerChoiceMap Instance;
    [SerializeField] private Button button_exit;
    [SerializeField] private Button button_choicemap; // dùng đc chọn map tỉnh
    [SerializeField] Button btnDowPanel_logUncl;
    public GameObject panel_logUnclick;
    public GameObject objbtnDowPanel_log;

    public string tmp_idMap ;
    private RectTransform rect;
    private RectTransform rect_panelLog;
    void Start()
    {
        Instance = this;
        tmp_idMap = null;
        rect = GetComponent<RectTransform>();
        rect_panelLog =panel_logUnclick.GetComponent<RectTransform>();
   
    }
    private void OnEnable()
    {
        button_exit.onClick.AddListener(exit);
        button_choicemap.onClick.AddListener(setdataScreenMap);
        btnDowPanel_logUncl.onClick.AddListener(eventDowPanelLog);
    }
    private void OnDisable()
    {
        button_choicemap.onClick.RemoveAllListeners();
        button_exit.onClick.RemoveAllListeners();
        btnDowPanel_logUncl.onClick.RemoveAllListeners();   
        Instance = null;
    }
    private void exit()
    {
        MainEvent.instance.ClosePanel();
        gameObject.GetComponent<OpenCloseAni>().aniClose();
    }
 
    public void setTmpIDMap(string tmp_id) // gán id tạm khi ấn vào screen
    {
        tmp_idMap = tmp_id;
        Debug.Log("id tạm :"+ tmp_idMap);
    } 
    
    public void setdataScreenMap()
    {
        MainEvent.instance.ClosePanel();
        if(tmp_idMap != null)
        {
        PrefManager.PrefSaveUserMap.SetUserMapID(tmp_idMap);
        DataScreenMap.Instance.setData(tmp_idMap);
        }
        gameObject.GetComponent<OpenCloseAni>().aniClose();
    }    
    public void eventDowPanelLog()
    {
        rect_panelLog.DOScale(0f, 0.35f)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            rect_panelLog.localScale = new Vector3(1f,1f,1f);
            panel_logUnclick.gameObject.SetActive(false);
        }        
    );
    }    
}

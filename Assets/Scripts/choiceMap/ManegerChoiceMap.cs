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
    public string tmp_idMap ;
    private RectTransform rect;

    void Start()
    {
        Instance = this;
        tmp_idMap = null;
        rect = GetComponent<RectTransform>();
    }
    private void Awake()
    {
        button_exit.onClick.AddListener(exit);
        button_choicemap.onClick.AddListener(setdataScreenMap);
    }
    private void OnDisable()
    {
        button_choicemap.onClick.RemoveAllListeners();
        button_exit.onClick.RemoveAllListeners();
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
        Destroy(gameObject);
    }    
}

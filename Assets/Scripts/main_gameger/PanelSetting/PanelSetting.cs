using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelSetting : MonoBehaviour
{
    [SerializeField] private Button close;
    [SerializeField] private Button btn_resetdata;
    [SerializeField] private GameObject m_bg;
    [SerializeField] private Button btnIntroduc;
    [SerializeField] private GameObject obj_introduc;

    [SerializeField] private Slider slider_mussic;
    [SerializeField] private Slider slider_UI;
    void Start()
    {
        slider_mussic.value = PrefManager.PrefProfiles.getValueMussic();
        slider_UI.value = PrefManager.PrefProfiles.getValueUI();
    }
    private void OnEnable()
    {
        slider_mussic.onValueChanged.AddListener(updateVolumMussic);
        slider_UI.onValueChanged.AddListener(updatevolumUI);

        btn_resetdata.onClick.AddListener(resetdata);
        close.onClick.AddListener(closepanel);
        btnIntroduc.onClick.AddListener(eventOpenIntroduc);
    }
    public void updateVolumMussic(float volum)
    {
        SoundManager.instance.setVolumMussic(volum);
    }    
    public void updatevolumUI(float volum)
    {
        SoundManager.instance.setVolumUI(volum);
    }    
    public void closepanel()
    {
        gameObject.SetActive(false);
    }    
    public void eventOpenIntroduc()
    {
       gameObject.SetActive(false );
       obj_introduc.SetActive(true);
    }
    public void resetdata()
    {
        PlayerPrefs.DeleteAll();
        //PrefManager.PrefSaveUserMap.ResetData();

        //PrefManager.PrefSaveUserMap = new PrefSaveUserMap();

        PlayerPrefs.Save();
    }    
}

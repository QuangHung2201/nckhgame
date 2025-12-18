using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DataItemScreen : MonoBehaviour
{
    public string myID;
    public TextMeshProUGUI text_NameScreen;
    public Button button_choiceCsreen;
    public GameObject panelUnClock;
    public Image img_sprite;
    public SpriteAtlas spriteAtlas;
    void Start()
    {
       Debug.Log( PrefManager.PrefSaveUserMap.GetstaticLocation(myID) );
    }
    private void OnEnable()
    {
        checkstatic();
        button_choiceCsreen.onClick.AddListener(setIdTmp);
    }
    private void OnDestroy()
    {
        button_choiceCsreen.onClick.RemoveAllListeners();
    }
    public void Setdata(string name)
    {
        text_NameScreen.text = "" + name;
    }    
    public void set_myid(string id)
    {
        myID = id;

    }    
    public int checkstatic()
    {
        if( PrefManager.PrefSaveUserMap.GetstaticLocation(myID)  == 1)
        {
            return 1;
        }    
        else
        {
            return 0;
        }    

    }    
    public void openPanelUnlock()
    {
        panelUnClock.SetActive(false); 
    }    
    public void clockOpenUnlock()
    {
        panelUnClock.SetActive(true);
    }    
    public void setIdTmp() // khi nhấp vào map ta sẽ chuyền id sang manager để có thể chọn
    {
        ManegerChoiceMap.Instance.setTmpIDMap(myID);
    }    
    public void SetSpriteGr() // set sprite cho item theo id 
    {
        string namesprite = myID;
        img_sprite.sprite = spriteAtlas.GetSprite(namesprite);
    }    
}

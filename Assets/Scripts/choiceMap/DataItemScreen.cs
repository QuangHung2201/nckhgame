using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DataItemScreen : MonoBehaviour
{
    public static DataItemScreen Instance;
    public string myID;
    public string myName;
    public TextMeshProUGUI text_NameScreen;
    public Button button_choiceCsreen;
    public GameObject unclick;
    public Button btnLogUnclick;
    public Image img_sprite;
    public SpriteAtlas spriteAtlas;


    void Start()
    {
       Instance = this;
       Debug.Log( PrefManager.PrefSaveUserMap.GetstaticLocation(myID) );
    }
    private void OnEnable()
    {
        checkstatic();
        button_choiceCsreen.onClick.AddListener(setIdTmp);
        btnLogUnclick.onClick.AddListener(logUnclick);
    }
    private void OnDestroy()
    {
        Instance = null;    
        button_choiceCsreen.onClick.RemoveAllListeners();
    }
    public void Setdata(string name)
    {
        myName = name;
        text_NameScreen.text = "" + name;
    }    
    public void set_myid(string id)
    {
        myID = id;

    }    
    public void logUnclick()
    {
        ManegerChoiceMap.Instance.panel_logUnclick.SetActive(true);
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
        unclick.SetActive(false); 
    }    
    public void clockOpenUnlock()
    {
        unclick.SetActive(true);
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

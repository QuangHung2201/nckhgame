using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabGameplay : MonoBehaviour
{
    public static PrefabGameplay instance;
    public string idMap;
    public Button button_close;
    public Transform content;
    public screens screenslist;
    public List<GameObject> itemList;
    public Image progress_img;
    public TextMeshProUGUI progresLoc_text;
    void Start()
    {
        instance = this;
        itemList = new List<GameObject>();  //list item
        idMap = PrefManager.PrefSaveUserMap.GetUserMapchoose();  // lấy id tỉnh đã chọn
        screenslist = ConfigManager.instance.creenslist; // lấy data từ hệ thống
        spawnItem();
    }
    private void OnEnable()
    {
        button_close.onClick.AddListener(close);
    }
    private void OnDestroy()
    {
        button_close.onClick.RemoveAllListeners();
        instance = null;
    }
    public void close()
    {
        MainEvent.instance.ClosePanel();
        gameObject.GetComponent<OpenCloseAni>().aniClose();
    }    
    
    public void spawnItem()
    {
        int indexmap = 0;
        if(indexMap() != -1)
        {
         indexmap = indexMap(); 
        }    

        for(int i = 0; i < screenslist.playscreens[indexmap].location.Count; i++)
        {
           GameObject prefab_item = Resources.Load<GameObject>("GamePlay_Manager/itemplace");
           GameObject itemclone = Instantiate(prefab_item);
           itemclone.transform.SetParent(content, false);
           itemclone.GetComponent<Itemplace>().getindex(indexmap, screenslist.playscreens[indexmap].location[i].id,idMap ); // lấy id tỉnh và id địa danh
           itemclone.GetComponent<Itemplace>().setdata();
           itemList.Add(itemclone);
        }
        StartCoroutine(checkStaticItem());
    }  
    IEnumerator checkStaticItem()
    {
        yield return null;
        int complet_tmp = -1; // biến lưu toppic cuổi cùng hoàn thành

        for(int i = 0; i< (itemList.Count -1); i++)
        {
            bool check = itemList[i].GetComponent<Itemplace>().checkstatictoppic();
            if(check == true )
            {
                complet_tmp++;
                itemList[i + 1].GetComponent<Itemplace>().openlock();
            }  
            else
            {
                Debug.Log("not found item");
            }    
        }
        progressLocation(complet_tmp + 1);
    }    
  
    public void progressLocation(int idex) // hàm set tiến trình toppic hiện tại và ở main
    {
        progresLoc_text.text = "" + (idex + 1);
        if(indexMap() != -1)
        {
        string idtoppic = screenslist.playscreens[indexMap()].location[idex].id;
        int prog = PrefManager.PrefSaveUserMap.GetUserQuestionLocationID(idtoppic);
        int sizes = PrefManager.PrefSaveUserMap.GetSizeToppic(idtoppic);
        progress_img.fillAmount = (float)prog / sizes;
        }   
        else
        {
            Debug.Log("lỗi ở prefabGamePlay");
        }   
        
        
    }    

    public int indexMap()
    {
        for (int i = 0; i < screenslist.playscreens.Count; i++)
        {
            if (screenslist.playscreens[i].ID == idMap)
            {
                return i;
            }    
        }
        return -1;
    }    
}

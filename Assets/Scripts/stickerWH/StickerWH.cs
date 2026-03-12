using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StickerWH : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button btn_out;
    [SerializeField] private Transform par;
    private List<string> listSticker;
    void Start()
    {
        listSticker = PrefManager.PrefSaveUserMap.GetListStickerReceived();
        loadDataItem();
    }
    private void Awake()
    {
        btn_out.onClick.AddListener(EventOut);
    }
    private void OnDisable()
    {
        btn_out.onClick.RemoveAllListeners();
    }
    public void loadDataItem()
    {
        GameObject prefabItem = Resources.Load<GameObject>("GamePlay_Manager/prefabsSticker/itemSticker");
        if (prefabItem == null)
        {
            Debug.Log("không tìm thấy item");
        }
        if (listSticker != null)
        {
           for(int i = 0; i < listSticker.Count; i++) //load từ 1 bởi có sẵn 1 pt
            {
                Debug.Log("sticker đã nhận" + listSticker[i]);
                GameObject clone_item = Instantiate(prefabItem);
                clone_item.transform.SetParent(par,false);
                clone_item.GetComponent<StickerItem>().setdata(listSticker[i]);
            }   
        }
    }    
    private void EventOut()
    {
        Destroy(gameObject);
    }   

}

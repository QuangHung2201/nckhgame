using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StickerWH : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button btn_out;
    [SerializeField] private Transform par;
    [SerializeField] private Image fillprogress;
    [SerializeField] private TextMeshProUGUI txt_progress;
    [SerializeField] private GameObject chest;
    [SerializeField] private Button openchest;
    [SerializeField] private GameObject rewardPanel;
    private Animator ani_chest;

    private List<string> listSticker;

    void Start()
    {
        ani_chest = chest.GetComponent<Animator>();
        listSticker = PrefManager.PrefSaveUserMap.GetListStickerReceived();
        loadDataItem();
        statusProgress();
    }

    private void OnEnable()
    {
    }
    private void OnDestroy()
    {
        openchest.onClick.RemoveAllListeners();
    }
    public void eventOpenChest()
    {
        ani_chest.SetBool("open", true);
        StartCoroutine(delayshow());
    }   
    IEnumerator delayshow()
    {
        yield return new WaitForSeconds(0.5f);
        rewardPanel.SetActive(true);
    }    
    public void statusProgress()
    {
        int numberReceived = 0;
        int numberALL = 0;

        for(int i = 0; i< listSticker.Count; i++)
        {
            numberReceived++;
        }
        for(int i = 0; i< ConfigManager.instance.creenslist.playscreens.Count; i++)
        {
            for (int j = 0; j < ConfigManager.instance.creenslist.playscreens[i].location.Count; j++)
            {
                numberALL++;
            }    
        }
        Debug.Log("số lượng đã nhận" + numberReceived +"/"+ numberALL);
        fillprogress.fillAmount = (float)numberReceived / numberALL;
        txt_progress.text = "" + numberReceived +"/"+ numberALL;
        if(numberReceived == numberALL)
        {
            openchest.onClick.AddListener(eventOpenChest);
        }    
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

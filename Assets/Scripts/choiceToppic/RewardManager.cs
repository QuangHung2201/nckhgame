using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private screens data;
    private string idToppicStr;
    public Transform parentTrF;
    public Button buttonConT;

    private int indexmaps;
    private int indextoppic;
    private int numbCoinReW;
    private Dictionary<string, GameObject> dicRew; // lưu list phần thưởng 
    [SerializeField] private CanvasGroup bg;
    void Start()
    {

    }
    
    private void Awake()
    {
        dicRew = new Dictionary<string, GameObject>() ; 
    }
    private void OnEnable()
    {
        dicRew=new Dictionary<string, GameObject>() ;
        data = PrefabGameplay.instance.screenslist;
        indexmaps = PrefabGameplay.instance.indexMap(); 
        idToppicStr = PrefManager.PrefSaveUserMap.GetUserLocationchoose();
        indextoppic = inDexToppic();
        setdataReward();
        Debug.Log("id quà được chọn:" + inDexToppic());
        buttonConT.onClick.AddListener(EvenButtonConT);      
    }
    private void OnDisable()
    {
        dicRew.Clear();
        bg.alpha = 1.0f;
        buttonConT.onClick.RemoveAllListeners();
    }
    public void setdataReward()
    {
        GameObject P_itemReW = Resources.Load<GameObject>("GamePlay_Manager/item_reward");
        for(int i = 0; i < data.playscreens[indexmaps].location[indextoppic].reward.Count; i++)
        {
            GameObject itemReWClone = Instantiate(P_itemReW);
            itemReWClone.transform.SetParent(parentTrF, false);
            string idsprite = data.playscreens[indexmaps].location[indextoppic].reward[i].idSprite;
            string name = data.playscreens[indexmaps].location[indextoppic].reward[i].name;
            int number = data.playscreens[indexmaps].location[indextoppic].reward[i].number;
            itemReWClone.GetComponent<ItemReward>().setdata(idsprite, name, number);
            if(name == "coin")
            {
                numbCoinReW = number;
            }
            dicRew.Add(idsprite, itemReWClone);
        }    
    }

    public int inDexToppic() // hàm lấy idex topic
    {
        for (int i = 0; i < data.playscreens[indexmaps].location.Count; i++)
        {
            if (idToppicStr == data.playscreens[indexmaps].location[i].id)
            {
                return i;
            }
        }
        return -1;
    }
    public void EvenButtonConT()
    {
        var key = dicRew.Keys.ToList(); // id rw
        var val = dicRew.Values.ToList(); // obj rw
        for(int i = 0; i < key.Count; i++)
        {
            val[i].GetComponent<ItemReward>().openLight();
            if (key[i] == "coin")
            {
                HelperCoinAni.flyToPopupCoin(val[i]);
                StartCoroutine(delay2());
            }  
            else
            {
                PrefManager.PrefSaveUserMap.SetListAddStickerReceived(key[i]); 
                HelperCoinAni.flyToPopupStickerWH(val[i]);
            }    
        }
    }    

    IEnumerator delay2()
    {
        yield return new WaitForSeconds(1f);
        int numberBef = PrefManager.PrefMoney.getNumberCoin();
        int numberAfTer = numberBef + numbCoinReW;
        PrefManager.PrefMoney.SetNumberCoin(numberAfTer);
        HelperCoinAni.AnimateCoinIncrease(numberBef, numberAfTer, 0.5f);
        bg.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        PopupCoin.instance.poppubPar.transform.SetAsFirstSibling();
    }    
}

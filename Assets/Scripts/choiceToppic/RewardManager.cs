using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private screens data;
    private string idToppicStr;
    public Transform parentTrF;
    public Button buttonConT;

    private int indexmap;
    private int indextoppic;
    private int numbCoinReW;
    private Dictionary<string, GameObject> dicRew;

    void Start()
    {

        data = PrefabGameplay.instance.screenslist;
        indexmap = PrefabGameplay.instance.indexMap();
        idToppicStr = PrefManager.PrefSaveUserMap.GetUserLocationchoose();
        indextoppic = inDexToppic();
        setdataReward();
    }

    private void Awake()
    {
        dicRew = new Dictionary<string, GameObject>() ; 
    }
    private void OnEnable()
    {
        buttonConT.onClick.AddListener(EvenButtonConT);      
    }
    private void OnDisable()
    {
        buttonConT.onClick.RemoveAllListeners();
    }
    public void setdataReward()
    {
        GameObject P_itemReW = Resources.Load<GameObject>("GamePlay_Manager/item_reward");
        for(int i = 0; i < data.playscreens[indexmap].location[indextoppic].reward.Count; i++)
        {
            GameObject itemReWClone = Instantiate(P_itemReW);
            itemReWClone.transform.SetParent(parentTrF, false);
            string idsprite = data.playscreens[indexmap].location[indextoppic].reward[i].idSprite;
            string name = data.playscreens[indexmap].location[indextoppic].reward[i].name;
            int number = data.playscreens[indexmap].location[indextoppic].reward[i].number;
            itemReWClone.GetComponent<ItemReward>().setdata(idsprite, name, number);
            if(name == "coin")
            {
                numbCoinReW = number;
            }
            dicRew.Add(name, itemReWClone);
        }    
    }

    public int inDexToppic() // hàm lấy idex topic
    {
        for (int i = 0; i < data.playscreens[indexmap].location.Count; i++)
        {
            if (idToppicStr == data.playscreens[indexmap].location[i].id)
            {
                return i;
            }
        }
        return -1;
    }
    public void EvenButtonConT()
    {
        //PrefabGameplay.instance.panel_reward.SetActive(false);
        var key = dicRew.Keys.ToList();
        var val = dicRew.Values.ToList();
        for(int i = 0; i < key.Count; i++)
        {
            if (key[i] == "coin")
            {
                HelperCoinAni.flyToPopupCoin(val[i]);
                StartCoroutine(delay2());
            }    
        }
    }    
    IEnumerator delay2()
    {
        yield return new WaitForSeconds(1f);
        int numberBef = PrefManager.PrefMoney.getNumberCoin();
        int numberAfTer = numberBef + numbCoinReW;
        HelperCoinAni.AnimateCoinIncrease(numberBef, numberAfTer, 1f);
    }    
}

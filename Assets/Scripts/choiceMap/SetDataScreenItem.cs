using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDataScreenItem : MonoBehaviour
{
    // Start is called before the first frame update
    public static SetDataScreenItem Instance;
    public Transform content; 
    public screens screenlist ;
    public List<GameObject> listItem;

    private void Awake()
    {
        Instance = this;
        screenlist = ConfigManager.instance.creenslist;
        SetDatamanager.instance.RegisterdataScreen(this);
        listItem = new List<GameObject>();
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void SetData()
    {
        StartCoroutine(delayspawn());
    } 
    IEnumerator delayspawn()
    {
        yield return null;
        GameObject ItemPrefab = Resources.Load<GameObject>("GamePlay_Manager/ScreenGamePlay");
        int ind_tmp = -1; // lưu chỉ số cuối cùng hoàn thành

        for (int i = 0; i < screenlist.playscreens.Count; i++)
        {
            GameObject Item_clone = Instantiate(ItemPrefab);
            Item_clone.transform.SetParent(content, false);
            Item_clone.GetComponent<DataItemScreen>().Setdata(screenlist.playscreens[i].name);
            Item_clone.GetComponent<DataItemScreen>().set_myid(screenlist.playscreens[i].ID);
            Item_clone.GetComponent<DataItemScreen>().SetSpriteGr();
            listItem.Add(Item_clone);
            if (i == 0)
            {
                Item_clone.GetComponent<DataItemScreen>().openPanelUnlock();
            }
            else
            {
                Item_clone.GetComponent<DataItemScreen>().clockOpenUnlock();
            }

            if(Item_clone.GetComponent<DataItemScreen>().checkstatic() == 1 && i < (screenlist.playscreens.Count - 1))
            {
                Item_clone.GetComponent<DataItemScreen>().openPanelUnlock();
                ind_tmp = i;
            }    
        }

        yield return null;
        if(ind_tmp != -1) 
        {
            listItem[ind_tmp + 1].GetComponent<DataItemScreen>().openPanelUnlock();
        }
    }    
}

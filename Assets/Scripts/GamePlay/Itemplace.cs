using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Itemplace : MonoBehaviour
{
    public TextMeshProUGUI textname;
    public int indexmap; // apoin địa danh 
    public string idMap; // id địa danh
    public string idlocation; // lấy id toppic 
    public Button buttonPlay;
    public GameObject locks;
    int indexlocations ;
    public GameObject stickerComplet;
    void Start()
    {
        Debug.Log("apoin" + indexlocations);
        openlockFirst();

        if(checkstatictoppic() == true)
        {
            stickerComplet.SetActive(true);
        }    
    }   

    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
    }
    public void getindex(int indexmaps, string idlocations, string idmaps)  // hàm lấy địa chỉ
    {
        indexmap = indexmaps;
        idlocation = idlocations;
        idMap = idmaps;
    }    
    public void setdata()
    {
        if(this.indexlocation() != -1) indexlocations = this.indexlocation(); 

        string name = PrefabGameplay.instance.screenslist.playscreens[indexmap].location[indexlocations].name;
        textname.text = name;
    } 
    public int indexlocation() // hàm lấy idex topic
    {
        for(int i = 0;i < PrefabGameplay.instance.screenslist.playscreens[indexmap].location.Count;i++)
        {
            if(idlocation == PrefabGameplay.instance.screenslist.playscreens[indexmap].location[i].id)
            {
                return i;
            }    
        }
        return -1;
    }    
    public void SceneNext()
    { 
        PrefManager.PrefSaveUserMap.SetUserLocationID(idlocation);
        Debug.Log(idlocation);
        SceneManager.LoadScene(2);
    }
    public void openlockFirst() // mở khoá đầu tiên
    {
        if (indexlocations == 0) 
        {
            locks.SetActive(false);
            buttonPlay.onClick.AddListener(SceneNext);
        }
       else
        {
            locks.SetActive(true);
            buttonPlay.onClick.RemoveAllListeners();
        }    
    }    

    public bool checkstatictoppic() // check để mở khoá màn tiếp theo
    {

        if( PrefManager.PrefSaveUserMap.GetstaticToppic(idlocation, idMap) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }    

    public void openlock() // nếu item trước đã hoàn thành thì mở khoá 
    {
        locks.SetActive(false);
        buttonPlay.onClick.AddListener(SceneNext);
    }

}

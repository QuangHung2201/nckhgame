using DG.Tweening;
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
    public GameObject ani_chest;
    public Button button_openchest;

    Animator animator;
    Sequence seqUpDown;

    void Start()
    {
        animator = ani_chest.GetComponent<Animator>();
        animator.SetBool("open", false);

        Debug.Log("apoin" + indexlocations);
        openlockFirst();

        if(checkstatictoppic() == true)
        {
            stickerComplet.SetActive(true);
            button_openchest.onClick.AddListener(open_chests);
            aniUpDown();
        }    
        else
        {
            // button gọi show quà
            Debug.Log("chưa hoàn thành"); 
        }

        if(checkstatic_openChest() == 1)
        {
            open_chests();
        }
    
    }
   
    private void OnDestroy()
    {
        buttonPlay.onClick.RemoveAllListeners();
        button_openchest.onClick.RemoveAllListeners();
    }    
    public int checkstatic_openChest()
    {
        int stt = PrefManager.PrefSaveUserMap.GetStaticGiftToppic(idlocation);
        if(stt == 1)
        {
            return 1;
        }
        else
        {
            return 0;
        }    
    }
    public void aniUpDown()
    {
        if(checkstatic_openChest() == 0)
        {
        seqUpDown = DOTween.Sequence();
        seqUpDown.Append(button_openchest.transform.DOScale(1.2f, 0.25f))
        .Append(button_openchest.transform.DOScale(1f, 0.25f))
        .AppendInterval(3f)
        .SetLoops(-1);
        }
    }
    public void seqKill()
    {
        seqUpDown.Kill();
    }    
    public void open_chests()
    {
        animator.SetBool("open", true);
        seqKill();
        PrefManager.PrefSaveUserMap.SetStaticGiftToppic(idlocation, 1); // khi đã mở thì sửa trạng thái quà của toppic sang đã mở
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

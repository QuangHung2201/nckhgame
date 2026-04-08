using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DataScreenMap : MonoBehaviour
{
    public static DataScreenMap Instance;
    private screens screenlist ; // lấy data json từ manager
    public Image sprite_gr;
    public SpriteAtlas sprite_atlas;
    void Start()
    {
        Instance = this;
        screenlist = ConfigManager.instance.creenslist; // lấy data json từ manager
        setdatastart();
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void setdatastart()
    {
       string idmap = PrefManager.PrefSaveUserMap.GetUserMapchoose(); // id map người dùng chọn trước đó
       setData(idmap);
    }    
    public void setData(string idscreen) // set data cho screen ở main
    {
       int indexScreen = findpoinscreen(idscreen);
        if (indexScreen != -1)
        {    
           sprite_gr.sprite = sprite_atlas.GetSprite(idscreen);
        }
        else
        {
        }
    }  
    public int findpoinscreen(string idscreen) // tìm index theo chỉ số
    {
        for(int i = 0; i < screenlist.playscreens.Count; i++)
        {
            if (screenlist.playscreens[i].ID == idscreen)
            {
                return i;
            }    
        }
        return -1; // nếu không tìm thấy
    }    
}

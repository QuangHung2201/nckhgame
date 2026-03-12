using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SetDatamanager : MonoBehaviour // quản lý các hàm set của class set vào 1 biến để quản lý
{

    public static SetDatamanager instance;

    public static Action SetdataScreen;  
    void Start()
    {
        instance = this;
        autoDataStart();
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public void RegisterdataScreen(SetDataScreenItem item) // hàm đăng ký khi item được khởi tạo
    {
        SetdataScreen = item.SetData;
    }    

    public void autoDataStart()
    {
        string imap = PrefManager.PrefSaveUserMap.GetUserMapchoose();
        screens Maplist = ConfigManager.instance.creenslist;
        string idMapFirst = Maplist.playscreens[0].ID;
        if (imap == "")
        {
            PrefManager.PrefSaveUserMap.SetUserMapID(idMapFirst);
        }
    }    
}

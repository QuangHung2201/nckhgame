using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefSaveUserMap 
{
    private const string KEY_USERMAPID = "usermapID"; // key lưu map đã chọn của player
    private const string KEY_USERLOCATIONID = "cachelocationID"; // lưu id topic vừa nhấn ( để tìm json )
    //Question{UserLocation}    : key động lưu id câu hỏi hiện tại theo chủ đề

    //Toppic{toppic}{UserMapId} : key lưu trạng thái chủ đề theo địa danh
    //Location{userMapId}  : key lưu trạng thái theo địa danh
    //SizeToppic{Userlocation}  : key lưu kích cỡ số lượng câu hỏi theo địa danh
    //$"StaticGift{UserToppic}" : key lưu trạng thái nhận quà theo toppic
    public string GetUserMapchoose()
    {
        return PlayerPrefs.GetString(KEY_USERMAPID, "");
    }
    public string GetUserLocationchoose()
    {
        return PlayerPrefs.GetString(KEY_USERLOCATIONID, "");
    }
    public int GetUserQuestionLocationID(string UserLocation)  // lấy id câu hỏi hiện tại theo chủ đề ( key động )
    {
        return PlayerPrefs.GetInt($"Question{UserLocation}", 0);
    }
    public int GetstaticToppic(string toppic, string UserMapId)  // hàm lấy trạng thái chủ đề theo địa danh
    {
        return PlayerPrefs.GetInt($"Toppic{toppic}{UserMapId}", 0); 
    }
    public int GetstaticLocation(string userMapId)  //hàm lấy trạng thái  của địa danh
    {
        return PlayerPrefs.GetInt($"Location{userMapId}", 0);
    }
    public int GetSizeToppic(string UserToppic)
    {
        return PlayerPrefs.GetInt($"SizeToppic{UserToppic}", 0);
    }
    public int GetStaticGiftToppic(string UserToppic) // lấy trạng thái quà theo toppic
    {
        return PlayerPrefs.GetInt($"StaticGift{UserToppic}", 0); 
    }

    public void SetUserMapID(string userMap) // hàm lưu map đã chọn
    {
        PlayerPrefs.SetString(KEY_USERMAPID, userMap);
        PlayerPrefs.Save();
    }
    public void SetUserLocationID(string userLocationid)  // hàm sửa lại id location
    {
        PlayerPrefs.SetString(KEY_USERLOCATIONID, userLocationid);
        PlayerPrefs.Save();
    }
    public void SetUserQuestionLocationID(string UserLocation, int indexQuestion)  // hàm lưu id câu hỏi hiện tại theo chủ đề ( key động )
    {
        PlayerPrefs.SetInt($"Question{UserLocation}", indexQuestion);
        PlayerPrefs.Save();
    }
    public void SetStaticToppic(string toppic, string UserMapId, int statictoppic)  // hàm sửa trạng thái chủ đề theo địa danh
    {
        PlayerPrefs.SetInt($"Toppic{toppic}{UserMapId}", statictoppic);
        PlayerPrefs.Save();
    }
    public void SetStaticLocation(string userMapId,int staticNew)  // hàm sửa trạng thái địa danh
     {
        PlayerPrefs.SetInt($"Location{userMapId}", staticNew);
        PlayerPrefs.Save();
     }
    public void SetSizeToppic(string UserToppic, int SizeToppic)
    {
        PlayerPrefs.SetInt($"SizeToppic{UserToppic}", SizeToppic);
        PlayerPrefs.Save();
    }
    public void SetStaticGiftToppic(string UserToppic, int staticGift) // hàm sửa trạng thái quà theo toppic
    {
        PlayerPrefs.SetInt($"StaticGift{UserToppic}",staticGift);
        PlayerPrefs.Save();
    }
}

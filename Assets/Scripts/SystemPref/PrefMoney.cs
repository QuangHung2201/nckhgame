using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefMoney // không mono, không static => chỉ cần khai báo new là sử dụng ( phù hợp class cấp con )
{
    private const string KEY_SAVECOIN = "coin"; //biến lưu key sl coin

    public int getNumberCoin()  // hàm lấy coin
    {
       return PlayerPrefs.GetInt(KEY_SAVECOIN, 0);
    }    
    public void SetNumberCoin(int number) // hàm sửa số lượng coin
    {
        PlayerPrefs.SetInt(KEY_SAVECOIN, number);
        PlayerPrefs.Save();
    }
    
    public void AddNumberCoin(int number) // hàm cộng thêm coin
    {
        int currentCoin = getNumberCoin();
        currentCoin += number;
        SetNumberCoin(currentCoin);
    }
}

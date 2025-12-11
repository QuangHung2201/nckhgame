using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class PrefProfile  //không kế thừa mono và không static => phải khởi tạo bằng new ( dùng cho hệ thống cấp con )
{
    private const string KEY_USERID = "user_id"; // key lưu id
    private const string KEY_USERNAME = "username"; // key lưu tên
    private const string KEY_USERDATEOFBIRTH = "userdateofbirth"; // key lưu sinh nhật

    public string GetUserName() // hàm lấy tên
    {
        return PlayerPrefs.GetString(KEY_USERNAME,"");
    }
    public string GetUserdateofbirth() // hàm lấy sn
    {
        return PlayerPrefs.GetString(KEY_USERDATEOFBIRTH,"");
    }
    public string GetUserID()
    {
        return PlayerPrefs.GetString(KEY_USERID, "");
    }



    public void SetUserName(string usernamenew) // hàm sửa tên
    {
        PlayerPrefs.SetString(KEY_USERNAME, usernamenew);
        PlayerPrefs.Save();
    }
    public void SetUserdateofbirth(string userdateofbirth) // hàm sửa tên
    {
        PlayerPrefs.SetString(KEY_USERDATEOFBIRTH, userdateofbirth);
        PlayerPrefs.Save();
    }
    public void SetUserID(string userID) // hàm sửa tên
    {
        PlayerPrefs.SetString(KEY_USERID, userID);
        PlayerPrefs.Save();
    }
 
}

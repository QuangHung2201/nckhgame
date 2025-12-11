using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefManager  // quản lý các save pref
{
    public static PrefProfile PrefProfiles = new PrefProfile();    
    public static PrefSaveUserMap PrefSaveUserMap = new PrefSaveUserMap();
    public static PrefMoney PrefMoney = new PrefMoney();
}

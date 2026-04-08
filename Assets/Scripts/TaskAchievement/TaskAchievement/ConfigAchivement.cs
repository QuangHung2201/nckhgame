using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cấu hình file JSon cho nhiệm vụ hàng ngày
[System.Serializable]
public class itemDaily
{
    public int id;      // ID nhiệm vụ
    public string name; // tên nhiệm vụ
    public int target;  // mục tiêu cần đạt
    public int reward;  // phần thưởng
}

[System.Serializable]
public class TaskDailys
{
    public List<itemDaily> TaskDaily; // list nhiệm vụ daily
}

// cấu hình file JSon cho nhiệm vụ tháng
[System.Serializable]
public class itemMonthly
{
    public int id;
    public string name;
    public int target;
    public int reward;
}

[System.Serializable]
public class TaskMonthlys
{
    public List<itemMonthly> TaskMonthly; // list nhiệm vụ monthly
}

public class ConfigAchivement : MonoBehaviour
{
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cấu hình file JSon cho nhiệm vụ hàng ngày
[System.Serializable]
public class itemDaily
{
    public int id;
    public string name;
    public int reward;
}

[System.Serializable]
public class TaskDailys
{
    public List<itemDaily> TaskDaily;
}

// cấu hình file JSon cho nhiệm vụ tháng
[System.Serializable]
public class itemMonthly
{
    public int id;
    public string name;
    public int reward;
}

[System.Serializable]
public class TaskMonthlys
{
    public List<itemMonthly> TaskMonthly;
}

public class ConfigAchivement : MonoBehaviour
{
    
}

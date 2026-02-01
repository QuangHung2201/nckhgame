using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// cấu hình file JSon cho nhiệm vụ hàng ngày
public class TaskDaily
{
    public int id;
    public string name;
    public int reward;
}

[System.Serializable]
public class RewardDaily
{
    public List<TaskDaily> rewardDailys;
}

public class ConfigDaily : MonoBehaviour
{
    
}

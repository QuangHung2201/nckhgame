using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cấu hình file JSon cho nhiệm vụ tháng
public class TaskMonthly
{
    public int id;
    public string name;
    public int reward;
}

[System.Serializable]
public class RewardMonthly
{
    public List<TaskMonthly> rewardMonthlys;
}
public class ConfigMonthly : MonoBehaviour
{

}

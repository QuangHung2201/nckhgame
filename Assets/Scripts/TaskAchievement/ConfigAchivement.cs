using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class itemDaily
{
    public int id;      // ID nhiệm vụ
    public string name; // tên nhiệm vụ
    public int target;  // mục tiêu cần đạt
    public int progress; // tiến độ hiện tại
    public bool isClaimed; // trạng thái đã nhận thưởng hay chưa
    public int reward;  // phần thưởng
}

[System.Serializable]
public class TaskDailys
{
    public List<itemDaily> TaskDaily; 
}

[System.Serializable]
public class itemMonthly
{
    public int id;
    public string name;
    public int target;
    public int progress;
    public bool isClaimed;
    public int reward;
}

[System.Serializable]
public class TaskMonthlys
{
    public List<itemMonthly> TaskMonthly; 
}

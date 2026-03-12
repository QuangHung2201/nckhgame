using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

// các loại nhiệm vụ trong game
public enum TaskType
{
    Daily1, Daily2, Daily3, Daily4, Daily5,
    Monthly1, Monthly2, Monthly3, Monthly4, Monthly5
}

public class TaskData : MonoBehaviour
{
    public static TaskData Instance; // singleton để truy cập toàn game
    [SerializeField] private List<TextMeshProUGUI> btns; // hiển thị debug tiến độ nhiệm vụ

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    // chạy khi game bắt đầu
    void Start()
    {
        SetDataStart();

        // debug dữ liệu daily
        Debug.Log("DataDayily: " + GetInt("Daily1")
            + " " + GetInt("Daily2")
            + " " + GetInt("Daily3")
            + " " + GetInt("Daily4")
            + " " + GetInt("Daily5"));

        // debug dữ liệu monthly
        Debug.Log("DataMonthly: " + GetInt("Monthly1")
            + " " + GetInt("Monthly2")
            + " " + GetInt("Monthly3")
            + " " + GetInt("Monthly4")
            + " " + GetInt("Monthly5"));

        printData(); // hiển thị dữ liệu lên UI
    }

    // khởi tạo dữ liệu nhiệm vụ trong PlayerPrefs
    void SetDataStart()
    {
        SetInt("Daily1", 0);
        SetInt("Daily2", 0);
        SetInt("Daily3", 0);
        SetInt("Daily4", 0);
        SetInt("Daily5", 0);

        SetInt("Monthly1", 0);
        SetInt("Monthly2", 0);
        SetInt("Monthly3", 0);
        SetInt("Monthly4", 0);
        SetInt("Monthly5", 0);
    }

    // tăng tiến độ nhiệm vụ daily
    public void AddDaily1() => AddData(TaskType.Daily1);
    public void AddDaily2() => AddData(TaskType.Daily2);
    public void AddDaily3() => AddData(TaskType.Daily3);
    public void AddDaily4() => AddData(TaskType.Daily4);
    public void AddDaily5() => AddData(TaskType.Daily5);

    // tăng tiến độ nhiệm vụ monthly
    public void AddMonthly1() => AddData(TaskType.Monthly1);
    public void AddMonthly2() => AddData(TaskType.Monthly2);
    public void AddMonthly3() => AddData(TaskType.Monthly3);
    public void AddMonthly4() => AddData(TaskType.Monthly4);
    public void AddMonthly5() => AddData(TaskType.Monthly5);

    // tăng dữ liệu nhiệm vụ
    public void AddData(TaskType taskType)
    {
        string KeyName = taskType.ToString();

        int currentValue = PlayerPrefs.GetInt(KeyName);
        currentValue++;
        PlayerPrefs.SetInt(KeyName, currentValue);

        printData();

        UpdateTaskProgress(taskType);

        checkTaskItem(taskType, currentValue);
    }

    // cập nhật tiến độ nhiệm vụ sau khi tăng dữ liệu
    void UpdateTaskProgress(TaskType taskType)
    {
        // kiểm tra daily
        foreach (var taskItem in TaskManager.Instance.TaskDailys)
        {
            if (taskItem.taskType == taskType)
            {
                taskItem.UpdateProgress();
                return;
            }
        }

        // kiểm tra monthly
        foreach (var taskItem in TaskManager.Instance.TaskMonthlys)
        {
            if (taskItem.taskType == taskType)
            {
                taskItem.UpdateProgress();
                return;
            }
        }
    }

    // kiểm tra nhiệm vụ đã đạt mục tiêu chưa
    void checkTaskItem(TaskType taskType, int currentValue)
    {
        // kiểm tra nhiệm vụ daily
        foreach (var taskItem in TaskManager.Instance.TaskDailys)
        {
            if (taskItem.taskType == taskType)
            {
                if (currentValue >= taskItem.target)
                {
                    taskItem.EnableObject(); // cho phép nhận thưởng
                }
                return;
            }
        }

        // kiểm tra nhiệm vụ monthly
        foreach (var taskItem in TaskManager.Instance.TaskMonthlys)
        {
            if (taskItem.taskType == taskType)
            {
                if (currentValue >= taskItem.target)
                {
                    taskItem.EnableObject(); // cho phép nhận thưởng
                }
                return;
            }
        }
    }

    // reset toàn bộ dữ liệu nhiệm vụ
    public void ResetData()
    {
        SetDataStart();
        printData();
    }

    // in dữ liệu nhiệm vụ lên UI debug
    public void printData()
    {
        foreach (var btn in btns)
        {
            string KeyName = btn.name;
            int value = GetInt(KeyName);
            btn.text = $"{KeyName}: {value}";
        }
    }

    // lưu dữ liệu vào PlayerPrefs
    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    // lấy dữ liệu từ PlayerPrefs
    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}
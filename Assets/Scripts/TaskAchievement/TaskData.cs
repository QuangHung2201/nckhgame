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
        foreach (TaskType taskType in System.Enum.GetValues(typeof(TaskType)))
        {
            UpdateTaskProgress(taskType);
        }

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

    }

    // tăng dữ liệu nhiệm vụ
    public void AddData(TaskType taskType)
    {
        string KeyName = taskType.ToString();

        int currentValue = PlayerPrefs.GetInt(KeyName);
        currentValue++;
        PlayerPrefs.SetInt(KeyName, currentValue);

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
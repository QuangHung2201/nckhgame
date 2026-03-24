using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance; // singleton để truy cập từ các script khác

    public GameObject taskAchievement; // panel tổng nhiệm vụ
    public GameObject taskDaily;       // panel nhiệm vụ daily
    public GameObject taskMonthly;     // panel nhiệm vụ monthly
    
    public Button buttonDaily;   // nút chuyển sang daily
    public Button buttonMonthly; // nút chuyển sang monthly
    public Button buttonClose;   // nút đóng bảng nhiệm vụ

    public GameObject panel_coin;

    public GameObject ContentDaily;   // parent chứa item nhiệm vụ daily
    public GameObject ContentMonthly; // parent chứa item nhiệm vụ monthly

    public List<TaskItem> TaskDailys = new List<TaskItem>();     // danh sách item daily
    public List<TaskItem> TaskMonthlys = new List<TaskItem>();   // danh sách item monthly

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }

    public void RefreshAllTasks()
    {
        foreach (TaskType taskType in System.Enum.GetValues(typeof(TaskType)))
        {
            string key = taskType.ToString();
            int value = PlayerPrefs.GetInt(key);

            UpdateTaskProgress(taskType, value);
        }
    }

    // cập nhật tiến độ nhiệm vụ đã đạt mục tiêu chưa
    void UpdateTaskProgress(TaskType taskType, int currentValue)
    {
        // kiểm tra nhiệm vụ daily
        foreach (var taskItem in TaskDailys)
        {
            if (taskItem.taskType == taskType)
            {
                taskItem.UpdateProgress();
                if (currentValue >= taskItem.target)
                {
                    taskItem.EnableObject(); // cho phép nhận thưởng
                }
                return;
            }
        }
        
        // kiểm tra nhiệm vụ monthly
        foreach (var taskItem in TaskMonthlys)
        {
            if (taskItem.taskType == taskType)
            {
                taskItem.UpdateProgress();
                if (currentValue >= taskItem.target)
                {
                    taskItem.EnableObject(); // cho phép nhận thưởng
                }
                return;
            }
        }
    }

    // cập nhật danh sách nhiệm vụ daily
    public void UpdateTaskDaily()
    {
        TaskDailys.Clear();

        // duyệt qua tất cả item con trong ContentDaily để lấy component TaskItem
        foreach (Transform child in ContentDaily.transform)
        {
            TaskItem taskItem = child.GetComponent<TaskItem>();
            if (taskItem != null)
            {
                TaskDailys.Add(taskItem);
                taskItem.DisableObject();
            }
        }

        // gán loại nhiệm vụ cho từng item
        for (int i = 0; i < TaskDailys.Count; i++)
        {
            TaskDailys[i].taskType = (TaskType)i;
        }
    }

    // cập nhật danh sách nhiệm vụ monthly
    public void UpdateTaskMonthly()
    {
        TaskMonthlys.Clear(); // xóa danh sách cũ

        // duyệt qua tất cả item con trong ContentMonthly để lấy component TaskItem
        foreach (Transform child in ContentMonthly.transform)
        {
            TaskItem taskItem = child.GetComponent<TaskItem>();
            if (taskItem != null)
            {
                TaskMonthlys.Add(taskItem);
                taskItem.DisableObject();
            }
        }

        // gán loại nhiệm vụ monthly
        for (int i = 0; i < TaskMonthlys.Count; i++)
        {
            TaskMonthlys[i].taskType = (TaskType)(i + 5);
        }
    }

    // đăng ký sự kiện button khi panel được bật
    private void OnEnable()
    {
        buttonDaily.onClick.AddListener(ShowDailyTasks);
        buttonMonthly.onClick.AddListener(ShowMonthlyTasks);
        buttonClose.onClick.AddListener(CloseTaskAchievement);
    }

    // hủy sự kiện button khi panel tắt
    private void OnDisable()
    {
        buttonDaily.onClick.RemoveListener(ShowDailyTasks);
        buttonMonthly.onClick.RemoveListener(ShowMonthlyTasks);
        buttonClose.onClick.RemoveListener(CloseTaskAchievement);
    }

    // hiển thị nhiệm vụ daily
    public void ShowDailyTasks()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }

    // hiển thị nhiệm vụ monthly
    public void ShowMonthlyTasks()
    {
        taskDaily.SetActive(false);
        taskMonthly.SetActive(true);
    }

    // đóng bảng nhiệm vụ
    public void CloseTaskAchievement()
    {
        Destroy(gameObject);
    }
}
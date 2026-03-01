using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public GameObject taskAchievement;
    public GameObject taskDaily;
    public GameObject taskMonthly;

    public Button buttonDaily;
    public Button buttonMonthly;
    public Button buttonClose;

    public TextMeshProUGUI PopupScore;

    public GameObject ContentDaily;
    public GameObject ContentMonthly;
    public List<TaskItem> TaskDailys = new List<TaskItem>();
    public List<TaskItem> TaskMonthlys = new List<TaskItem>();

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }

    public void UpdateTaskDaily()
    {
        TaskDailys.Clear();

        foreach (Transform child in ContentDaily.transform)
        {
            TaskItem taskItem = child.GetComponent<TaskItem>();
            if (taskItem != null)
            {
                TaskDailys.Add(taskItem);
                taskItem.DisableObject();
            }
        }

        for (int i = 0; i < TaskDailys.Count; i++)
        {
            TaskDailys[i].taskType = (TaskType)i;
        }
    }

    public void UpdateTaskMonthly()
    {
        TaskMonthlys.Clear();
        foreach (Transform child in ContentMonthly.transform)
        {
            TaskItem taskItem = child.GetComponent<TaskItem>();
            if (taskItem != null)
            {
                TaskMonthlys.Add(taskItem);
                taskItem.DisableObject();
            }
        }

        for (int i = 0; i < TaskMonthlys.Count; i++)
        {
            TaskMonthlys[i].taskType = (TaskType)(i + 5);
        }
    }


    private void OnEnable()
    {
        buttonDaily.onClick.AddListener(ShowDailyTasks);
        buttonMonthly.onClick.AddListener(ShowMonthlyTasks);
        buttonClose.onClick.AddListener(CloseTaskAchievement);
    }

    private void OnDisable()
    {
        buttonDaily.onClick.RemoveListener(ShowDailyTasks);
        buttonMonthly.onClick.RemoveListener(ShowMonthlyTasks);
        buttonClose.onClick.RemoveListener(CloseTaskAchievement);
    }

    public void ShowDailyTasks()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }

    public void ShowMonthlyTasks()
    {
        taskDaily.SetActive(false);
        taskMonthly.SetActive(true);
    }

    public void CloseTaskAchievement()
    {
        taskAchievement.SetActive(false);
    }
}

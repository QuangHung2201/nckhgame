using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public enum TaskType
{
    Daily1, Daily2, Daily3, Daily4, Daily5,
    Monthly1, Monthly2, Monthly3, Monthly4, Monthly5
}

public class TaskData : MonoBehaviour
{
    public static TaskData Instance;
    [SerializeField] private List<TextMeshProUGUI> btns;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetData();
        Debug.Log("DataDayily: " + GetInt("Daily1")
            + " " + GetInt("Daily2")
            + " " + GetInt("Daily3")
            + " " + GetInt("Daily4")
            + " " + GetInt("Daily5"));
        Debug.Log("DataMonthly: " + GetInt("Monthly1")
            + " " + GetInt("Monthly2")
            + " " + GetInt("Monthly3")
            + " " + GetInt("Monthly4")
            + " " + GetInt("Monthly5"));

        printData();
    }

    void SetData()
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

    public void AddDaily1() => AddData(TaskType.Daily1);
    public void AddDaily2() => AddData(TaskType.Daily2);
    public void AddDaily3() => AddData(TaskType.Daily3);
    public void AddDaily4() => AddData(TaskType.Daily4);
    public void AddDaily5() => AddData(TaskType.Daily5);

    public void AddMonthly1() => AddData(TaskType.Monthly1);
    public void AddMonthly2() => AddData(TaskType.Monthly2);
    public void AddMonthly3() => AddData(TaskType.Monthly3);
    public void AddMonthly4() => AddData(TaskType.Monthly4);
    public void AddMonthly5() => AddData(TaskType.Monthly5);

    public void AddData(TaskType taskType)
    {
        string KeyName = taskType.ToString();

        int currentValue = PlayerPrefs.GetInt(KeyName);
        currentValue ++;
        PlayerPrefs.SetInt(KeyName, currentValue);
             
        printData();

        checkTaskItem(taskType, currentValue);
    }

    void checkTaskItem(TaskType taskType, int currentValue)
    {
        foreach (var taskItem in TaskManager.Instance.TaskDailys)
        {
            if (taskItem.taskType == taskType && currentValue >= 5)
            {
                taskItem.EnableObject();
            }
        }

        foreach (var taskItem in TaskManager.Instance.TaskMonthlys)
        {
            if (taskItem.taskType == taskType && currentValue >= 5)
            {
                taskItem.EnableObject();
            }
        }
    }

    public void ResetData()
    {
        SetData();
        printData();
    }

    public void printData()
    {
        foreach (var btn in btns)
        {
            string KeyName = btn.name;
            int value = GetInt(KeyName);
            btn.text = $"{KeyName}: {value}";
        }
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int GetInt(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}

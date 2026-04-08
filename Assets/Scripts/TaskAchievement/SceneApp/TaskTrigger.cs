using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// các loại nhiệm vụ trong game
public enum TaskType
{
    Daily1, Daily2, Daily3, Daily4, Daily5,
    Monthly1, Monthly2, Monthly3, Monthly4, Monthly5
}

public class TaskTrigger : MonoBehaviour
{
    //public static TaskTrigger Instance;

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this; 
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnEnable()
    {
        EventAchievement.Subscribe(EventType.CheckDaily1, checkDaily1);
        EventAchievement.Subscribe(EventType.CheckDaily3, checkDaily3);
        EventAchievement.Subscribe(EventType.CheckDaily4, checkDaily4);
        EventAchievement.Subscribe(EventType.CheckMonthly3, checkMonthly3);
        EventAchievement.Subscribe(EventType.CheckMonthly4, checkMonthly4);
    }

    private void OnDisable()
    {
        EventAchievement.Unsubscribe(EventType.CheckDaily1, checkDaily1);
        EventAchievement.Unsubscribe(EventType.CheckDaily3, checkDaily3);
        EventAchievement.Unsubscribe(EventType.CheckDaily4, checkDaily4);
        EventAchievement.Unsubscribe(EventType.CheckMonthly3, checkMonthly3);
        EventAchievement.Unsubscribe(EventType.CheckMonthly4, checkMonthly4);
    }

    private void Start()
    {
        ResetAchievement.Instance.CheckDailyReset(); // kiểm tra reset nhiệm vụ daily
        ResetAchievement.Instance.CheckMonthlyReset(); // kiểm tra reset nhiệm vụ monthly

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

    // kiểm tra đăng nhập hàng ngày
    public void checkDaily1()
    {
        int currentValue = PlayerPrefs.GetInt(TaskType.Daily1.ToString());
        int targetValue = 1;

        if (currentValue < targetValue)
        {
            AddDaily1(); // tăng dữ liệu nhiệm vụ daily 1
        }
        
    }

    // kiểm tra hoàn thành 3 câu hỏi
    public void checkDaily3()
    {
        int currentValue = PlayerPrefs.GetInt(TaskType.Daily3.ToString());
        int targetValue = 3; // mục tiêu trả lời đúng 3 câu hỏi

        if(currentValue < targetValue)
        {
            AddDaily3(); // tăng dữ liệu nhiệm vụ daily 3
        }
    }

    // kiểm tra trả lời đúng 3 câu hỏi liên tiếp
    private int correctStreak = 0;
    public void checkDaily4(bool isCorrect)
    {
        int currentValue = PlayerPrefs.GetInt(TaskType.Daily4.ToString());
        int targetValue = 3;

        if (currentValue >= targetValue) return;

        if (isCorrect)
        {
            correctStreak++; // tăng liên tiếp

            if (correctStreak >= targetValue)
            {
                AddDaily4(); // hoàn thành
            }
        }
        else
        {
            correctStreak = 0; // sai là reset
        }
    }
    
    // kiểm tra trả lời đúng 150 câu hỏi
    public void checkMonthly3()
    {
        int currentValue = PlayerPrefs.GetInt(TaskType.Monthly3.ToString());
        int targetValue = 150; // mục tiêu trả lời đúng 150 câu hỏi

        if (currentValue < targetValue)
        {
            AddMonthly3(); // tăng dữ liệu nhiệm vụ monthly 3
        }
    }

    // Kiểm tra đăng nhập hàng tháng
    public void checkMonthly4()
    {
        int currentValue = PlayerPrefs.GetInt(TaskType.Monthly4.ToString());
        int targetValue = 10;

        if (currentValue < targetValue)
        {
            AddMonthly4(); // tăng dữ liệu nhiệm vụ monthly 4
        }
    }

    public void AddDaily1() => AddData(TaskType.Daily1);
    public void AddDaily2() => AddData(TaskType.Daily2);
    // trả lời đúng nhiệm vụ daily
    public void AddDaily3() => AddData(TaskType.Daily3);
    public void AddDaily4() => AddData(TaskType.Daily4);
    public void AddDaily5() => AddData(TaskType.Daily5);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAchievement : MonoBehaviour
{
    public static ResetAchievement Instance; // singleton để truy cập từ các script khác

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

    public void CheckDailyReset()
    {
        string today = System.DateTime.Now.ToString("yyyyMMdd");
        string lastReset = PlayerPrefs.GetString("LastDailyReset", "");

        //Debug.Log("Today: " + today + " LastDailyReset: " + lastReset);

        // nếu ngày hôm nay khác với ngày đã lưu lần cuối reset, thì reset nhiệm vụ daily
        if (today != lastReset)
        {
            ResetDailyTasks();

            PlayerPrefs.SetString("LastDailyReset", today);

            // sự kiện làm mới giao diện nhiệm vụ daily
            EventAchievement.Trigger(EventType.CheckDaily1);
        }
    }

    void ResetDailyTasks()
    {
        // reset tất cả nhiệm vụ daily về 0
        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.SetInt("Daily" + i, 0);
        }
    }

    public void CheckMonthlyReset()
    {
        string thisMonth = System.DateTime.Now.ToString("yyyyMM");
        string lastMonth = PlayerPrefs.GetString("LastMonthlyReset", "");

        //Debug.Log("ThisMonth: " + thisMonth + " LastMonthlyReset: " + lastMonth);

        // nếu tháng này khác với tháng đã lưu lần cuối reset, thì reset nhiệm vụ monthly
        if (thisMonth != lastMonth)
        {
            ResetMonthlyTasks();

            PlayerPrefs.SetString("LastMonthlyReset", thisMonth);

            // sự kiện làm mới giao diện nhiệm vụ monthly
            EventAchievement.Trigger(EventType.CheckMonthly4);
        }
    }

    void ResetMonthlyTasks()
    {
        // reset tất cả nhiệm vụ monthly về 0
        for (int i = 1; i <= 5; i++)
        {
            PlayerPrefs.SetInt("Monthly" + i, 0);
        }
    }
}

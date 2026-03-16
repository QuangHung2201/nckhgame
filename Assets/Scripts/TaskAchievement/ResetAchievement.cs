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

        Debug.Log("Today: " + today + " LastDailyReset: " + lastReset);

        if (today != lastReset)
        {
            ResetDailyTasks();

            PlayerPrefs.SetString("LastDailyReset", today);

            TaskTrigger.Instance.OnLogin(); // kích hoạt lại nhiệm vụ đăng nhập hàng ngày

            Debug.Log("Daily tasks reset");
        }
    }

    void ResetDailyTasks()
    {
        PlayerPrefs.SetInt("Daily1", 0);
        PlayerPrefs.SetInt("Daily2", 0);
        PlayerPrefs.SetInt("Daily3", 0);
        PlayerPrefs.SetInt("Daily4", 0);
        PlayerPrefs.SetInt("Daily5", 0);
    }
}

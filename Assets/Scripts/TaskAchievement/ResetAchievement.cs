using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAchievement : MonoBehaviour
{
    void Start()
    {
        CheckDailyReset();
    }

    void CheckDailyReset()
    {
        string today = System.DateTime.Now.ToString("yyyyMMdd");
        string lastReset = PlayerPrefs.GetString("LastDailyReset", "");

        Debug.Log("Today: " + today + " LastDailyReset: " + lastReset);

        if (today != lastReset)
        {
            ResetDailyTasks();

            PlayerPrefs.SetString("LastDailyReset", today);

            //TaskTrigger.Instance.OnLogin(); // kích hoạt lại nhiệm vụ đăng nhập hàng ngày

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

        //TaskData.Instance.printData();
    }
}

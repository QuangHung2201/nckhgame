using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ResetAchievement
{
    public static void CheckFirstTime()
    {
        CheckDailyReset();
        CheckMonthlyReset();
    }

    public static void CheckDailyReset()
    {
        string today = System.DateTime.Now.ToString("yyyyMMdd");
        string lastDay = PlayerPrefs.GetString("LastDailyReset", "");

        Debug.Log("Today: " + today + " LastDailyReset: " + lastDay);

        if (string.IsNullOrEmpty(lastDay) || today != lastDay)
        {
            //Debug.Log("Reset Daily Data");

            JsonHelper.ResetDataDaily();

            PlayerPrefs.SetString("LastDailyReset", today);
            PlayerPrefs.Save();
        }
    }

    public static void CheckMonthlyReset()
    {
        string thisMonth = System.DateTime.Now.ToString("yyyyMM");
        string lastMonth = PlayerPrefs.GetString("LastMonthlyReset", "");

        Debug.Log("ThisMonth: " + thisMonth + " LastMonthlyReset: " + lastMonth);

        if (string.IsNullOrEmpty(lastMonth) || thisMonth != lastMonth)
        {
            //Debug.Log("Reset Monthly Data");

            JsonHelper.ResetDataMonthly();

            PlayerPrefs.SetString("LastMonthlyReset", thisMonth);
            PlayerPrefs.Save();
        }
    }
}

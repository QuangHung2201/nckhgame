using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonHelper
{
    public static TaskDailys dailyList;
    public static TaskMonthlys monthlyList;

    public static void LoadDataDaily()
    {
        string path = Application.persistentDataPath + "/TaskDaily.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonHelper.dailyList = JsonUtility.FromJson<TaskDailys>(json);
        }
        else
        {
            TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchievement/FileJSon/TaskDaily");
            if (textJSon == null) return;
            JsonHelper.dailyList = JsonUtility.FromJson<TaskDailys>(textJSon.text);

            JsonHelper.SaveDataDaily();
        }
    }

    public static void LoadDataMonthly()
    {
        string path = Application.persistentDataPath + "/TaskMonthly.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonHelper.monthlyList = JsonUtility.FromJson<TaskMonthlys>(json);
        }
        else
        {
            TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchievement/FileJSon/TaskMonthly");
            if (textJSon == null) return;
            JsonHelper.monthlyList = JsonUtility.FromJson<TaskMonthlys>(textJSon.text);
            JsonHelper.SaveDataMonthly();
        }
    }

    public static void SaveDataDaily()
    {
        string path = Application.persistentDataPath + "/TaskDaily.json";
        
        string json = JsonUtility.ToJson(JsonHelper.dailyList, true);
        File.WriteAllText(path, json);
    }

    public static void SaveDataMonthly()
    {
        string path = Application.persistentDataPath + "/TaskMonthly.json";
        
        string json = JsonUtility.ToJson(JsonHelper.monthlyList, true);
        File.WriteAllText(path, json);
    }

    public static void ResetDataDaily()
    {
        string path = Application.persistentDataPath + "/TaskDaily.json";

        TextAsset defaultJson = Resources.Load<TextAsset>("PrefabsAchievement/FileJSon/TaskDaily");

        if (defaultJson == null)
        {
            Debug.LogError("Không tìm thấy file TaskDaily trong Resources!");
            return;
        }

        File.WriteAllText(path, defaultJson.text);
    }

    public static void ResetDataMonthly()
    {
        string path = Application.persistentDataPath + "/TaskMonthly.json";

        TextAsset defaultJson = Resources.Load<TextAsset>("PrefabsAchievement/FileJSon/TaskMonthly");

        if (defaultJson == null)
        {
            Debug.LogError("Không tìm thấy file TaskMonthly trong Resources!");
            return;
        }

        File.WriteAllText(path, defaultJson.text);
    }
}

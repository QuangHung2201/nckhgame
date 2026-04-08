using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using static UnityEditor.Progress;

public class TaskDaily : MonoBehaviour
{
    public TaskDailys dailyList;   // danh sách nhiệm vụ daily đọc từ JSON
    public Transform parent;      // object chứa các item nhiệm vụ trong UI

    // khi object được bật -> load dữ liệu JSON
    private void OnEnable()
    {
        loaddata();
    }

    // khởi tạo UI nhiệm vụ
    private void Start()
    {
        setdata();
        TaskManager.Instance.UpdateTaskDaily();
        TaskManager.Instance.RefreshAllTasks();
    }

    // load file JSON nhiệm vụ daily
    private void loaddata()
    {
        TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchievement/FileJSon/TaskDaily");
        dailyList = JsonUtility.FromJson<TaskDailys>(textJSon.text);
    }

    // tạo UI item nhiệm vụ
    private void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchievement/TaskItem");

        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }

        // tạo item nhiệm vụ từ dữ liệu JSON
        for (int i = 0; i < dailyList.TaskDaily.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);

            itemClone.transform.SetParent(parent, false);

            itemClone.GetComponent<TaskItem>().SetData(
                dailyList.TaskDaily[i].name,
                dailyList.TaskDaily[i].reward,
                dailyList.TaskDaily[i].target
            );

            itemClone.GetComponent<TaskItem>().index = i;
            itemClone.GetComponent<TaskItem>().taskDailyRoot = this;

            itemClone.GetComponent<TaskItem>().rewardCoin = dailyList.TaskDaily[i].reward;
        }
    }
}
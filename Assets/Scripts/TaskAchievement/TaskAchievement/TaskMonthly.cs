using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMonthly : MonoBehaviour
{
    public TaskMonthlys monthlyList; // danh sách nhiệm vụ tháng đọc từ JSON
    public Transform parent;        // object chứa các item nhiệm vụ trong UI

    // khi object được bật -> load dữ liệu JSON
    private void OnEnable()
    {
        loaddata();
    }

    // khởi tạo UI nhiệm vụ
    private void Start()
    {
        setdata();
        TaskManager.Instance.UpdateTaskMonthly();
        TaskManager.Instance.RefreshAllTasks();
    }

    // load file JSON nhiệm vụ tháng
    private void loaddata()
    {
        TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchievement/TaskMonthly");
        monthlyList = JsonUtility.FromJson<TaskMonthlys>(textJSon.text);
    }

    // tạo UI item nhiệm vụ tháng
    private void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchievement/TaskItem");

        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }

        // tạo item nhiệm vụ từ dữ liệu JSON
        for (int i = 0; i < monthlyList.TaskMonthly.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);

            itemClone.transform.SetParent(parent, false);

            itemClone.GetComponent<TaskItem>().SetData(
                monthlyList.TaskMonthly[i].name,
                monthlyList.TaskMonthly[i].reward,
                monthlyList.TaskMonthly[i].target
            );

            itemClone.GetComponent<TaskItem>().rewardCoin = monthlyList.TaskMonthly[i].reward;
        }
    }
}
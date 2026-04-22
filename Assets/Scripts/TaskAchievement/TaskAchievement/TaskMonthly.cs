using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class TaskMonthly : MonoBehaviour
{ 
    public Transform parent;
    protected List<TaskItem> itemList = new();

    private void OnEnable()
    {
        JsonHelper.LoadDataMonthly();
    }

    private void Start()
    {
        setdata();
    }

    private void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchievement/TaskItem");

        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }

        for (int i = 0; i < JsonHelper.monthlyList.TaskMonthly.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);

            itemClone.transform.SetParent(parent, false);

            TaskItem taskItem = itemClone.GetComponent<TaskItem>();
            itemList.Add(taskItem);

            this.UpdateData(itemClone, i);
        }

        this.SetListMonthly();
    }

    protected void UpdateData(GameObject itemClone, int index)
    {
        var item = itemClone.GetComponent<TaskItem>();
        var itemIndex = JsonHelper.monthlyList.TaskMonthly[index];

        item.SetData(
            itemIndex.id,
            itemIndex.name,
            itemIndex.reward,
            itemIndex.target,
            itemIndex.progress,
            itemIndex.isClaimed
        );
    }

    protected void SetListMonthly()
    {
        itemList.Sort((a, b) =>
        {
            return a.isClaimed.CompareTo(b.isClaimed);
        });

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].transform.SetSiblingIndex(i);
        }
    }
}
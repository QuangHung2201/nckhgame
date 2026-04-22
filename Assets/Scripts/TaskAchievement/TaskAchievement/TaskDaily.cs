using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class TaskDaily : MonoBehaviour
{
    public Transform parent;
    protected List<TaskItem> itemList = new();

    private void OnEnable()
    {
        JsonHelper.LoadDataDaily();
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

        for (int i = 0; i < JsonHelper.dailyList.TaskDaily.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);

            itemClone.transform.SetParent(parent, false);

            TaskItem taskItem = itemClone.GetComponent<TaskItem>();
            itemList.Add(taskItem);

            this.UpdateData(itemClone, i);
        }

        this.SetListDaily();
    }

    protected void UpdateData(GameObject itemClone, int index)
    {
        var item = itemClone.GetComponent<TaskItem>();
        var itemIndex = JsonHelper.dailyList.TaskDaily[index];

        item.SetData(
            itemIndex.id,
            itemIndex.name,
            itemIndex.reward,
            itemIndex.target,
            itemIndex.progress,
            itemIndex.isClaimed
        );
    }

    protected void SetListDaily()
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
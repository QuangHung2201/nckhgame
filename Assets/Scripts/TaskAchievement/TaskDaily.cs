using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDaily : MonoBehaviour
{
    public TaskDailys dailyList;
    public Transform parent;

    private void OnEnable()
    {
        loaddata();
    }

    private void Start()
    {
        setdata();
        TaskManager.Instance.UpdateTaskDaily();
    }

    private void loaddata()
    {
        TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchivement/TaskDaily");
        dailyList = JsonUtility.FromJson<TaskDailys>(textJSon.text);
    }

    private void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchivement/TaskItem");
        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }
        for (int i = 0; i < dailyList.TaskDaily.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);
            
            itemClone.transform.SetParent(parent, false);
            itemClone.GetComponent<TaskItem>().SetData(
                dailyList.TaskDaily[i].name,
                dailyList.TaskDaily[i].reward
            );
            itemClone.GetComponent<TaskItem>().rewardCoin = dailyList.TaskDaily[i].reward;
        }
    }
}

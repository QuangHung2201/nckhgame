using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMonthly : MonoBehaviour
{
    public TaskMonthlys monthlyList;
    public Transform parent;

    private void OnEnable()
    {
        loaddata();
    }

    private void Start()
    {
        setdata();
    }

    private void loaddata()
    {
        TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchivement/TaskMonthly");
        monthlyList = JsonUtility.FromJson<TaskMonthlys>(textJSon.text);;
    }

    private void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchivement/TaskItem");
        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }
        for (int i = 0; i < monthlyList.TaskMonthly.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);

            itemClone.transform.SetParent(parent, false);
            itemClone.GetComponent<TaskItem>().SetData(
                monthlyList.TaskMonthly[i].name,
                monthlyList.TaskMonthly[i].reward
            );
            itemClone.GetComponent<TaskItem>().rewardCoin = monthlyList.TaskMonthly[i].reward;
        }
    }
}

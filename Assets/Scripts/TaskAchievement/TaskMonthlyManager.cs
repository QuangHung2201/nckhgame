using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMonthlyManager : MonoBehaviour
{
    private RewardMonthly rewardMonthlyList;
    public Transform parentTransform;

    private void OnEnable()
    {
        loadData();
    }

    private void Start()
    {
        setData();
    }

    private void loadData()
    {
        TextAsset textJSon = Resources.Load<TextAsset>("PrefabsAchivement/TaskMonthly");
        rewardMonthlyList = JsonUtility.FromJson<RewardMonthly>(textJSon.text);
    }

    private void setData()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchivement/TaskItem");
        if (itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }

        for (int i = 0; i < rewardMonthlyList.rewardMonthlys.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);
            itemClone.transform.SetParent(parentTransform, false);
            itemClone.GetComponent<TaskItem>().SetData(
                rewardMonthlyList.rewardMonthlys[i].name
            );
        }
    }
}

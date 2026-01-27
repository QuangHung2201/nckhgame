using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class itemreward
{
    public int id;
    public string name;
}
[System.Serializable]
class rewards
{
    public List<itemreward> reward;
}
public class TaskManager : MonoBehaviour
{
    public GameObject taskAchievement;
    public GameObject scrollViewDaily;
    public GameObject scrollViewMonthly;

    public Button buttonDaily;
    public Button buttonMonthly;
    public Button buttonClose;
    rewards rewardlist;
    public Transform parrent;

    // Start is called before the first frame update
    void Start()
    {

        scrollViewDaily.SetActive(true);
        scrollViewMonthly.SetActive(false);
        setdata();
    }
    private void OnEnable()
    {
        loaddata();
        buttonDaily.onClick.AddListener(ShowDailyTasks);
        buttonMonthly.onClick.AddListener(ShowMonthlyTasks);
        buttonClose.onClick.AddListener(CloseTaskAchievement);
    }
    private void OnDestroy()
    {
        buttonDaily.onClick.RemoveAllListeners();
    }
    public void loaddata()
    {
        TextAsset textjson = Resources.Load<TextAsset>("PrefabsAchivement/testjson");
        rewardlist = JsonUtility.FromJson<rewards>(textjson.text);
        Debug.Log("số lượng item reward: " + rewardlist.reward.Count);
    }


    public void setdata()
    {
        GameObject itemPrefab = Resources.Load<GameObject>("PrefabsAchivement/TaskItem");
        if(itemPrefab == null)
        {
            Debug.Log("không load được prefab");
        }
        for (int i= 0; i< rewardlist.reward.Count; i++)
        {
            GameObject itemClone = Instantiate(itemPrefab);
            itemClone.transform.SetParent(parrent,false);
            itemClone.GetComponent<TaskItem>().SetData(rewardlist.reward[i].name);
        }

    }
    public void ShowDailyTasks()
    {
        scrollViewDaily.SetActive(true);
        scrollViewMonthly.SetActive(false);
    }

    public void ShowMonthlyTasks()
    {
        scrollViewDaily.SetActive(false);
        scrollViewMonthly.SetActive(true);
    }

    public void CloseTaskAchievement()
    {
        taskAchievement.SetActive(false);
    }
}

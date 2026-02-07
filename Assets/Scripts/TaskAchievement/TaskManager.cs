using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public GameObject taskAchievement;
    public GameObject taskDaily;
    public GameObject taskMonthly;

    public Button buttonDaily;
    public Button buttonMonthly;
    public Button buttonClose;

    void Start()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }
    private void OnEnable()
    {
        buttonDaily.onClick.AddListener(ShowDailyTasks);
        buttonMonthly.onClick.AddListener(ShowMonthlyTasks);
        buttonClose.onClick.AddListener(CloseTaskAchievement);
    }
    private void OnDestroy()
    {
        buttonDaily.onClick.RemoveAllListeners();
    }

    public void ShowDailyTasks()
    {
        taskDaily.SetActive(true);
        taskMonthly.SetActive(false);
    }

    public void ShowMonthlyTasks()
    {
        taskDaily.SetActive(false);
        taskMonthly.SetActive(true);
    }

    public void CloseTaskAchievement()
    {
        taskAchievement.SetActive(false);
    }
}

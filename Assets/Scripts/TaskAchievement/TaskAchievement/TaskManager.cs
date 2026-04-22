using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public GameObject taskAchievement;
    public GameObject taskDaily;
    public GameObject taskMonthly;
    
    public Button buttonDaily;
    public Button buttonMonthly;
    public Button buttonClose;

    public GameObject panel_reward;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    private void OnDisable()
    {
        buttonDaily.onClick.RemoveListener(ShowDailyTasks);
        buttonMonthly.onClick.RemoveListener(ShowMonthlyTasks);
        buttonClose.onClick.RemoveListener(CloseTaskAchievement);
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
        Destroy(gameObject);
    }
}
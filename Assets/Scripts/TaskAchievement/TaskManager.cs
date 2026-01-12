using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public GameObject taskAchievement;
    public GameObject scrollViewDaily;
    public GameObject scrollViewMonthly;

    public Button buttonDaily;
    public Button buttonMonthly;
    public Button buttonClose;

    // Start is called before the first frame update
    void Start()
    {
        scrollViewDaily.SetActive(true);
        scrollViewMonthly.SetActive(false);

        buttonDaily.onClick.AddListener(ShowDailyTasks);
        buttonMonthly.onClick.AddListener(ShowMonthlyTasks);
        buttonClose.onClick.AddListener(CloseTaskAchievement);
    }

    // Update is called once per frame
    void Update()
    {
        
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

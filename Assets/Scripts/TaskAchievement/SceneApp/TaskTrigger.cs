using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    Daily1 = 0, 
    Daily2 = 1, 
    Daily3 = 2,
    Daily4 = 3, 
    Daily5 = 4,
    Monthly1 = 5, 
    Monthly2 = 6, 
    Monthly3 = 7, 
    Monthly4 = 8, 
    Monthly5 = 9
}

public class TaskTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        EventAchievement.Subscribe(EventType.AddDaily1, AddDaily1);
        EventAchievement.Subscribe(EventType.AddDaily2, AddDaily2);
        EventAchievement.Subscribe(EventType.AddDaily3, AddDaily3);
        EventAchievement.Subscribe<bool>(EventType.AddDaily4, AddDaily4);
        EventAchievement.Subscribe(EventType.AddDaily5, AddDaily5);
        EventAchievement.Subscribe(EventType.AddMonthly1, AddMonthly1);
        EventAchievement.Subscribe(EventType.AddMonthly2, AddMonthly2);
        EventAchievement.Subscribe(EventType.AddMonthly3, AddMonthly3);
        EventAchievement.Subscribe(EventType.AddMonthly4, AddMonthly4);
        EventAchievement.Subscribe(EventType.AddMonthly5, AddMonthly5);
    }

    private void OnDisable()
    {
        EventAchievement.Unsubscribe(EventType.AddDaily1, AddDaily1);
        EventAchievement.Unsubscribe(EventType.AddDaily2, AddDaily2);
        EventAchievement.Unsubscribe(EventType.AddDaily3, AddDaily3);
        EventAchievement.Unsubscribe<bool>(EventType.AddDaily4, AddDaily4);
        EventAchievement.Unsubscribe(EventType.AddDaily5, AddDaily5);
        EventAchievement.Unsubscribe(EventType.AddMonthly1, AddMonthly1);
        EventAchievement.Unsubscribe(EventType.AddMonthly2, AddMonthly2);
        EventAchievement.Unsubscribe(EventType.AddMonthly3, AddMonthly3);
        EventAchievement.Unsubscribe(EventType.AddMonthly4, AddMonthly4);
        EventAchievement.Unsubscribe(EventType.AddMonthly5, AddMonthly5);
    }

    public void AddDaily1() => AddProgress(TaskType.Daily1, 1);

    public void AddDaily2() => AddProgress(TaskType.Daily2, 1);

    public void AddDaily3() => AddProgress(TaskType.Daily3, 1);

    private int correctStreak = 0;
    public void AddDaily4(bool isCorrect)
    {
        if (isCorrect)
        {
            correctStreak++;

            if (correctStreak >= 3)
            {
                AddProgress(TaskType.Daily4, 1);
                correctStreak = 0;
            }
        }
        else
        {
            correctStreak = 0;
        }
    }

    public void AddDaily5() => AddProgress(TaskType.Daily5, 1);

    public void AddMonthly1() => AddProgress(TaskType.Monthly1, 1);

    public void AddMonthly2() => AddProgress(TaskType.Monthly2, 1);

    public void AddMonthly3() => AddProgress(TaskType.Monthly3, 1);

    public void AddMonthly4() => AddProgress(TaskType.Monthly4, 1);

    public void AddMonthly5() => AddProgress(TaskType.Monthly5, 1);

    public void AddProgress(TaskType taskType, int value)
    {
        if (taskType >= TaskType.Daily1 && taskType <= TaskType.Daily5)
        {
            AddProgressDaily((int)taskType, value);
        }
        else if (taskType >= TaskType.Monthly1 && taskType <= TaskType.Monthly5)
        {
            AddProgressMonthly((int)taskType, value);
        }
    }
    
    public void AddProgressDaily(int id, int value)
    {
        JsonHelper.LoadDataDaily();
        foreach (var task in JsonHelper.dailyList.TaskDaily)
        {
            if (task.id == id)
            {
                if (task.progress < task.target)
                    task.progress += value;
                break;
            }
        }

        JsonHelper.SaveDataDaily();
    }

    public void AddProgressMonthly(int id, int value)
    {
        JsonHelper.LoadDataMonthly();
        foreach (var task in JsonHelper.monthlyList.TaskMonthly)
        {
            if (task.id == id)
            {
                if (task.progress < task.target)
                    task.progress += value;
                break;
            }
        }

        JsonHelper.SaveDataMonthly();
    }
}
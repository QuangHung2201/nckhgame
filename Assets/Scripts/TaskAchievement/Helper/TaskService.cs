using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class TaskService
{
    public static void IsClaimed(int id, TaskType taskType)
    {
        if (taskType >= TaskType.Daily1 && taskType <= TaskType.Daily5)
        {
            IsClaimedDaily(id);
        }
        else if (taskType >= TaskType.Monthly1 && taskType <= TaskType.Monthly5)
        {
            IsClaimedMonthly(id);
        }
    }

    public static void IsClaimedDaily(int id)
    {
        JsonHelper.LoadDataDaily();
        foreach (var task in JsonHelper.dailyList.TaskDaily)
        {
            if (task.id == id)
            {
                task.isClaimed = true;
                break;
            }
        }

        JsonHelper.SaveDataDaily();
    }

    public static void IsClaimedMonthly(int id)
    {
        JsonHelper.LoadDataMonthly();
        foreach (var task in JsonHelper.monthlyList.TaskMonthly)
        {
            if (task.id == id)
            {
                task.isClaimed = true;
                break;
            }
        }

        JsonHelper.SaveDataMonthly();
    }
}

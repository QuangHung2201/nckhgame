using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public static TaskTrigger Instance { get; private set; }
    int correctStreak = 0;
    bool quizSkip = false;

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

    // đăng nhập game
    public void OnLogin()
    {
        TaskData.Instance.AddData(TaskType.Daily1);
    }

    // chơi 1 địa danh
    public void OnStartLocation()
    {
        TaskData.Instance.AddData(TaskType.Daily2);
    }

    // trả lời đúng
    public void OnCorrectAnswer()
    {
        TaskData.Instance.AddData(TaskType.Daily3);

        correctStreak++;

        if (correctStreak >= 3)
        {
            TaskData.Instance.AddData(TaskType.Daily4);
        }
    }

    // trả lời sai
    public void OnWrongAnswer()
    {
        correctStreak = 0;
    }

    // bỏ câu
    public void OnSkipQuestion()
    {
        quizSkip = true;
    }

    // kết thúc quiz
    public void OnFinishQuiz()
    {
        if (!quizSkip)
        {
            TaskData.Instance.AddData(TaskType.Daily5);
        }

        quizSkip = false;
        correctStreak = 0;
    }
}

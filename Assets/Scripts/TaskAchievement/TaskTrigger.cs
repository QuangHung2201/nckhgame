using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public static TaskTrigger Instance { get; private set; }
    int correctStreak = 0; // số câu trả lời đúng liên tiếp
    bool quizSkip = false; // kiểm tra người chơi bỏ qua câu hỏi trong quiz

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
        // Daily - đăng nhập
        TaskData.Instance.AddData(TaskType.Daily1);

        // Monthly - chơi game trong 10 ngày
        TaskData.Instance.AddData(TaskType.Monthly4);
    }

    // chơi 1 địa danh
    public void OnStartLocation()
    {
        // Daily
        TaskData.Instance.AddData(TaskType.Daily2);
    }

    // hoàn thành 1 địa danh
    public void OnCompleteLocation()
    {
        // Monthly - hoàn thành 1 địa danh
        TaskData.Instance.AddData(TaskType.Monthly1);
    }

    // trả lời đúng
    public void OnCorrectAnswer()
    {
        // Daily - trả lời đúng 1 câu
        TaskData.Instance.AddData(TaskType.Daily3);

        // Monthly - trả lời đúng 150 câu
        TaskData.Instance.AddData(TaskType.Monthly3);

        correctStreak++;
        // nếu trả lời đúng liên tiếp 3 câu
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
        // Daily - hoàn thành quiz mà không bỏ câu nào
        if (!quizSkip)
        {
            TaskData.Instance.AddData(TaskType.Daily5);
        }

        // Monthly - hoàn thành 1 quiz
        TaskData.Instance.AddData(TaskType.Monthly2);

        quizSkip = false;
        correctStreak = 0;
    }

    // hoàn thành 1 tỉnh
    public void OnCompleteToppic()
    {
        TaskData.Instance.AddData(TaskType.Monthly5);
    }

}

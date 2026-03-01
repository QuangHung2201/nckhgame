using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public TaskType taskType;
    public Image imgIconTask;
    public TextMeshProUGUI txtContentTask;
    public TextMeshProUGUI txtReward;
    public int rewardCoin = 0;

    [SerializeField] private Button btnReceive;
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        btnReceive.onClick.AddListener(receiveReward);
    }

    private void OnDisable()
    {
        btnReceive.onClick.RemoveListener(receiveReward);
    }

    void receiveReward()
    {
        int currentCoin = PrefManager.PrefMoney.getNumberCoin();
        PrefManager.PrefMoney.SetNumberCoin(currentCoin + rewardCoin);
        
        TaskManager.Instance.PopupScore.text = PrefManager.PrefMoney.getNumberCoin().ToString();

        PlayerPrefs.SetInt(taskType.ToString(), 0);
        TaskData.Instance.printData();

        DisableObject();
    }

    public void DisableObject()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        btnReceive.interactable = false;
    }

    public void EnableObject()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        btnReceive.interactable = true;
    }

    public void SetData(string text, int reward)
    {
        txtContentTask.text = text;
        txtReward.text = reward.ToString();
    }
}

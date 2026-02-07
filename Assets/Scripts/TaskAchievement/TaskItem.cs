using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public Image imgIconTask;
    public TextMeshProUGUI txtContentTask;
    public TextMeshProUGUI txtReward;
    [SerializeField] private Button btnReceive;
    public int rewardCoin = 0;
    [SerializeField] private CanvasGroup canvasGroup;

    void Start()
    {
        btnReceive.onClick.AddListener(receiveReward);
    }

    void receiveReward()
    {
        int currentCoin = PrefManager.PrefMoney.getNumberCoin();
        PrefManager.PrefMoney.SetNumberCoin(currentCoin + rewardCoin);

        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        btnReceive.interactable = false;
    }

    public void SetData(string text, int reward)
    {
        txtContentTask.text = text;
        txtReward.text = reward.ToString();
    }
}

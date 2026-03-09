using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public TaskType taskType;              // loại nhiệm vụ
    public Image imgIconTask;              // icon nhiệm vụ
    public TextMeshProUGUI txtContentTask; // nội dung nhiệm vụ
    public TextMeshProUGUI txtReward;      // hiển thị phần thưởng
    public int rewardCoin = 0;             // số coin nhận được

    [SerializeField] private Button btnReceive;      // nút nhận thưởng
    [SerializeField] private CanvasGroup canvasGroup; // điều khiển trạng thái UI

    private void OnEnable()
    {
        btnReceive.onClick.RemoveAllListeners();
        btnReceive.onClick.AddListener(receiveReward);
    }

    private void OnDisable()
    {
        btnReceive.onClick.RemoveListener(receiveReward);
    }

    // nhận thưởng nhiệm vụ
    void receiveReward()
    {
        int currentCoin = PrefManager.PrefMoney.getNumberCoin();
        PrefManager.PrefMoney.SetNumberCoin(currentCoin + rewardCoin);

        TaskManager.Instance.PopupScore.text = PrefManager.PrefMoney.getNumberCoin().ToString();

        // reset trạng thái nhiệm vụ
        PlayerPrefs.SetInt(taskType.ToString(), 0);
        TaskData.Instance.printData();

        DisableObject();
    }

    // khóa item nhiệm vụ
    public void DisableObject()
    {
        //canvasGroup.alpha = 0.5f;
        //canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    // mở khóa item nhiệm vụ
    public void EnableObject()
    {
        //canvasGroup.alpha = 1f;
        //canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    // gán dữ liệu cho UI nhiệm vụ
    public void SetData(string text, int reward)
    {
        txtContentTask.text = text;
        txtReward.text = reward.ToString();
    }
}
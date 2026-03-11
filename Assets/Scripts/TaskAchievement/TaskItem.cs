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
    public TextMeshProUGUI txtTarget;      // hiển thị tiến độ nhiệm vụ (ví dụ: 3/5)
    public int rewardCoin = 0;             // số coin nhận được
    public int target = 0;                 // mục tiêu hoàn thành nhiệm vụ

    [SerializeField] private Button btnReceive;      // nút nhận thưởng
    [SerializeField] private CanvasGroup canvasGroup; // điều khiển trạng thái UI
    [SerializeField] private Image progressFill;      // thanh tiến độ nhiệm vụ

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
        ResetProgress();

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
    public void SetData(string text, int reward, int target)
    {
        this.target = target;
        rewardCoin = reward;

        txtContentTask.text = text;
        txtReward.text = reward.ToString();

        //int currentValue = PlayerPrefs.GetInt(taskType.ToString());
        //txtTarget.text = currentValue + "/" + target;

        UpdateProgress();
    }

    // hiển thị tiến độ nhiệm vụ lên UI
    public void UpdateProgress()
    {
        int currentValue = PlayerPrefs.GetInt(taskType.ToString());
        txtTarget.text = currentValue + "/" + target;

        float progress = (float)currentValue / target;

        progressFill.fillAmount = progress;
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt(taskType.ToString(), 0);
        UpdateProgress();
    }
}
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public TaskType taskType;              // loại nhiệm vụ
    public TextMeshProUGUI txtContentTask; // nội dung nhiệm vụ
    public TextMeshProUGUI txtTarget;      // hiển thị tiến độ nhiệm vụ (ví dụ: 3/5)
    public int rewardCoin = 0;             // số coin nhận được
    public int target = 0;                 // mục tiêu hoàn thành nhiệm vụ

    public int index;                 // vị trí trong list
    public TaskDaily taskDailyRoot;   // reference về script cha

    [SerializeField] private Image progressFill;      // thanh tiến độ nhiệm vụ

    public CanvasGroup canvasGroup; // để bật/tắt tương tác với item nhiệm vụ
    public GameObject locks;
    public GameObject ani_chest;
    public Button button_openchest;

    Animator animator;
    Sequence seqUpDown;

    void Start()
    {
        animator = ani_chest.GetComponent<Animator>();
        animator.SetBool("open", false);
    }

    // hiệu ứng animation cho item nhiệm vụ (nhấp nháy khi hoàn thành)
    public void aniUpDown(Transform transformAni)
    {
        seqUpDown = DOTween.Sequence();
        seqUpDown.Append(transformAni.DOScale(1.2f, 0.25f))
        .Append(transformAni.DOScale(1f, 0.25f))
        .AppendInterval(3f)
        .SetLoops(-1);
    }

    public void seqKill()
    {
        seqUpDown.Kill();
    }

    private void OnEnable()
    {
        button_openchest.onClick.AddListener(eVenOpenReW);
    }

    private void OnDisable()
    {
        button_openchest.onClick.RemoveAllListeners();
    }

    public void eVenOpenReW()
    {
        open_chestsStart();

        StartCoroutine(delayRewardOpen());
    }

    IEnumerator delayRewardOpen()
    {
        yield return new WaitForSeconds(1f);

        GameObject panel = TaskManager.Instance.panel_reward;
        panel.SetActive(true);

        RewardAchievement rewardAchievementScript = panel.GetComponent<RewardAchievement>();
        rewardAchievementScript.SetCoin(rewardCoin);

        DisableObject();
        transform.SetSiblingIndex(transform.parent.childCount - 1);
    }

    public void open_chestsStart()
    {
        animator.SetBool("open", true);
        seqKill();
    }

    public void DisableObject()
    {
        canvasGroup.blocksRaycasts = false;   // chặn click
    }

    public void EnableObject()
    {
        canvasGroup.blocksRaycasts = true;    // cho click
    }

    // gán dữ liệu cho UI nhiệm vụ
    public void SetData(string text, int reward, int target)
    {
        this.target = target;
        rewardCoin = reward;

        txtContentTask.text = text;
    }

    // hiển thị tiến độ nhiệm vụ lên UI
    public void UpdateProgress()
    {
        int currentValue = PlayerPrefs.GetInt(taskType.ToString());
        
        txtTarget.text = currentValue + "/" + target;

        float progress = (float)currentValue / target;

        progressFill.fillAmount = progress;

        // kiểm tra hoàn thành nhiệm vụ
        if (currentValue >= target)
        {
            locks.SetActive(false);
            aniUpDown(ani_chest.transform);
        }
        else
        {
            locks.SetActive(true);
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt(taskType.ToString(), 0);
        UpdateProgress();
    }
}
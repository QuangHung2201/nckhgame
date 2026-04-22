using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public TaskType taskType;
    public TextMeshProUGUI txtTarget;
    
    public int id;
    public TextMeshProUGUI txtContentTask;
    public int rewardCoin = 0;
    public int target = 0;
    public int progress = 0;
    public bool isClaimed;

    [SerializeField] private Image progressFill;

    public CanvasGroup canvasGroup;
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
        if (this.progress < this.target || this.isClaimed)
        {
            return;
        }

        open_chestsStart();

        StartCoroutine(delayRewardOpen());

        TaskService.IsClaimed(this.id, this.taskType);
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
        canvasGroup.blocksRaycasts = false;
    }

    public void EnableObject()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void SetData(int id, string text, int reward, int target, int progress, bool isClaimed)
    {
        this.id = id;
        this.txtContentTask.text = text;
        this.rewardCoin = reward;
        this.target = target;
        this.progress = progress;
        this.isClaimed = isClaimed;

        this.UpdateUI();
    }

    protected void UpdateUI()
    {
        txtContentTask.text = this.txtContentTask.text;
        txtTarget.text = this.progress + "/" + this.target;
        progressFill.fillAmount = this.target > 0
            ? (float)this.progress / this.target
            : 0;

        this.CheckProgress();
    }

    protected void CheckProgress()
    {
        if (this.progress >= this.target && !this.isClaimed)
        {
            locks.SetActive(false);

            aniUpDown(ani_chest.transform);
        }
        else
        {
            locks.SetActive(true);
        }
    }
}
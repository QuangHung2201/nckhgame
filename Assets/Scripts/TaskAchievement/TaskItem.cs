using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    [SerializeField] private Button btnReceive;
    [SerializeField] private int rewardCoin;// vàng thưởng
    [SerializeField] private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        btnReceive.onClick.AddListener(receiveReward);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void receiveReward()
    {
        PrefManager.PrefMoney.AddNumberCoin(rewardCoin);
        Debug.Log("Nhận thưởng: " + rewardCoin);

        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        btnReceive.interactable = false;

    }
}

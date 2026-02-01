using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public Image imgIconTask; // icon nhiệm vụ
    public TextMeshProUGUI txtContentTask; // nội dung nhiệm vụ
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
        // Cộng vàng vào tài khoản
        int currentCoin = PrefManager.PrefMoney.getNumberCoin();
        PrefManager.PrefMoney.SetNumberCoin(currentCoin + rewardCoin);
        Debug.Log("Nhận thưởng: " + rewardCoin);

        // Vô hiệu hóa nút nhận thưởng
        canvasGroup.alpha = 0.5f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        btnReceive.interactable = false;

    }

    public void SetData(string text)
    {
        txtContentTask.text = text;
    }
}

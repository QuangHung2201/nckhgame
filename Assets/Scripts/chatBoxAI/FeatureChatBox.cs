
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeatureChatBox : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private GameObject bossPopupObj;
    [SerializeField] private Button btnSentReq;


    [SerializeField] private TextMeshProUGUI txtResponse;
    [SerializeField] private TMP_InputField txtReQuest;
    void Start()
    {
        btnClose.onClick.AddListener(CloseChatbos);
        btnSentReq.onClick.AddListener(CallReQuestAPI);
    }
    private void OnDestroy()
    {
        btnClose.onClick.RemoveAllListeners();
    }
    public void CloseChatbos()
    {
        gameObject.SetActive(false);
        bossPopupObj.SetActive(true);   
    }    
      
    public void CallReQuestAPI() // hàm gọi và đợi trả về txt
    {
        string txtReQString = txtReQuest.text;
        txtResponse.text = "Đợi chút nha Bro...";
        APImanager.instance.StartCoroutine(APImanager.instance.reQuesttxt(txtReQString, (responeAPI) =>
        {
            txtResponse.text = responeAPI.ToString();
        }
        ));
    }   
   }

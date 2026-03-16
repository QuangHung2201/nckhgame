using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainEvent : MonoBehaviour
{
    public static MainEvent instance;
    [SerializeField] private Button button_choosemap;
    [SerializeField] private Button button_Profile;
    [SerializeField] private Button button_chooseLocation;
    [SerializeField] private Button button_StickerWareHouse;
    [SerializeField] private Button button_reloadTMP;
    [SerializeField] private Button button_TaskAchivement;

    public GameObject PanelUnClick;
    //public GameObject TaskAchivement;

    public  Transform Rood;

    public TextMeshProUGUI PopupScore; // hiển thị coin sau khi nhận thưởng

    private void Start()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void Awake() // nối button với hàm sự kiện
    {
        button_choosemap.onClick.AddListener(StartGame);
        button_Profile.onClick.AddListener(OpenProfileUser);
        button_chooseLocation.onClick.AddListener(OpenChooseLocation);
        button_StickerWareHouse.onClick.AddListener(OpenStickerWareHouse);
        button_reloadTMP.onClick.AddListener(reloadTMP);
        button_TaskAchivement.onClick.AddListener(OpenTaskAchivement);
    }
    private void OnDisable() // chuyển qua scene khác sẽ ngắt lắng nghe
    {
        button_choosemap.onClick.RemoveListener(StartGame);
        button_Profile.onClick.RemoveAllListeners();
        button_chooseLocation.onClick.RemoveAllListeners();
        button_StickerWareHouse.onClick.RemoveAllListeners();
        button_reloadTMP.onClick.RemoveAllListeners();
        button_TaskAchivement.onClick.RemoveAllListeners();
        instance = null;
    }

    private void reloadTMP()
    {
        PlayerPrefs.DeleteAll();
    }
    private void StartGame()
    {
        OpenPanel();
        GameObject gamestart_prefab = Resources.Load<GameObject>("GamePlay_Manager/MapScreen");
        GameObject gameplay_clone = Instantiate(gamestart_prefab);
        gameplay_clone.transform.SetParent(Rood, false);
        SetDatamanager.SetdataScreen();
        RectTransform rect = gameplay_clone.GetComponent<RectTransform>();
        rect.anchorMin = Vector3.zero;  
        rect.anchorMax = Vector3.one;
    }    


    private void OpenProfileUser()
    {
        OpenPanel();
        GameObject UserProfile_prefab = Resources.Load<GameObject>("UserProfile/PrefabUserProfile");
        GameObject UserProfile_clone = Instantiate(UserProfile_prefab);
        UserProfile_clone.transform.SetParent(Rood, false);
        RectTransform rect = UserProfile_clone.GetComponent<RectTransform>();
        rect.anchorMin = Vector3.zero;
        rect.anchorMax = Vector3.one;
    }    

    private void OpenChooseLocation()
    {
        OpenPanel();
        GameObject prefab_chooseLocation = Resources.Load<GameObject>("GamePlay_Manager/prefab_gameplay");
        GameObject chooseLocationclone = Instantiate(prefab_chooseLocation);
        chooseLocationclone.transform.SetParent(Rood, false);
        RectTransform rect = chooseLocationclone.GetComponent<RectTransform>();
        rect.anchorMin = Vector3.zero;
        rect.anchorMax = Vector3.one;
    }    
    private void OpenStickerWareHouse()
    {
        GameObject prefab_stickerWH = Resources.Load<GameObject>("GamePlay_Manager/prefabsSticker/stickerWareHouse");
        GameObject clone_stickerWH = Instantiate(prefab_stickerWH);
        clone_stickerWH.transform.SetParent(Rood, false);
        RectTransform rect = clone_stickerWH.GetComponent<RectTransform>();
        rect.anchorMin = Vector3.zero;
        rect.anchorMax = Vector3.one;
    }

    private void OpenTaskAchivement()
    {
        GameObject prefab_TaskAchivement = Resources.Load<GameObject>("PrefabsAchievement/Task_Achievement");
        GameObject clone_TaskAchivement = Instantiate(prefab_TaskAchivement);
        clone_TaskAchivement.transform.SetParent(Rood, false);
        RectTransform rect = clone_TaskAchivement.GetComponent<RectTransform>();
        rect.anchorMin = Vector3.zero;
        rect.anchorMax = Vector3.one;
    }

    public void OpenPanel()
    {
        PanelUnClick.SetActive(true);
    }    

    public void ClosePanel()
    {
        PanelUnClick?.SetActive(false);
    }
}

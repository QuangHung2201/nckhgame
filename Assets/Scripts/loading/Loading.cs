using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_loading ;
    [SerializeField] private Image progressfill;
    AsyncOperation assyncMain; // quản lý load sang scene map ( và lấy được person )

    public GameObject panel_SignUp;
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60; //lúc build bật để mượt hơn
        StartCoroutine(loading());
        if(PrefManager.PrefProfiles.GetUserID() == "") // mới vào game nếu rỗng thì mình sẽ tạo id
        {
            createID();
        }    
    }
    IEnumerator loading()
    {
        yield return new WaitForSeconds(0.5f); // đợi load xong ui
        ConfigManager.instance.loadlocalscreenMap(); 
        assyncMain = SceneManager.LoadSceneAsync(1);
        assyncMain.allowSceneActivation = false;
      
        while (!assyncMain.isDone)
        {
            float percent = assyncMain.progress * 100;
            text_loading.text = "LOADLING..." +  percent + "%";

            progressfill.fillAmount = assyncMain.progress;
            if(assyncMain.progress >= 0.9f && PrefManager.PrefProfiles.GetUserName() == "") 
            {
              panel_SignUp.SetActive(true);
              panel_SignUp.GetComponent<PanelSignUp>().listen_loadMain(loadingMain);
            }
            if(assyncMain.progress >= 0.9f && PrefManager.PrefProfiles.GetUserName() != "")
            {
                loadingMain();
            }    
            yield return null; // đợi 1 fame xong load tiếp ( không sẽ lag )
        }
    }    
    public void loadingMain()
    {
        assyncMain.allowSceneActivation = true;
    }    
    public void createID()
    {
        string IDnew = System.Guid.NewGuid().ToString().Substring(0,8);
        PrefManager.PrefProfiles.SetUserID(IDnew);
        Debug.Log(PrefManager.PrefProfiles.GetUserID());
    }    
}

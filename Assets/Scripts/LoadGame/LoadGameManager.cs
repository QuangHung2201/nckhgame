using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameManager : MonoBehaviour
{
    public string idlocationcache;  // lấy id địa danh vừa nhấn
    public TextMeshProUGUI textpercent;
    public Image imageFill;
    // Start is called before the first frame update
    void Start()
    {
        idlocationcache = PrefManager.PrefSaveUserMap.GetUserLocationchoose();
        StartCoroutine(loaddatagame());
    }

    IEnumerator loaddatagame()
    {
        yield return new WaitForSeconds(0.5f);

        if(!ConfigManager.instance.Dic_location.ContainsKey(idlocationcache))  //ktra nếu id đó chưa có trong dic thì load json và lưu vào dic
        {
            ConfigManager.instance.loadDatajsonQuestion(idlocationcache); // load json theo id địa danh  ( test)
        }    
        AsyncOperation sceneAsyncGame = SceneManager.LoadSceneAsync(3);  // biến quản lý load scene : syncOperation
        sceneAsyncGame.allowSceneActivation = false;
        while(!sceneAsyncGame.isDone)
        {
            textpercent.text = "Loading..." + sceneAsyncGame.progress * 100 + "%";
            imageFill.fillAmount = sceneAsyncGame.progress ;
            if(sceneAsyncGame.progress >= 0.9f)
            {
                sceneAsyncGame.allowSceneActivation = true;
            }
            yield return null;
        }    
    }    
}

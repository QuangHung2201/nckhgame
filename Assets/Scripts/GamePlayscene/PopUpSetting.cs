using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSetting : MonoBehaviour
{
    public Button button_backmain;
    public Button button_resume;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        button_backmain.onClick.AddListener(ManagerSceneGame.Instance.backMain);
        button_resume.onClick.AddListener(ManagerSceneGame.Instance.playcoutdown);
    }
    private void OnDestroy()
    {
        button_backmain.onClick.RemoveAllListeners();
        button_resume.onClick.RemoveAllListeners();
    }
}

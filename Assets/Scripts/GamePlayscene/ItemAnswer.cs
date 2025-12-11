using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAnswer : MonoBehaviour
{
    public bool check;
    public Button button;
    public TextMeshProUGUI textanswer;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnEnable()
    {
       button.onClick.AddListener(checkcr);
    }
    private void OnDestroy()
    {
       button.onClick.RemoveAllListeners();   
    }
    public void SetData(string answer,bool checks)
    {
        textanswer.text = answer;
        check = checks;
    }     
    public void checkcr()
    {
        ManagerSceneGame.Instance.checkcorrect(check);
    }    
}

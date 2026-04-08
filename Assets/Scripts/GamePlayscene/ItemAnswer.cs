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
    public GameObject tick_true;
    public GameObject tick_false;
    public GameObject fade_true;
    public GameObject fade_false;
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
    public void settrue()
    {
        tick_true.SetActive(true);
        fade_true .SetActive(true);
    }    
    public void setfalse()
    {
        tick_false.SetActive(true);
        fade_false .SetActive(true);
    }
    public void resetStickandFade()
    {
        tick_false.SetActive(false);
        tick_true.SetActive(false);
        fade_false.SetActive(false);
        fade_true.SetActive(false);
    }    
    public void SetData(string answer,bool checks)
    {
        resetStickandFade();
        textanswer.text = answer;
        check = checks;
    }     
    public void checkcr()
    {
        ManagerSceneGame.Instance.checkcorrect(check);
        ManagerSceneGame.Instance.resetTickAllbtn();
        if(check == true)
        {
        settrue();
        }    
        else
        {
        setfalse(); 
        }    
    }


}

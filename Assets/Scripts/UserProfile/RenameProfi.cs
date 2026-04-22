using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RenameProfi : MonoBehaviour
{
    public Button closebutton;
    public Button btnFixname;
    public TMP_InputField TMP_InputField;
    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        closebutton.onClick.AddListener(close);
        btnFixname.onClick.AddListener(ReNameUser);
    }
    private void OnDestroy()
    {
        closebutton.onClick.RemoveListener(close);
        btnFixname.onClick.RemoveAllListeners();
    }
    public void close()
    {
        bg.GetComponent<OpenCloseAni>().aniClose();
        StartCoroutine(destroy());
    }    
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject);
    }    
    public void ReNameUser()
    {
        string idFigure = PichFigure.instance.idFigureChoose;
        string name = TMP_InputField.text;
        PrefManager.PrefProfiles.SetUserName(name);
        UserProfilePanel.instance.setID();
        PrefManager.PrefProfiles.SetUserIDfigure(idFigure);
        Btnprofile.instance.setdatabtn();
        Debug.Log("nhân vật được chọn " + idFigure);
        close();
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenameProfi : MonoBehaviour
{
    public Button closebutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        closebutton.onClick.AddListener(close);
    }
    private void OnDestroy()
    {
        closebutton.onClick.RemoveListener(close);
    }
    public void close()
    {
        Destroy(gameObject);
    }    
    public void ReNameUser(string newName)
    {
        PrefManager.PrefProfiles.SetUserName(newName);
        UserProfilePanel.instance.setID();
    }    
}

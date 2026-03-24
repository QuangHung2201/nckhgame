using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureChatBox : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    void Start()
    {
        btnClose.onClick.AddListener(CloseChatbos);
    }
    private void OnDestroy()
    {
        btnClose.onClick.RemoveAllListeners();
    }
    public void CloseChatbos()
    {
        gameObject.SetActive(false);
    }    
}

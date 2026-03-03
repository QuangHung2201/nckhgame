using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerWH : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button btn_out;
    void Start()
    {
        
    }
    private void Awake()
    {
        btn_out.onClick.AddListener(EventOut);
    }
    private void OnDisable()
    {
        btn_out.onClick.RemoveAllListeners();
    }
    private void EventOut()
    {
        Destroy(gameObject);
    }   

}

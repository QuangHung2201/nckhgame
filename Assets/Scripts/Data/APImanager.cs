using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APImanager : MonoBehaviour 
{
    public static APImanager instance;

    private void Start()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }
    
    public string reQuesttxt(string txtrequest)
    {
        string error = "lỗi api";
        APIchatBoxAI.instance.StartCoroutine(APIchatBoxAI.instance.PostReQuest(txtrequest, (txtRequestAPI) =>
        error = txtRequestAPI
        )); 
        return error;
    }    
}

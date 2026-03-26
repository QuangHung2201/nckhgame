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
    
    public IEnumerator reQuesttxt(string txtrequest,System.Action<string> Ondone)
    {
        string txtRespone = "lỗi api";
        yield return APIchatBoxAI.instance.StartCoroutine(APIchatBoxAI.instance.PostReQuest(txtrequest, (txtRequestAPI) =>
        txtRespone = txtRequestAPI
        ));
        Ondone.Invoke(txtRespone);
    }    
}

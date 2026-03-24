using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIchatBoxAI : MonoBehaviour
{
    public static APIchatBoxAI instance;
    public bool static_connect;
    void Start()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public IEnumerator PostReQuest(string textpost, System.Action<string> onDone) // trả về text sau khi chạy xong
    {
        string url = "https://leaves-constant-months-molecular.trycloudflare.com";
        UnityWebRequest request = new UnityWebRequest(url,"POST");// tạo request
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(textpost)); // tạo body upload
        request.downloadHandler = new DownloadHandlerBuffer(); // tạo body nhận tất cả data
        request.SetRequestHeader("Content-Type", "text/plain"); // báo api biết là gửi text

        yield return request.SendWebRequest(); // gửi request và đợi

        if(request.result == UnityWebRequest.Result.Success)
        {
            static_connect = true;
            onDone.Invoke(request.downloadHandler.text);
        }
        else
        {
            static_connect =false;
            Debug.Log("error connect API");
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIchatBoxAI : MonoBehaviour
{
    public static APIchatBoxAI instance;
    public bool static_connect;

    [System.Serializable]
    public class chatAPI
    {
        public string detail;
    }

    [System.Serializable]
    public class ReQuestChatAPI
    {
        public string question;
    }    
    
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
        string url = "https://parental-photo-mag-linda.trycloudflare.com/generative_ai";
        UnityWebRequest request = new UnityWebRequest(url,"POST");// tạo request
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json(textpost))); // tạo body upload
        request.downloadHandler = new DownloadHandlerBuffer(); // tạo body nhận tất cả data
        request.SetRequestHeader("Content-Type", "application/json"); // báo api biết là gửi json

        yield return request.SendWebRequest(); // gửi request và đợi

        if(request.result == UnityWebRequest.Result.Success)
        {
            static_connect = true;

            string jsonResponse = request.downloadHandler.text;
            chatAPI data = new chatAPI();
            data = JsonUtility.FromJson<chatAPI>(jsonResponse);
            onDone.Invoke(data.detail);
        }
        else
        {
            static_connect =false;
            Debug.Log("error connect API");
        }
    }   
    
    public string json(string txtReQuest)
    {
        ReQuestChatAPI data = new ReQuestChatAPI();
        data.question = txtReQuest;

        string json = JsonUtility.ToJson(data); // convert sang json
        return json;
    }    
}

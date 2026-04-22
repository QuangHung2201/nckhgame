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
        public string answer;
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
        string url = "https://cement-flexible-darwin-departmental.trycloudflare.com/generative_ai";

        UnityWebRequest request = new UnityWebRequest(url, "POST");

        string body = json(textpost);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text); // debug

        if (request.result == UnityWebRequest.Result.Success)
        {
            chatAPI data = JsonUtility.FromJson<chatAPI>(request.downloadHandler.text);
            onDone?.Invoke(data.answer);
        }
        else
        {
            Debug.LogError(request.error);
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

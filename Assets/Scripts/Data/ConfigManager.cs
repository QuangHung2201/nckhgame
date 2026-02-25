
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager instance;
    public screens creenslist ; // list các màn - lấy ở đây để sử dụng ( không khởi tạo vì đã gắn )
    public questionss questionslist; // list các các câu hỏi của địa danh ( khởi tạo vì không kế thừa monobihavior và không gắn )
    public Dictionary<string, questionss> Dic_location; // dictionary : lưu các data json đã được load để sử dụng chỉ cần gọi key và không cần load lại
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject); // chuyển scene không bị huỷ
        questionslist = new questionss();
        Dic_location = new Dictionary<string, questionss>(); 
    }
    private void OnDestroy()
    {
        instance = null;
    }
    public void loadlocalscreenMap() // hàm load json từ đầu game
    {
        TextAsset screenjson = Resources.Load<TextAsset>("GamePlay_Manager/screens");

        creenslist = JsonUtility.FromJson<screens>(screenjson.text);
    }
    
    public void loadDatajsonQuestion(string idLocation) // hàm load data json khi vào game play
    {
        TextAsset Questionjson = Resources.Load<TextAsset>($"DataJsonGameplay/{idLocation}"); // dùng biến nội suy khi cần load json nào
        questionslist = JsonUtility.FromJson<questionss>(Questionjson.text); 
        Dic_location.Add(idLocation, questionslist); // lưu data json vào dic mỗi khi pasr xong json địa danh
        if (Dic_location.ContainsKey(idLocation)) Debug.Log("đã lưu json vào dic");
    }    
}

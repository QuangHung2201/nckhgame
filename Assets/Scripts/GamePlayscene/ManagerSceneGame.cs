using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerSceneGame : MonoBehaviour
{
    public static ManagerSceneGame Instance;
    private string idToppic; // lưu id topic cache
    private questionss datalocation;
    private int idexquestion;
    private List<GameObject> listbuttonclone;
    private screens screenList;

    public Button buttonsetting;
    public GameObject panel_setting;
    public GameObject Gbject_Question;
    public Transform content;
    public GameObject panelWin;
    public Image fillprogress;
    public TextMeshProUGUI text_timecdown;
    private int time_countdown; // thời gian countdown hiện tại
    public GameObject panel_answerfalse;
    public GameObject panel_fail;

    DG.Tweening.Sequence seq_countdown; // quản lý tween ( chuỗi hành động theo timeline )của countdown
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        idToppic = PrefManager.PrefSaveUserMap.GetUserLocationchoose(); // lây id địa danh
        datalocation = ConfigManager.instance.Dic_location[idToppic];  // tìm data theo key id
        listbuttonclone = new List<GameObject>();  // mỗi lần vào khởi tạo 1 list mới
        screenList = ConfigManager.instance.creenslist; // lấy data screen json   
        seq_countdown = DOTween.Sequence();
        SetdataGameplay();
        countdown(20);

    }
    private void OnEnable()
    {
        buttonsetting.onClick.AddListener(openPanelSetting);
    }
    private void OnDestroy()
    {
        buttonsetting.onClick.RemoveAllListeners();
        Instance = null;
    }
    public void SetdataGameplay()
    {
        idexquestion = PrefManager.PrefSaveUserMap.GetUserQuestionLocationID(idToppic); // lấy id câu hỏi hiện tại theo địa danh
        string question = datalocation.questions[idexquestion].question; // lấy câu hỏi của địa danh theo id câu hỏi đã lưu
        PrefManager.PrefSaveUserMap.SetSizeToppic(idToppic, datalocation.questions.Count);// lưu số lượng câu hỏi theo địa danh

        fillprogress.fillAmount =  (float)(idexquestion )  / (datalocation.questions.Count);  // bị thiếu pregress khi chưa đầy
        Gbject_Question.GetComponent<Question>().setQuestion(question);
        SetAnswers();
        countdown(20);
    }    

    public void SetAnswers()
    {
        List<(string answer,bool correct)> answerlist = new List<(string,bool)> ();
        answerlist.Add((datalocation.questions[idexquestion].answertrue,true));  // thêm answer đúng vào list
        answerlist.Add((datalocation.questions[idexquestion].answerfalse1, false));  // thêm answer sai vào list
        answerlist.Add((datalocation.questions[idexquestion].answerfalse2, false));  
        answerlist.Add((datalocation.questions[idexquestion].answerfalse3, false)); 
        answerlist = answerlist.OrderBy(x => UnityEngine.Random.value).ToList(); // trộn lại đáp án mỗi khi vào câu hỏi

        if(listbuttonclone.Count == 0) // nếu chưa khởi tạo lần nào thì sẽ spawn và set data
        {
            spawnbuttonanswer(); 
            for (int i = 0; i< listbuttonclone.Count; i++)
               {
                 listbuttonclone[i].GetComponent<ItemAnswer>().SetData(answerlist[i].answer, answerlist[i].correct);
               }  
        }
        else // nếu đã khởi tạo thì chỉ cần set data mới vào cho nó
        {
            for (int i = 0; i < listbuttonclone.Count; i++)
            {
                listbuttonclone[i].GetComponent<ItemAnswer>().SetData(answerlist[i].answer, answerlist[i].correct);
            }
        }    
    }    

    public void spawnbuttonanswer()
    {
        GameObject answer = Resources.Load<GameObject>("sceneGameplay/itemAnswer");
        for (int i = 0; i < 4; i++)
        {
            GameObject answerClone = Instantiate(answer);
            answerClone.transform.SetParent(content, false);
            listbuttonclone.Add(answerClone);
        }
    }   
    public void checkcorrect(bool correct)
    {
        string idmapcache = PrefManager.PrefSaveUserMap.GetUserMapchoose();
        string idtoppiccache = PrefManager.PrefSaveUserMap.GetUserLocationchoose();

        if(correct == true)
        {
            CoinBasket.Instance.upDataCoinBasket();
            idexquestion++;
            PrefManager.PrefSaveUserMap.SetUserQuestionLocationID( idToppic,idexquestion);  // nếu đúng sẽ tăng id câu hỏi của địa danh đó


            idexquestion = PrefManager.PrefSaveUserMap.GetUserQuestionLocationID(idToppic); // lấy id câu hỏi hiện tại theo địa danh
            if (idexquestion >= datalocation.questions.Count) // nếu đã trả lời hết thì set lại id câu hỏi của địa danh về 0
            {
                  if(checkToppicLast(idmapcache,idtoppiccache) == 1)    // check nếu là item cuối
                 {
                    PrefManager.PrefSaveUserMap.SetStaticLocation(idmapcache, 1);
                    Debug.Log( ""+ idmapcache + ".đã hoàn thành :" + PrefManager.PrefSaveUserMap.GetstaticLocation(idmapcache));
                 }    
                fillprogress.fillAmount = 1f; // nếu trả lời hết sẽ đặt đầy
                PrefManager.PrefSaveUserMap.SetStaticToppic(idtoppiccache, idmapcache,1); // nếu đã hoàn thành thì lưu trạng thái đã hoàn thành
                Debug.Log(PrefManager.PrefSaveUserMap.GetstaticToppic(idtoppiccache,idmapcache));
                PrefManager.PrefSaveUserMap.SetUserQuestionLocationID(idToppic, 0);
                panelWin.SetActive(true);
                killCountDown();
            }
            else  // nếu còn câu hỏi mới set
            {
                SetdataGameplay(); // và set lại câu hỏi tiếp theo
            }
        }
        else
        {
            int timepresent = time_countdown - 10; // trả lời sai bị trừ 10s
            if(timepresent < 0) 
            { 
                timepresent = 0;
                killCountDown();
                panel_fail.SetActive(true);
                CoinBasket.Instance.resetCoin();// hết giờ sẽ reset lại coin
            }
            else
            {
              countdown(timepresent); // cập nhật lại countdown
              panel_answerfalse.SetActive(true);
              StartCoroutine(downPanelAnswerfalse());
            }    

        }    
    }    

    IEnumerator downPanelAnswerfalse()
    {
        yield return new WaitForSeconds(2f);
        panel_answerfalse.SetActive(false);
    }
    public void openPanelSetting()  // bật tắt panel setting
    {
        if(panel_setting.active == false)
        {
            pauseCountdown();
            panel_setting.SetActive(true);
        }
        else
        {
            playcoutdown();
        }    
    }  
    public int checkToppicLast(string idLocation,string idToppic)
    {
        if (idLocation == "") idLocation = screenList.playscreens[0].ID;

        for (int i = 0; i < screenList.playscreens.Count; i++)
        {
            if (screenList.playscreens[i].ID == idLocation) 
            {
               if (screenList.playscreens[i].location.Count > 0)
                {
                    int indexlast = screenList.playscreens[i].location.Count - 1;
                    Debug.Log("đã tìm thấy ");
                    string check = screenList.playscreens[i].location[indexlast].id;
                    if (check == idToppic)
                    {
                        return 1;
                    } 
                }
               return 0;
            }
        }
               return 0;
    }    
    public void countdown(int time_cd)
    {
        int time_countdowns = time_cd;
        seq_countdown.Kill();
        seq_countdown = DOTween.Sequence();// sau khi kill phải khởi tạo lại cho nó);
        for(int i = time_countdowns; i >= 0; i--)
        {

            int tmp = i;  // biến để lưu vào timeline sequence

            seq_countdown.AppendCallback(() =>
            {
                text_timecdown.text = tmp.ToString();

                if(tmp <= 5)// hiệu ứng text khi nhỏ hơn bằng 5
                {
                text_timecdown.DOColor(Color.red, 0.3f);
                text_timecdown.transform
                .DOScale(1.3f,0.3f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    text_timecdown.DOColor(Color.black, 0.2f);
                    text_timecdown.transform.DOScale(1f, 0.2f);
                });
                }    

                time_countdown = tmp; // cập nhật time hiện tại 
            });  // thêm hành động vào timeline sequence
            seq_countdown.AppendInterval(1f); // thêm khoảng thời gian 1f vào timeline sequence
        }
        seq_countdown.OnComplete(() => panel_fail.SetActive(true));
    }    
     
    private void pauseCountdown() // taạm dừng sequence
    {
        seq_countdown.Pause();
    }    
    public void playcoutdown() // tiếp tục sequence
    {
        seq_countdown.Play();
        panel_setting.SetActive(false );
    }    
    public void killCountDown() // xoá sequence
    {
        seq_countdown.Kill();
    }    
    public void backMain()
    {
        SceneManager.LoadScene(1);
    }    

}

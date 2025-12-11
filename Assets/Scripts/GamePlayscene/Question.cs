using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Question : MonoBehaviour
{
    public TextMeshProUGUI textQuestion;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setQuestion(string textQuestions)  // hàm set data
    {
        textQuestion.text = textQuestions;
    }    
    
}

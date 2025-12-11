using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Continue : MonoBehaviour
{
    Button mybutton;
    private void OnEnable()
    {
        mybutton = gameObject.GetComponent<Button>();
        mybutton.onClick.AddListener(exitMain);
    }
    public void exitMain()
    {
        SceneManager.LoadScene(1);
    }    
}

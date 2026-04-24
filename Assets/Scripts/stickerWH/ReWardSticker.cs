using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReWardSticker : MonoBehaviour
{
    [SerializeField] private Button btnClose;
    [SerializeField] private TextMeshProUGUI txt_name;
    [SerializeField] private Transform tick1;
    [SerializeField] private Transform tick2;
    [SerializeField] private Transform tick3;
    [SerializeField] private Transform tick4;
    void Start()
    {
        setName();
        aniSticker();
    }

    private void OnEnable()
    {
        btnClose.onClick.AddListener(eventClose);
    }
    private void OnDisable()
    {
        btnClose.onClick.RemoveAllListeners();
    }
    public void aniSticker()
    {
        tick1.localScale = Vector3.zero;
        tick2.localScale = Vector3.zero;
        tick3.localScale = Vector3.zero;
        tick4.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(tick1.DOScale(1f, 0.4f).SetEase(Ease.InBounce));
        seq.Append(tick2.DOScale(1f, 0.4f).SetEase(Ease.InBounce));
        seq.Append(tick3.DOScale(1f, 0.4f).SetEase(Ease.InBounce));
        seq.Append(tick4.DOScale(1f, 0.4f).SetEase(Ease.InBounce));
        seq.OnComplete(()=> seq.Kill());
    }    
    public void eventClose()
    {
        gameObject.SetActive(false);
    } 
    public void setName()
    {
       string name = PrefManager.PrefProfiles.GetUserName();
        if(name != "")
        {
            txt_name.text = name;
        }
        else
        {
            txt_name.text = "Người chơi xuất sắc";
        }
    }    
}

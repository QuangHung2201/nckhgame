using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructControll : MonoBehaviour
{
    [SerializeField] private Image arrow_1;
    [SerializeField] private Image arrow_2;
    [SerializeField] private Image img1;
    [SerializeField] private Image img2;
    [SerializeField] private Image img3;
    [SerializeField] private TextMeshProUGUI txt_1;
    [SerializeField] private TextMeshProUGUI txt_2;
    [SerializeField] private TextMeshProUGUI txt_3;
    [SerializeField] private Button btncont;

    Sequence seq;
    void Start()
    {
    }
    private void OnEnable()
    {
        animation_instr();
    }
    public void animation_instr()
    {
        MainEvent.instance.OpenPanel();
        seq = DOTween.Sequence();
        arrow_1.transform.localScale = Vector3.zero;
        arrow_2.transform.localScale = Vector3.zero;
        img1.transform.localScale = Vector3.zero;
        img2.transform.localScale = Vector3.zero;
        img3.transform.localScale = Vector3.zero;
        txt_1.transform.localScale = Vector3.zero;
        txt_2.transform.localScale = Vector3.zero;
        txt_3.transform.localScale = Vector3.zero;
        animation();
        btncont.onClick.AddListener(eventCont);
    }    
    public void animation()
    {
        seq.Append(img1.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce));
        seq.Append(txt_1.transform.DOScale(1f, 0.4f));
        seq.Append(arrow_1.transform.DOScale(1f,0.4f).SetEase(Ease.OutBack));

        seq.Append(img2.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce));
        seq.Append(txt_2.transform.DOScale(1f, 0.4f));
        seq.Append(arrow_2.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack));

        seq.Append(img3.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBounce));
        seq.Append(txt_3.transform.DOScale(1f, 0.4f));


        seq.AppendCallback(() =>
        btncont.gameObject.SetActive(true)
        );

        seq.OnComplete(() =>
        seq.Kill()
        );

    }    

    public void eventCont()
    {
        MainEvent.instance.ClosePanel();
        gameObject.transform.DOScale(0f, 0.5f).SetEase(Ease.InBack);
        StartCoroutine(delay1());
    }    
    IEnumerator delay1()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }    
}

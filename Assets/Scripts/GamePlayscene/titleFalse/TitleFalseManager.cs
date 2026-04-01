using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFalseManager : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private RectTransform m_rectStamp;
    [SerializeField] private GameObject tick_false;
    [SerializeField] private GameObject stamp;


    private void Start()
    {

    }
    private void OnEnable()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_rectStamp = stamp.GetComponent<RectTransform>();
        tick_false.SetActive(false);
        stamp.SetActive(true);
        scaleBgAni();
    }

    public void scaleBgAni()
    {
        ManagerSceneGame.Instance.panel_unlick.SetActive(true);
        m_RectTransform.sizeDelta = new Vector2(m_RectTransform.sizeDelta.x - 300f, m_RectTransform.sizeDelta.y);
        m_RectTransform.DOSizeDelta(new Vector2(m_RectTransform.sizeDelta.x + 300f, m_RectTransform.sizeDelta.y),0.3f)
            .SetEase(Ease.OutBack)
            .OnComplete(()=>
            aniTickAndStamp());
    }
    
    public void aniTickAndStamp()
    {
        Sequence seq = DOTween.Sequence();
        Vector3 startPos = m_rectStamp.anchoredPosition;

        seq.Append( m_rectStamp
        .DOAnchorPos(
            startPos + new Vector3(50f,50f)
            , 0.3f));

        seq.Append(
           m_rectStamp.DOAnchorPos(startPos, 0.6f)
          .SetEase(Ease.InBack)
          .OnComplete(() =>
          {
              tick_false.SetActive(true);
          })
          );
        seq.AppendInterval(0.4f);
        seq.AppendCallback(()=>
        {
            stamp.SetActive(false);
            ManagerSceneGame.Instance.panel_unlick.SetActive(false);
        }
            ) ;
    }    
}
